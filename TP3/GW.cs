using System;
using System.IO;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.Collections.Generic;
using System.Timers;
namespace TP3
{
  public class GW
  {
        // Constantes et propriétés statiques
        
        /// <summary>
        /// Dimension de la fenêtre de rendu
        /// </summary>
        public const int WIDTH = 1024;
        public const int HEIGHT = 768;
        /// <summary>
        /// Limite FPS
        /// </summary>
        public const uint FRAME_LIMIT = 60;
        const float DELTA_T = 1.0f / (float)FRAME_LIMIT;
        /// <summary>
        /// Random utilisé pour plusieurs choses
        /// </summary>
        private static Random r = new Random();
        /// <summary>
        /// le spaceship
        /// </summary>
        public Hero hero = null;
        /// <summary>
        /// Timer pour calculer le temps total
        /// </summary>
        Timer tempsTotal = new Timer(1000);
        /// <summary>
        /// Liste pour les tous les éléments du jeu
        /// </summary>
        public List<Projectile> projectiles = new List<Projectile>();
        List<Projectile> projectilesToREmove = new List<Projectile>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Enemy> enemiesToRemove = new List<Enemy>();
        List<Star> stars = new List<Star>();
        public List<Enemy> basicEnemies = new List<Enemy>();
        public List<Particule> particles = new List<Particule>();
        List<Particule> particlesToRemove = new List<Particule>();

        // SFML
        RenderWindow window = null;
        Font font = new Font("Data/emulogic.ttf");
        Text text = null;

        // Propriétés pour la partie
        float totalTime = 0;

        // Langage utilisé pour l'interface
        public Language CurrentLanguage = Language.French;
    
        private void OnClose(object sender, EventArgs e)
        {
          RenderWindow window = (RenderWindow)sender;
          window.Close();
        }

        void OnKeyPressed(object sender, KeyEventArgs e)
        {
  
        }
        public GW()
        {
            text = new Text("", font);
            window = new RenderWindow(new SFML.Window.VideoMode(WIDTH, HEIGHT), "GW");
            window.Closed += new EventHandler(OnClose);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.SetKeyRepeatEnabled(false);
            window.SetFramerateLimit(FRAME_LIMIT);
            //Spawn du héro
            hero = new Hero(WIDTH / 2, HEIGHT / 2);

            //Spawn des étoiles
            for (int i = 0; i < 75; i++)
            {
                stars.Add(new Star(r.Next(0, WIDTH), r.Next(0, HEIGHT), Color.Yellow, r.Next(1, 3), r.Next(5, 10)));
            }
            for (int i = 0; i < 75; i++)
            {
                stars.Add(new Star(r.Next(0, WIDTH), r.Next(0, HEIGHT), Color.White, r.Next(1, 3), r.Next(5, 10)));
            }
            for (int i = 0; i < 75; i++)
            {
                stars.Add(new Star(r.Next(0, WIDTH), r.Next(0, HEIGHT), Color.Red, r.Next(1, 3), r.Next(5, 10)));
            }
            //Temps Total
            tempsTotal.Start();
            tempsTotal.AutoReset = false;
        }
        public void Run()
        {
            // ppoulin
            // Chargement de la StringTable. A décommenter au moment opportun
            if (ErrorCode.OK == StringTable.GetInstance().Parse(File.ReadAllText("Data/st.txt")))
            {
            window.SetActive();
        
            while (window.IsOpen)
            {
              window.Clear(Color.Black);
              window.DispatchEvents();
              if (false == Update())
                break;
              Draw();
              window.Display();
            }
          }
        }
        /// <summary>
        /// Affichage de tous les éléments du jeu
        /// </summary>
        public void Draw()
        {
            // Parcourez les listes appropriées pour faire afficher les éléments demandés.
            hero.Draw(window);
            foreach (Projectile i in projectiles)
            {
                i.Draw(window);
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(window);
            }
            foreach (Enemy enemy in basicEnemies)
            {
                enemy.Draw(window);
            }
            foreach (Star star in stars)
            {
                star.Draw(window);
            }
            foreach (Particule particule in particles)
            {
                particule.Draw(window);
            }

            // Affichage des statistiques.A décommenter au moment opportun
            //Temps total
            text.Position = new Vector2f(0, 10);
            text.DisplayedString = string.Format("{1} = {0,-5}", ((int)(totalTime)).ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_TOTAL_TIME"));
            window.Draw(text);

            //Points de vie
            text.Position = new Vector2f(0, 50);
            text.DisplayedString = string.Format("{1} = {0,-4}", hero.Life.ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_LIFE"));
            window.Draw(text);
        }

        /// <summary>
        /// Détermine si un Movable est situé à l'intérieur de la surface de jeu
        /// Peut être utilisée pour déterminer si les projectiles sont sortis du jeu
        /// ou si le héros ou un ennemi s'apprête à sortir.
        /// </summary>
        /// <param name="m">Le Movable à tester</param>
        /// <returns>true si le Movable est à l'intérieur, false sinon</returns>
        public bool Contains(Movable m)
        {
          FloatRect r = new FloatRect(0, 0, GW.WIDTH, GW.HEIGHT);
          return r.Contains(m.Position.X, m.Position.Y);
        }

        /// <summary>
        /// Update de tous les éléments du jeu
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            //Temps total
            if(tempsTotal.Enabled == false)
            {
                totalTime++;
                tempsTotal.Start();
            }
            // A compléter
            #region Updates
            // Particules
            foreach (Particule particule in particles)
            {
                particule.Update(this);
            }
            // Étoiles      
            foreach (Star star in stars)
            {
                star.Update(this);
            }
            // Personnages et projectiles
            if(hero.Update(this)== false)
            {
                Environment.Exit(0);
            }           
            foreach(Enemy enemy in enemies)
            {
                enemy.Update(this);
            }
            foreach (Enemy enemy in basicEnemies)
            {
                enemy.Update(this);
            }
            
            #endregion
            #region Gestion des collisions
            // Collision projectile hero et enemy
            foreach (Projectile projectile in projectiles)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.Intersects(projectile) && projectile.Type == CharacterType.Hero)
                    {
                        basicEnemies.Add(new BasicEnemy(enemy.Position.X - 30, enemy.Position.Y));
                        basicEnemies.Add(new BasicEnemy(enemy.Position.X + 30, enemy.Position.Y));
                        basicEnemies.Add(new BasicEnemy(enemy.Position.X, enemy.Position.Y + 30));
                        //Effet lors de la mort 
                        for (int i = 0; i < 360; i++)
                        {
                            particles.Add(new Particule(enemy.Position.X, enemy.Position.Y, enemy.Color, 0.5f, i, 6.0f));
                        }
                        enemiesToRemove.Add(enemy);
                        projectilesToREmove.Add(projectile);

                    }
                }
                foreach (Enemy enemy in basicEnemies)
                {
                    if (enemy.Intersects(projectile) && projectile.Type == CharacterType.Hero)
                    {
                        enemiesToRemove.Add(enemy);
                    }
                }
                if (hero.Intersects(projectile) && projectile.Type == CharacterType.Ennemi)
                {
                    hero.Life--;
                    projectilesToREmove.Add(projectile);
                }
            }
            // Colision hero vs ennemy
            foreach (Enemy enemy in enemies)
            {
                if (enemy.Intersects(hero))
                {
                    basicEnemies.Add(new BasicEnemy(enemy.Position.X - 30, enemy.Position.Y));
                    basicEnemies.Add(new BasicEnemy(enemy.Position.X + 30, enemy.Position.Y));
                    basicEnemies.Add(new BasicEnemy(enemy.Position.X, enemy.Position.Y + 30));

                    for (int i = 0; i < 360; i++)
                    {
                        particles.Add(new Particule(enemy.Position.X, enemy.Position.Y, Color.Cyan, 0.5f, i, 6.0f));
                    }
                    enemiesToRemove.Add(enemy);
                    hero.Life -= 2;

                }
            }
            foreach (Enemy enemy in basicEnemies)
            {
                if (enemy.Intersects(hero))
                {
                    enemiesToRemove.Add(enemy);
                    hero.Life--;
                }
            }
            #endregion
            #region Retraits
            // Retrait des ennemis détruits , des projectiles inutiles et des particules 
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update();
                if (projectiles[i].Position.X < 0 || projectiles[i].Position.X > WIDTH || projectiles[i].Position.Y < 0 || projectiles[i].Position.Y > HEIGHT)
                {
                    projectiles.Remove(projectiles[i]);
                }
            }
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].Position.X < 0 || particles[i].Position.X > WIDTH || particles[i].Position.Y < 0 || particles[i].Position.Y > HEIGHT)
                {
                    particles.Remove(particles[i]);
                }
            }
            foreach (Enemy enemy in enemiesToRemove)
            {
                
                enemies.Remove(enemy);
                basicEnemies.Remove(enemy);
                
            }
            foreach (Projectile projectile in projectilesToREmove)
            {
                projectiles.Remove(projectile);

            }
            #endregion
            #region Spawning des nouveaux ennemis
            // On veut avoir au minimum 5 ennemis (n'incluant pas les triangles). Il faut les ajouter ici
            if (enemies.Count < 5)
            {
                int rnddd = r.Next(0, 100);
                if (rnddd < 30)
                {
                    enemies.Add(new Square(r.Next(0, WIDTH), r.Next(-HEIGHT / 6, 0)));
                }
                else if (rnddd >= 30 && rnddd < 60)
                {
                    enemies.Add(new Circle(r.Next(0, WIDTH), r.Next(-HEIGHT / 6, 0)));
                }
                else if (rnddd >= 60)
                {
                    enemies.Add(new Circle(r.Next(0, WIDTH), r.Next(HEIGHT, HEIGHT + 20)));
                }

            }
            #endregion
            // ppoulin 
            // A COMPLETER
            // Retourner true si le héros est en vie, false sinon.
            if (hero.Life <= 0)
            {
                return false;
            }
            return true;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.Timers;
namespace TP3
{
    public class Hero : Character
    {
        Random rnd = new Random();
       
        // temps entre chaque tire
        Timer fireDelay = new Timer(100);

        // temps entre chaque bombe
        Timer bombDelay = new Timer(1000);
        /// <summary>
        /// Vitesse du Hero
        /// </summary>
        public static float heroSpeed = 5.0f;
        /// <summary>
        /// Accesseur sur les points de vie du hero
        /// </summary>
        public int Life
        {
            get;
            set;
        }
        /// <summary>
        /// Nombre de point de vie du hero au debut de la partie
        /// </summary>
        public const int LIFE_AT_BEGINNING = 100;
        /// <summary>
        /// Couleur du Hero
        /// </summary>
        static Color heroColor = Color.Cyan;
        /// <summary>
        /// Nombre de bombes que le héro possède
        /// </summary>
        int nbBombs = 3;
        
        public Hero(float posX, float posY)
            : base(posX, posY, 3, heroColor, heroSpeed)
        {
            nbBombs = 3;
            Life = LIFE_AT_BEGINNING;
            Vector2f point1 = new Vector2f(15,0);
            Vector2f point2 = new Vector2f(-20,10 );
            Vector2f point3 = new Vector2f(-20, -10);
            this[0] = point1;
            this[1] = point2;
            this[2] = point3;
            fireDelay.AutoReset = false;
            bombDelay.AutoReset = false;
        }
        public bool Update(GW gw)
        {      
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {

                Advance(heroSpeed);
                //"flamme" derrière le hero
                gw.particles.Add(new Particule(Position.X, Position.Y, Color.Yellow, 1.3f, Angle - rnd.Next(170,190) , 5));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {

                Advance(-heroSpeed);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Rotate(5);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                Rotate(-5);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (fireDelay.Enabled == false)
                {
                    fireDelay.Enabled = true;
                    gw.projectiles.Add(new Projectile(CharacterType.Hero, Position.X, Position.Y, 0, Color.Blue, Angle,10));
                }
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                //effet de la bombe
                if (bombDelay.Enabled == false)
                {
                    if (nbBombs > 0)
                    {
                        for (int i = 0; i < 360; i++)
                        {
                            gw.particles.Add(new Particule(Position.X, Position.Y, Color.Yellow, 0.5f, i,25));
                        }
                        foreach (Enemy enemy in gw.enemies)
                        {
                            gw.enemiesToRemove.Add(enemy);
                        }
                        foreach (Enemy enemy in gw.basicEnemies)
                        {
                            gw.enemiesToRemove.Add(enemy);
                        }
                        nbBombs--;
                    }
                    bombDelay.Enabled = true;
                }

            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F4))
            {
                gw.CurrentLanguage = Language.French;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F5))
            {
                gw.CurrentLanguage = Language.English;
            }
            return true;
        }

        

        

    }
}

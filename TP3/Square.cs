using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using System.Timers;
namespace TP3
{
    /// <summary>
    /// Il avance vers le joueur et quand il arrive
    /// à une distance déterminer du joueur il arrête d’avancer et tire sur le joueur.
    /// Les projectiles font perdre 1 point de vie et quand le joueur fonce dans le carrée il perd 2 points de vie. 
    /// </summary>
    class Square : Enemy
    {
        /// <summary>
        /// distance de reaction par rapport au joueur
        /// </summary>
        float distanceReaction = 0;
        /// <summary>
        /// Temps entre chaque tire
        /// </summary>
        Timer fireDelay = new Timer(100);
        /// <summary>
        /// Taille du carré
        /// </summary>
        static int SquareSize = 10;
        /// <summary>
        /// Vitesse du carré
        /// </summary>
        static int SquareSpeed = 1;

        public Square(float posX,float posY)
            :base(posX,posY,4,SquareSize,Color.Magenta,SquareSpeed)
        {
            this[0] = new Vector2f(-SquareSize, SquareSize);
            this[1] = new Vector2f(SquareSize, SquareSize);
            this[2] = new Vector2f(SquareSize, -SquareSize);
            this[3] = new Vector2f(-SquareSize, -SquareSize);
            fireDelay.AutoReset = false;
            distanceReaction = 250;

        }
        public override bool Update(GW gw)
        {
            base.Update(gw);
            //Calcul pour la distance entre le Hero et le carré
            if(Math.Sqrt((Math.Pow(Math.Max(Position.X,gw.hero.Position.X)-Math.Min(Position.X,gw.hero.Position.X),2)) + (Math.Pow(Math.Max(Position.Y,gw.hero.Position.Y)-Math.Min(Position.Y,gw.hero.Position.Y),2))) < distanceReaction)
            {
                
                if (fireDelay.Enabled == false)
                {
                    gw.projectiles.Add(new Projectile(CharacterType.Ennemi, Position.X, Position.Y, 0, Color.Green, Angle,5));
                    fireDelay.Enabled = true;
                    fire = true;
                }
            }
            else
            {
                fire = false;
            }
            return true;

        }
        
    }
}

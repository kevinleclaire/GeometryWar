using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
namespace TP3
{
    /// <summary>
    /// Un personnage est sois un ennemy ou un hero
    /// </summary>
    public class Character : Movable
    {
        /// <summary>
        /// Methode qui permet au personnage d'avancer sans sortir de la fenêtre 
        /// </summary>
        /// <param name="nbPixels"></param>
        protected override void Advance(float nbPixels)
        {
            base.Advance(nbPixels);
            if (Position.Y > GW.HEIGHT)
            {
                Position = new Vector2f(Position.X, GW.HEIGHT);        
            }
            else if (Position.Y < 0)
            {
                Position = new Vector2f(Position.X, 0);        
            }

            if (Position.X > GW.WIDTH)
            {
                Position = new Vector2f(GW.WIDTH, Position.Y);           
            }
            else if (Position.X < 0)
            {
                Position = new Vector2f(0, Position.Y);           
            }

        }
        protected Character(float posX,float posY,UInt32 nbVertices,Color color,float speed)
            :base(posX,posY,nbVertices,color,speed)
        {

        }
    }

}

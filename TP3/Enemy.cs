using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
namespace TP3
{
    /// <summary>
    /// Un ennemi peut sois etre un cercle, un carré ou un triangle 
    /// </summary>
   public  class Enemy : Character
    {
        
        /// <summary>
        /// Si l'ennemi est en train de faire feu
        /// </summary>
        public bool fire = false;

        protected Enemy(float posX, float posY, UInt32 nbVertices, float size, Color color, float speed)
            : base(posX, posY, nbVertices, color, speed)
        {
        }
        public virtual bool Update(GW gw)
        {
            if(fire == false)
            {
                Advance(Speed);
            }        
            //Calcul pour le pathfinder  
            float diffangle = (float)(Math.Atan2(gw.hero.Position.Y - Position.Y, gw.hero.Position.X - Position.X) * 180 / Math.PI);
            Angle = 0;
            Rotate(diffangle);
                    
            return true;
        }
    }
}

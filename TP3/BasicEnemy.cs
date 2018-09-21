using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace TP3
{
    /// <summary>
    /// Ces ennemis sont spawner seulement quand un des deux autres ennemis meurt
    /// </summary>
    class BasicEnemy : Enemy
    {
        /// <summary>
        /// Taille du triangle
        /// </summary>
        static int BasicSize = 5;
        /// <summary>
        /// Vitesse du triangle
        /// </summary>
        static int BasicSpeed = 1;

        public BasicEnemy(float posX, float posY)
            : base(posX, posY, 3, 5, Color.Red, BasicSpeed)
        {
            this[0] = new Vector2f(0,BasicSize);
            this[1] = new Vector2f(-BasicSize, -BasicSize);
            this[2] = new Vector2f(BasicSize, -BasicSize);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace TP3
{
    /// <summary>
    /// Petit cercle utilisé pour faire l'explosion des ennemies et pour les "flammes" derrière le hero
    /// </summary>
    public class Particule : Movable
    {
        /// <summary>
        /// Taille des particules
        /// </summary>
        float particleSize = 1;
        /// <summary>
        /// Couleur des particules
        /// </summary>
        Color color = Color.Red;

        CircleShape particle = null;

        public Particule(float posX, float posY, Color color, float particleSize,float angle,float speed)
            : base(posX, posY, 0,color,speed)
        {
            this.color = color;
            this.particleSize = particleSize;
            particle = new CircleShape(particleSize);
            Angle = angle;
            
        }
        public void Update(GW gw)
        {  
            Advance(Speed);
        }
        public override void Draw(RenderWindow window)
        {
            particle.Position = new Vector2f(Position.X, Position.Y);
            particle.Origin = new Vector2f(5 * 0.5f, 5 * 0.5f);
            particle.FillColor = color;
            window.Draw(particle);
        }
    }
}

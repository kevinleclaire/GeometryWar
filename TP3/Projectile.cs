using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
    /// <summary>
    /// Les projectiles tirés par les ennemies ou le Hero
    /// </summary>
    public class Projectile : Movable
    {
        /// <summary>
        /// Pour determiner si un ennemi tir le projectile ou le hero
        /// </summary>
        public CharacterType Type
        {
            get;
            set;
        }

        CircleShape projShape = null;
        /// <summary>
        /// Couleur du projectile
        /// </summary>
        Color color = Color.Cyan;
        
        public Projectile(CharacterType type,float posX,float posY,uint nbVertices,Color color,float angle,float speed)
            :base(posX,posY,nbVertices,color, speed)
        {
            Type = type;
            Angle = angle;
            this.color = color;
            projShape = new CircleShape(3);
           
        }

        public bool Update()
        {
            Advance(Speed);
            return true;          
        }

        public override void Draw(RenderWindow window)
        {
           
            projShape.Position = new Vector2f(Position.X, Position.Y);
            projShape.Origin = new Vector2f(5 * 0.5f, 5 * 0.5f);
            projShape.FillColor = color;
            window.Draw(projShape);
            
            
        }

        public override FloatRect BoundingBox
        {
            get { return projShape.GetGlobalBounds(); }
        }


    }
}

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
    /// Les étoiles dans le background qui procure un effet 3D
    /// </summary>
    public class Star : Movable
    {
        /// <summary>
        /// Taille de l'étoile
        /// </summary>
        float starSize = 1;
        /// <summary>
        /// Couleur de l'étoile
        /// </summary>
        Color color = Color.Red;
        /// <summary>
        /// Vitesse de l'étoile
        /// </summary>
        float starSpeed = 3.0f;

        CircleShape star = null;

        Random rnd = new Random();
      
        public Star(float posX, float posY, Color color, float starSize, float starSpeed)
            : base(posX, posY, 0, color, starSpeed)
        {
            this.color = color;
            this.starSize = starSize;
            star = new CircleShape(starSize);
            this.starSpeed = starSpeed;
        }

        public void Update(GW gw)
        {
            Angle = gw.hero.Angle - 180;
            Advance(starSpeed);
            this.Respawn();           
        }

        public override void Draw(RenderWindow window)
        {
            star.Position = new Vector2f(Position.X, Position.Y);
            star.Origin = new Vector2f(5 * 0.5f, 5 * 0.5f);
            star.FillColor = color;
            window.Draw(star);
        }
        /// <summary>
        /// Methode qui permet de faire respawn les étoiles quand elles sortent de la fenêtre
        /// </summary>
        public void Respawn()
        {
            Vector2f position = new Vector2f();
            if (Position.Y > GW.HEIGHT)
            {
                position = new Vector2f(Position.X, 0);
                Position = position;
            }
            else if (Position.Y < 0)
            {
                position = new Vector2f(Position.X, GW.HEIGHT);
                Position = position;
            }

            if (Position.X > GW.WIDTH)
            {
                position = new Vector2f(0, Position.Y);
                Position = position;
            }
            else if (Position.X < 0)
            {
                position = new Vector2f(GW.WIDTH, Position.Y);
                Position = position;
            }

        }

    }
}

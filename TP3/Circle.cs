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
using System.Timers;
namespace TP3
{
    /// <summary>
    /// Il avancent vers le joueur et quand il arrive à une distance déterminer
    /// il comment à tourner autour du joueur a une vitesse plus rapide.
    /// Quand il foncent dans le joueur, le joueur perd 2 points de vie.
    /// </summary>
    class Circle : Enemy
    {
        /// <summary>
        /// distance de reaction par rapport au joueur
        /// </summary>
        float distanceReaction = 0;

        CircleShape circle = null;
        /// <summary>
        /// Taille du cercle
        /// </summary>
        static int CircleSize = 10;
        /// <summary>
        /// Couleur du cercle
        /// </summary>
        Color color = Color.White;
        /// <summary>
        /// Vitesse du cercle
        /// </summary>
        static int CircleSpeed = 2;

        public Circle(float posX, float posY)
            : base(posX, posY, 0, CircleSize, Color.White, CircleSpeed)
        {
            circle = new CircleShape(CircleSize);
            Color = color;
            distanceReaction = 120;
        }
        /// <summary>
        /// On utilise un CircleShape alors on override le Draw
        /// </summary>
        /// <param name="window"></param>
        public override void Draw(RenderWindow window)
        {
            circle.Position = new Vector2f(Position.X, Position.Y);
            circle.Origin = new Vector2f(5 * 0.5f, 5 * 0.5f);
            circle.FillColor = color;
            window.Draw(circle);
        }
        public override FloatRect BoundingBox
        {
            get { return circle.GetGlobalBounds(); }
        }
        public override bool Update(GW gw)
        {
            //Calcul de la distance entre le joueur et le cercle
            if(Math.Sqrt((Math.Pow(Math.Max(Position.X, gw.hero.Position.X) - Math.Min(Position.X, gw.hero.Position.X), 2)) + (Math.Pow(Math.Max(Position.Y, gw.hero.Position.Y) - Math.Min(Position.Y, gw.hero.Position.Y), 2))) < distanceReaction)
            {
                Advance(10);
                Rotate(-10);
            }
            else
            {
                base.Update(gw);
            }
            return true;
        }

    }
}

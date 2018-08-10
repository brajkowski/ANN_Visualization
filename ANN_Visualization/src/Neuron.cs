using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization
{
    public static class Utility
    {
        public static void AlignCircles(ref CircleShape outer, ref CircleShape inner)
        {
            inner.Position = outer.Position;
            float adjustment = outer.Radius - inner.Radius;
            float newX = inner.Position.X + adjustment;
            float newY = inner.Position.Y + adjustment;
            inner.Position = new Vector2f(newX, newY);
        }
    }

    class Neuron
    {
        public List<Drawable> Drawables { get; }
        public CircleShape inner;
        public CircleShape outer;

        public Neuron(Vector2f position, float radius)
        {
            float occlusion = 0.8f;
            Drawables = new List<Drawable>();

            outer = new CircleShape(radius)
            {
                Position = position,
                FillColor = new Color(255, 255, 255, 90),
            };

            inner = new CircleShape(radius * occlusion)
            {
                FillColor = new Color(0, 100, 0),
                Position = position,
                
            };

            Utility.AlignCircles(ref outer, ref inner);
            Drawables.Add(outer);
            Drawables.Add(inner);
        }

        public void ChangeColor(byte green)
        {
            inner.FillColor = new Color(0, green, 0);
        }
    }
}

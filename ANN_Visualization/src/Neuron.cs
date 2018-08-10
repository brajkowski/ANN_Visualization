using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization
{
    public static class NeuronUtility
    {
        public static void AlignCircles(ref CircleShape outer, ref CircleShape inner)
        {
            inner.Position = outer.Position;
            float adjustment = outer.Radius - inner.Radius;
            float newX = inner.Position.X + adjustment;
            float newY = inner.Position.Y + adjustment;
            inner.Position = new Vector2f(newX, newY);
        }

        public static Vector2f CalculateCenter(ref CircleShape circle)
        {
            float x = circle.Position.X + circle.Radius;
            float y = circle.Position.Y + circle.Radius;
            return new Vector2f(x, y);
        }
    }

    class Neuron
    {
        //public List<Drawable> Drawables { get; }
        public CircleShape inner;
        public CircleShape outer;
        public Vector2f CenterPoint { get; }
        public float Activation;

        public Neuron(Vector2f position, float radius)
        {
            float occlusion = 0.8f;
            //Drawables = new List<Drawable>();

            outer = new CircleShape(radius)
            {
                Position = position,
                FillColor = new Color(90, 90, 90),
            };

            inner = new CircleShape(radius * occlusion)
            {
                FillColor = new Color(0, 0, 0),
                Position = position,
                
            };

            NeuronUtility.AlignCircles(ref outer, ref inner);
            CenterPoint = NeuronUtility.CalculateCenter(ref outer);
            ChangeActivation(0f);
            //Drawables.Add(outer);
            //Drawables.Add(inner);
        }

        public void ChangeColor(byte green)
        {
            inner.FillColor = new Color(0, green, 0);
        }

        public void Draw(ref MainWindow window)
        {
            outer.Draw(window, RenderStates.Default);
            inner.Draw(window, RenderStates.Default);
        }

        public void ChangeActivation(float a)
        {
            Activation = a;
            byte green = (byte)(255f * a / 1f);
            inner.FillColor = new Color(0, green, 0);
        }
    }
}

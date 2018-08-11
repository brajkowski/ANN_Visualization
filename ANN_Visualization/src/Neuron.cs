using System;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
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

    class Neuron : Drawable
    {
        public CircleShape inner;
        public CircleShape outer;
        public Vector2f CenterPoint { get; }
        public float Activation;
        public bool ShouldBeDrawn;

        public Neuron(Vector2f position, float radius)
        {
            float occlusion = 0.8f;
            ShouldBeDrawn = true;

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
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (ShouldBeDrawn)
            {
                target.Draw(outer);
                target.Draw(inner);
            }
        }

        public void ChangeActivation(float a)
        {
            if (a < 0)
            {
                Console.WriteLine("Warning: Neuron color based on negative activation.");
            }
            Activation = a;
            float green = (255f * a);
            inner.FillColor = new Color(0, (byte)(green), 0);
        }
    }
}

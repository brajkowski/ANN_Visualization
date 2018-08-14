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
        public Vector2f CenterPoint;
        public Popup BiasPopup;
        public float Activation;
        public float Bias;
        public bool ShouldBeDrawn;
        public bool ShouldDrawBiasPopup;

        public Neuron(Vector2f position, float radius, float bias)
        {
            float occlusion = 0.8f;
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

            BiasPopup = new Popup(CenterPoint);
            Bias = bias;
            BiasPopup.UpdateText("Bias: " + bias.ToString());

            ShouldBeDrawn = true;
            ShouldDrawBiasPopup = false;
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (ShouldBeDrawn)
            {
                target.Draw(outer);
                target.Draw(inner);

                if (ShouldDrawBiasPopup)
                {
                    target.Draw(BiasPopup);
                }
            }
        }

        public void ChangeActivation(float a)
        {
            if (a < 0)
            {
                Console.WriteLine("Warning: Neuron color based on negative activation.");
            }
            Activation = a;
            float green = (255f * a); // Creates linear relationship since 'a' should range from [0,1].
            inner.FillColor = new Color(0, (byte)(green), 0);
        }
    }
}

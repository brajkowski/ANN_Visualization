using System;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
{
    public static class ConnectionUtility
    {
        public static RectangleShape GenerateLine(ref Vector2f from, ref Vector2f to, float width)
        {
            float length = (float)(Math.Sqrt(Math.Pow(from.X - to.X, 2d) + Math.Pow(from.Y - to.Y, 2d)));
            var size = new Vector2f(length, width);
            float angle = (float)(Math.Asin(Math.Abs(from.X - to.X) / length) / Math.PI) * 180f;
            if (to.Y < from.Y)
            {
                angle = -(90 - angle);
            }
            else
            {
                angle = 90f - angle;
            }
            var fromAdjusted = new Vector2f(from.X + width / 2f, from.Y);
            var line = new RectangleShape(size)
            {
                Position = fromAdjusted,
                Rotation = angle,
            };
            return line;
        }
    }

    class Connection : Drawable
    {
        Neuron fromN;
        Neuron toN;
        Vector2f from;
        Vector2f to;
        RectangleShape line;
        float weight;
        int minOpacity;
        int maxOpacity;

        public Connection(Neuron from, Neuron to, float width)
        {
            fromN = from;
            toN = to;
            this.from = from.CenterPoint;
            this.to = to.CenterPoint;
            line = ConnectionUtility.GenerateLine(ref this.from, ref this.to, width);
            line.FillColor = new Color(20, 20, 20, 100);
            minOpacity = 10;
            maxOpacity = 100;
            weight = 0f;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (fromN.ShouldBeDrawn && toN.ShouldBeDrawn)
            {
                target.Draw(line, RenderStates.Default);
            }
        }

        public void ChangeWeight(float weight, float opacityFactor)
        {
            this.weight = weight;
            int red = 0;
            int green = 0;
            int opacity = minOpacity;
            if (weight <= 0)
            {
                red = 255;
            }
            else
            {
                green = 255;
            }
            opacity = (int)(minOpacity + opacityFactor * Math.Abs(weight));
            if (opacity > maxOpacity)
                opacity = maxOpacity;
            line.FillColor = new Color((byte)red, (byte)green, 0, (byte)opacity);
        }
    }
}

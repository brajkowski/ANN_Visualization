using System;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
{
    class Connection : Drawable
    {
        Neuron fromN;
        Neuron toN;
        Vector2f from;
        Vector2f to;
        RectangleShape line;
        public float weight;
        int minOpacity;
        int maxOpacity;

        public Connection(Neuron from, Neuron to, float width)
        {
            fromN = from;
            toN = to;
            this.from = from.CenterPoint;
            this.to = to.CenterPoint;
            line = SFMLGeometryUtility.GenerateLine(ref this.from, ref this.to, width);
            line.FillColor = new Color(20, 20, 20, 100);    // From visual trial and error.
            minOpacity = 1;                                 // Based on RGB scheme.
            maxOpacity = 255;                               // Based on RGB scheme.
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
            opacity = (int)(minOpacity + opacityFactor * Math.Abs(weight)); // opacityFactor is set from visualizer and is user controlled.

            // Protects against user setting opacityFactor too high.
            if (opacity > maxOpacity)
                opacity = maxOpacity;
            line.FillColor = new Color((byte)red, (byte)green, 0, (byte)opacity);
        }

        public void Dampen()
        {
            // Get color to keep connection red/green, then change opacity to min.
            Color newColor = line.FillColor;
            newColor.A = (byte)minOpacity;
            line.FillColor = newColor;
        }
    }
}

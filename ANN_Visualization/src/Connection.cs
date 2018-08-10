using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
{
    public static class ConnectionUtility
    {
        public static RectangleShape GenerateLine(ref Vector2f from, ref Vector2f to, float width)
        {
            float length = (float)(Math.Sqrt(Math.Pow(from.X - to.X, 2d) + Math.Pow(from.Y - to.Y, 2d)));
            //float width = 5f;
            var size = new Vector2f(length, width);
            float angle = (float)(Math.Asin(Math.Abs(from.X - to.X) / length) / Math.PI) * 180f;
            angle = 90f - angle;
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
        Vector2f from;
        Vector2f to;
        RectangleShape line;

        public Connection(ref Neuron from, ref Neuron to, float width)
        {
            this.from = from.CenterPoint;
            this.to = to.CenterPoint;
            line = ConnectionUtility.GenerateLine(ref this.from, ref this.to, width);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            //RectangleShape line = ConnectionUtility.GenerateLine(ref from, ref to);
            line.FillColor = Color.Cyan;
            target.Draw(line, RenderStates.Default);
        }
    }
}

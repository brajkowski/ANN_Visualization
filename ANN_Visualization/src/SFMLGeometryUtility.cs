using System;
using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
{
    public static class SFMLGeometryUtility
    {
        // Generates a line between any two points.
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

        // Aligns two circles so that their center points are the same.
        public static void AlignCircles(ref CircleShape outer, ref CircleShape inner)
        {
            inner.Position = outer.Position;
            float adjustment = outer.Radius - inner.Radius;
            float newX = inner.Position.X + adjustment;
            float newY = inner.Position.Y + adjustment;
            inner.Position = new Vector2f(newX, newY);
        }

        // Calculates the center point of a circle.
        public static Vector2f CalculateCenter(ref CircleShape circle)
        {
            float x = circle.Position.X + circle.Radius;
            float y = circle.Position.Y + circle.Radius;
            return new Vector2f(x, y);
        }
    }
}

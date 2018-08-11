using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
{
    class DigitImage : Drawable
    {
        public Image image;
        public RectangleShape imageHolder;

        public DigitImage()
        {
            image = new Image(28, 28);
            imageHolder = new RectangleShape(new Vector2f(28f, 28f));
            imageHolder.Position = new Vector2f(50f, 50f);
        }

        public void Draw(RenderTarget target, RenderStates s)
        {
            imageHolder.Draw(target, RenderStates.Default);
        }

        public void Update(float[] pixels, int label)
        {
            uint x = 0;
            uint y = 0;
            foreach (float pixel in pixels)
            {
                byte sat = (byte)(pixel * 255f);
                image.SetPixel(x, y, new Color(sat, sat, sat));
                if (y == image.Size.Y)
                {
                    y++;
                    x = 0;
                    continue;
                }
                x++;
            }
            imageHolder.Texture = new Texture(image);
        }
    }
}

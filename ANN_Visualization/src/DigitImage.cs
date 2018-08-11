using SFML.Graphics;
using SFML.System;
using System;

namespace ANN_Visualization.src
{
    class DigitImage : Drawable
    {
        public Image image;
        public RectangleShape imageHolder;
        public int label;
        public int predicting;
        public Font labelFont;

        public DigitImage()
        {
            image = new Image(28, 28);
            imageHolder = new RectangleShape(new Vector2f(28f, 28f));
            imageHolder.Position = new Vector2f(50f, 50f);
            label = 0;
            predicting = 0;
            labelFont = new Font("C:/WINDOWS/FONTS/ARIAL.TTF");
        }

        public void Draw(RenderTarget target, RenderStates s)
        {
            GUI gui = (GUI)(target);
            Vector2f imageCoords = gui.MapPixelToCoords(new Vector2i(0, 0));
            imageHolder.Position = imageCoords;
            float imageScaleAdjust = (float)(Math.Pow(gui.ZoomRate, -gui.ZoomState)) * 1.5f;
            imageHolder.Scale = new Vector2f(imageScaleAdjust,imageScaleAdjust);
            imageHolder.Draw(target, RenderStates.Default);

            Text labelText = new Text("Label: " + label.ToString() + "\n" + "Predicting: " + predicting, labelFont,10);
            Vector2f labelCoords = gui.MapPixelToCoords(new Vector2i(0, 40));
            labelText.Position = labelCoords;
            float labelScaleAdjust = (float)(Math.Pow(gui.ZoomRate, -gui.ZoomState))*0.5f;
            labelText.Scale = new Vector2f(imageScaleAdjust, imageScaleAdjust);
            labelText.Draw(target, RenderStates.Default);
        }

        public void Update(float[] pixels, int label, int predicting)
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
            this.label = label;
            this.predicting = predicting;
        }
    }
}

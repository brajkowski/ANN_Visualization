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
            image = new Image(28, 28);                                  // Size of mnist images.
            imageHolder = new RectangleShape(new Vector2f(28f, 28f));   //
            label = 0;
            predicting = 0;
            labelFont = new Font("C:/WINDOWS/FONTS/ARIAL.TTF");
        }

        public void Draw(RenderTarget target, RenderStates s)
        {
            // Hack to keep mnist digit appearing in top left corner of screen.
            // TODO: Utilize viewports to create a view port for mnist image/label.
            GUI gui = (GUI)(target);
            Vector2f imageCoords = gui.MapPixelToCoords(new Vector2i(0, 0));                    // Ensures top left of screen.
            imageHolder.Position = imageCoords;
            float imageScaleAdjust = (float)(Math.Pow(gui.ZoomFactor, -gui.ZoomState)) * 1.5f;  // 1.5 came from visual trial/error.
            imageHolder.Scale = new Vector2f(imageScaleAdjust,imageScaleAdjust);
            target.Draw(imageHolder, RenderStates.Default);

            // Hack to keep textbox below the mnist image.
            // TODO: Utilize viewports to create a view port for mnist image/label.
            Text labelText = new Text("Label: " + label.ToString() + "\n" + "Predicting: " + predicting, labelFont,15);
            Vector2f labelCoords = gui.MapPixelToCoords(new Vector2i(0, 40));                   // Ensures text box is below image.
            labelText.Position = labelCoords;
            float labelScaleAdjust = (float)(Math.Pow(gui.ZoomFactor, -gui.ZoomState))*0.5f;    // 0.5 came from visual trial/error.
            labelText.Scale = new Vector2f(imageScaleAdjust, imageScaleAdjust);
            target.Draw(labelText, RenderStates.Default);
        }

        public void Update(float[] pixels, int label, int predicting)
        {
            uint x = 0; // Used to properly orient pixels from mnist pixel list into 2x2 array.
            uint y = 0; //
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

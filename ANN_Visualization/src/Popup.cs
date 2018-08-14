using SFML.Graphics;
using SFML.System;

namespace ANN_Visualization.src
{
    class Popup : Drawable
    {
        RectangleShape background;
        Text text;

        public Popup(Vector2f position)
        {
            background = new RectangleShape()
            {
                FillColor = new Color(30, 30, 30, 200),     //
                OutlineColor = new Color(30, 30, 30, 200),  // Create a grey background with 2px padding for text.
                OutlineThickness = 2f,                      //
            };
            background.Position = position;

            var font = new Font("C:/WINDOWS/FONTS/ARIAL.TTF");
            text = new Text("No string to display", font, 20)
            {
                Position = position,
                Scale = new Vector2f(0.3f, 0.3f),
            };
        }

        public void UpdateText(string newText)
        {
            text.DisplayedString = newText;

            // Update background to fit all of the text.
            FloatRect textBounds = text.GetGlobalBounds();
            background.Position = new Vector2f(textBounds.Left, textBounds.Top);
            background.Size = new Vector2f(textBounds.Width, textBounds.Height);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(background, RenderStates.Default);
            target.Draw(text, RenderStates.Default);
        }
    }
}

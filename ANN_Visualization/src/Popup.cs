using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ANN_Visualization.src
{
    class Popup : Drawable
    {
        RectangleShape background;
        Text text;
        //string displayedText;

        public Popup(Vector2f position)
        {
            background = new RectangleShape()
            {
                FillColor = new Color(30, 30, 30, 200),
                OutlineColor = new Color(30, 30, 30, 200),
                OutlineThickness = 2f,
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

            FloatRect textBounds = text.GetGlobalBounds();
            background.Position = new Vector2f(textBounds.Left, textBounds.Top);
            background.Size = new Vector2f(textBounds.Width, textBounds.Height);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {


            target.Draw(background, RenderStates.Default);
            target.Draw(text);
        }
    }
}

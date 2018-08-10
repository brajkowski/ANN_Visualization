using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Collections.Generic;
using System.Threading;

namespace ANN_Visualization
{
    class Program
    {
        static void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            var window = (RenderWindow)(sender);
            View newView = window.GetView();
            if (e.Delta > 0)
                newView.Zoom(0.5f);
            else
                newView.Zoom(2f);
            window.SetView(newView);
        }

        static void OnClose(object sender, EventArgs e)
        {
            var window = (RenderWindow)(sender);
            window.Close();
        }

        static void OnResize(object sender, SizeEventArgs e)
        {
            var window = (RenderWindow)(sender);
            var viewRectangle = new FloatRect(0f, 0f, e.Width, e.Height);
            window.SetView(new View(viewRectangle));
        }

        static void OnMouseButtonPress(object sender, MouseButtonEventArgs e)
        {
            var window = (RenderWindow)(sender);
            if (e.Button == Mouse.Button.Middle)
            {
                int originX = e.X;
                int originY = e.Y;
                int mouseX;
                int mouseY;
                float newX;
                float newY;
                View newView;
                // TODO: Add multithreading to keep GUI updating.
                while (Mouse.IsButtonPressed(Mouse.Button.Middle))
                {
                    mouseX = Mouse.GetPosition(window).X;
                    mouseY = Mouse.GetPosition(window).Y;
                    newX = -(mouseX - originX);
                    newY = -(mouseY - originY);
                    newView = window.GetView();
                    newView.Move(new Vector2f(newX, newY));
                    window.SetView(newView);
                    originX = mouseX;
                    originY = mouseY;
                }
            }            
        }

        static void Main()
        {
            var window = new RenderWindow(new VideoMode(500, 500), "Test window");
            window.Closed += new EventHandler(OnClose);
            window.Resized += new EventHandler<SizeEventArgs>(OnResize);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseButtonPress);
            window.MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(OnMouseScroll);
            window.SetFramerateLimit(60);
            var neuron = new Neuron(new Vector2f(250f, 250f), 20f);
            neuron.ChangeColor(255);
            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();
                foreach (Drawable d in neuron.Drawables)
                {
                    window.Draw(d);
                }
                window.Display();
            }
        }
    }
}

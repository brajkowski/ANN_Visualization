﻿using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Threading;

namespace ANN_Visualization.src
{
    public static class MainWindowEvents
    {
        public static void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            var window = (MainWindow)(sender);
            View newView = window.GetView();
            if (e.Delta > 0)
            {
                newView.Zoom(1f/window.ZoomFactor);
                window.ZoomState += 1;
            }
            else
            {
                newView.Zoom(window.ZoomFactor);
                window.ZoomState -= 1;
            }
            window.SetView(newView);
        }

        public static void OnClose(object sender, EventArgs e)
        {
            var window = (MainWindow)(sender);
            window.Close();
        }

        public static void OnResize(object sender, SizeEventArgs e)
        {
            var window = (MainWindow)(sender);
            var viewRectangle = new FloatRect(0f, 0f, e.Width, e.Height);
            window.SetView(new View(viewRectangle));
            window.ZoomState = 0;
        }

        public static void OnMouseButtonPress(object sender, MouseButtonEventArgs e)
        {
            var window = (MainWindow)(sender);
            if (e.Button == Mouse.Button.Middle)
            {
                // Multithreaded pan to keep ui responsive.
                var t = new Thread(new ThreadStart(() => window.ConstantPan(e.X,e.Y)));
                t.Start();
            }
        }
    }

    public class MainWindow : RenderWindow
    {
        public int ZoomState { get; set; }
        public float ZoomFactor { get; set; }

        public MainWindow(uint width, uint height, string title) : base(new VideoMode(width, height), title)
        {
            ZoomState = 0;
            ZoomFactor = 2f;
            SetFramerateLimit(60);
            Closed += new EventHandler(MainWindowEvents.OnClose);
            Resized += new EventHandler<SizeEventArgs>(MainWindowEvents.OnResize);
            MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(MainWindowEvents.OnMouseButtonPress);
            MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(MainWindowEvents.OnMouseScroll);
        }

        public void ConstantPan(int originX, int originY)
        {
            int mouseX;
            int mouseY;
            float newX;
            float newY;
            View newView;
            while (Mouse.IsButtonPressed(Mouse.Button.Middle))
            {
                mouseX = Mouse.GetPosition(this).X;
                mouseY = Mouse.GetPosition(this).Y;
                newX = -(mouseX - originX) * (float)Math.Pow(ZoomFactor, -ZoomState);   // Undo zooming to get constant panning at all zoom states.
                newY = -(mouseY - originY) * (float)Math.Pow(ZoomFactor, -ZoomState);   //
                newView = GetView();
                newView.Move(new Vector2f(newX, newY));
                SetView(newView);
                originX = mouseX;
                originY = mouseY;
            }
        }
    }
}

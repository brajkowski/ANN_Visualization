﻿using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.System;

namespace ANN_Visualization.src
{
    public static class GUIEvents
    {
        public static void OnKeyPress(object sender, KeyEventArgs e)
        {
            GUI gui = (GUI)(sender);
            switch (e.Code)
            {
                case Keyboard.Key.Left:
                    gui.Visualizer.VisualizePrevious(ref gui.Neurons, ref gui.Connections);
                    gui.Visualizer.VisualizeDigit(ref gui.digitImage);
                    break;

                case Keyboard.Key.Right:
                    gui.Visualizer.VisualizeNext(ref gui.Neurons, ref gui.Connections);
                    gui.Visualizer.VisualizeDigit(ref gui.digitImage);
                    break;

                case Keyboard.Key.Up:
                    gui.Visualizer.IncreaseOpacityFactor(ref gui.Neurons, ref gui.Connections);
                    break;

                case Keyboard.Key.Down:
                    gui.Visualizer.DecreaseOpacityFactor(ref gui.Neurons, ref gui.Connections);
                    break;

                case Keyboard.Key.A:
                    foreach (Neuron n in gui.Neurons)
                    {
                        n.ShouldBeDrawn = true;
                    }
                    break;

                case Keyboard.Key.R:
                    gui.Visualizer.connectionViewState = Connections.Negative;
                    gui.Visualizer.Visualize(ref gui.Neurons, ref gui.Connections);
                    foreach (Connection c in gui.Connections)
                    {
                        if (c.weight >= 0)
                        {
                            c.Dampen();
                        }
                    }
                    break;

                case Keyboard.Key.G:
                    gui.Visualizer.connectionViewState = Connections.Positive;
                    gui.Visualizer.Visualize(ref gui.Neurons, ref gui.Connections);
                    foreach (Connection c in gui.Connections)
                    {
                        if (c.weight < 0)
                        {
                            c.Dampen();
                        }
                    }
                    break;

                case Keyboard.Key.B:
                    gui.Visualizer.connectionViewState = Connections.All;
                    gui.Visualizer.Visualize(ref gui.Neurons, ref gui.Connections);
                    break;

                case Keyboard.Key.C:
                    foreach (Neuron n in gui.Neurons)
                    {
                        n.ShouldDrawBiasPopup = false;
                    }
                    break;
            }
        }
        public static void OnMouseButtonPress(object sender, MouseButtonEventArgs e)
        {
            GUI gui = (GUI)(sender);
            Vector2f coords = gui.MapPixelToCoords(new Vector2i(e.X, e.Y),gui.GetView());
            switch (e.Button)
            {
                case Mouse.Button.Right:
                    foreach (Neuron n in gui.Neurons)
                    {
                        if (n.outer.GetGlobalBounds().Contains(coords.X, coords.Y))
                        {
                            n.ShouldBeDrawn = false;
                            n.ShouldDrawBiasPopup = false;
                            return;
                        }
                    }
                    break;

                case Mouse.Button.Left:
                    foreach (Neuron n in gui.Neurons)
                    {
                        if (n.outer.GetGlobalBounds().Contains(coords.X, coords.Y))
                        {
                            n.ShouldDrawBiasPopup = !n.ShouldDrawBiasPopup;
                            return;
                        }
                    }
                    break;

            }
        }
    }

    class GUI : MainWindow
    {
        public DigitImage digitImage;
        public List<Connection> Connections;
        public List<Neuron> Neurons;
        public NetworkVisualizer Visualizer;
        public int Height;
        public int Width;
          
        public GUI(uint width, uint height, string title) : base(width, height, title)
        {
            Width = (int)width;
            Height = (int)height;
            Neurons = new List<Neuron>();
            Connections = new List<Connection>();
            Visualizer = new NetworkVisualizer();
            KeyPressed += new EventHandler<KeyEventArgs>(GUIEvents.OnKeyPress);
            MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(GUIEvents.OnMouseButtonPress);
            Neurons = Visualizer.GenerateVisualNeurons(3f, Height, Width);
            Connections = Visualizer.GenerateVisualConnections(0.5f, ref Neurons);
            digitImage = new DigitImage();
            Visualizer.Visualize(ref Neurons, ref Connections);
            Visualizer.VisualizeDigit(ref digitImage);
        }

        public void Run()
        {
            while (IsOpen)
            {
                Clear();
                DispatchEvents();
                foreach (Connection c in Connections)
                {
                    Draw(c);
                }
                foreach (Neuron n in Neurons)
                {
                    Draw(n);
                }
                Draw(digitImage);
                Display();
            }
        }
    }
}

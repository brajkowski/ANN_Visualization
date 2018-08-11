using System;
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
            if (e.Code == Keyboard.Key.Left)
            {
                gui.Visualizer.VisualizePrevious(ref gui.Neurons, ref gui.Connections);
            }
            else if(e.Code == Keyboard.Key.Right)
            {
                gui.Visualizer.VisualizeNext(ref gui.Neurons,ref gui.Connections);
            }
            else if(e.Code == Keyboard.Key.Up)
            {
                gui.Visualizer.IncreaseOpacityFactor(ref gui.Neurons, ref gui.Connections);
            }
            else if (e.Code == Keyboard.Key.Down)
            {
                gui.Visualizer.DecreaseOpacityFactor(ref gui.Neurons, ref gui.Connections);
            }
            else if (e.Code == Keyboard.Key.A)
            {
                foreach (Neuron n in gui.Neurons)
                {
                    n.ShouldBeDrawn = true;
                }
            }
        }
        public static void OnMouseButtonPress(object sender, MouseButtonEventArgs e)
        {
            GUI gui = (GUI)(sender);
            Vector2f coords = gui.MapPixelToCoords(new Vector2i(e.X, e.Y),gui.GetView());
            if (e.Button == Mouse.Button.Right)
            {
                foreach (Neuron n in gui.Neurons)
                {
                    if (n.outer.GetGlobalBounds().Contains(coords.X,coords.Y))
                    {
                        n.ShouldBeDrawn = false;
                        return;
                    }
                }
            }
        }
    }

    class GUI : MainWindow
    {
        public List<Connection> Connections;
        public List<Neuron> Neurons;
        public NetworkVisualizer Visualizer;
        public int Height { get; }
        public int Width { get; }
        
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
            Visualizer.Visualize(ref Neurons, ref Connections);
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
                Display();
            }
        }
    }
}

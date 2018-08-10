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
    public static class GUIEvents
    {
        public static void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left)
            {
                GUI gui = (GUI)sender;
                foreach (Neuron n in gui.Neurons)
                {
                    n.ChangeActivation(0.5f);
                }
            }
            if(e.Code == Keyboard.Key.Right)
            {
                Console.WriteLine("Right Key");
                GUI gui = (GUI)sender;
                gui.visualizer.Visualize();
            }
        }
    }

    class GUI : MainWindow
    {
        public List<Connection> Connections;
        public List<Neuron> Neurons;
        public NetworkVisualizer visualizer;
        //public MainWindow window;
        
        public GUI(uint width, uint height, string title) : base(width, height, title)
        {
            //window = new MainWindow(width, height, title);
            Neurons = new List<Neuron>();
            Connections = new List<Connection>();
            visualizer = new NetworkVisualizer();
            KeyPressed += new EventHandler<KeyEventArgs>(GUIEvents.OnKeyPress);
            
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

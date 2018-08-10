using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ANN_Visualization
{
    class GUI
    {
        public List<Connection> Connections;
        public List<Neuron> Neurons;
        public MainWindow window;
        
        public GUI(uint width, uint height, string title)
        {
            window = new MainWindow(width, height, title);
            Neurons = new List<Neuron>();
            Connections = new List<Connection>();
        }

        public void Run()
        {
            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();
                foreach (Connection c in Connections)
                {
                    c.Draw(ref window);
                }
                foreach (Neuron n in Neurons)
                {
                    n.Draw(ref window);
                }
                window.Display();
            }
        }
    }
}

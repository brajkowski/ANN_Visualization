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
    public static class GUIEvents
    {
        public static void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left)
            {
                GUI gui = (GUI)sender;
                foreach (Neuron n in gui.Neurons)
                {
                    n.ChangeActivation(.8f);
                }
            }
        }
    }

    class GUI : MainWindow
    {
        public List<Connection> Connections;
        public List<Neuron> Neurons;
        //public MainWindow window;
        
        public GUI(uint width, uint height, string title) : base(width, height, title)
        {
            //window = new MainWindow(width, height, title);
            Neurons = new List<Neuron>();
            Connections = new List<Connection>();
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

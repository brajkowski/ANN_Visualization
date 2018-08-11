using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Collections.Generic;
using System.Threading;

namespace ANN_Visualization.src
{
    class Program
    { 
        static void Main()
        {
            var gui = new GUI(500,500, "Test window");
            //var neuron = new Neuron(new Vector2f(250f, 250f), 20f);
            //var neuron2 = new Neuron(new Vector2f(100f, 300f), 40f);
            //gui.Neurons.Add(neuron);
            //gui.Neurons.Add(neuron2);
            //Connection test = new Connection(neuron2, neuron, 3f);
            //gui.Connections.Add(test);
            gui.Run();
        }
    }
}

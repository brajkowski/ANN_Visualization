using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN_Visualization.src
{
    class NetworkVisualizer
    {

        public NeuralNetworkCS.MnistData mnistData;
        public NeuralNetworkCS.Network network;
        public int currentImage;

        public NetworkVisualizer()
        {
            mnistData = new NeuralNetworkCS.MnistData();
            mnistData.LoadAll();
            List<int> sizes = NeuralNetworkCS.NetworkUtility.SizeSavedNetwork(@"NetworkData/network.dat");
            network = new NeuralNetworkCS.Network(sizes,NeuralNetworkCS.Activation.Sigmoid);
            network.LoadNetwork(@"NetworkData/network.dat");
            currentImage = 0;
        }

        public void Visualize()
        {
            Console.WriteLine("Visualizing image {0}", currentImage);
            network.SetInputLayer(mnistData.TrainImages.Column(currentImage));
            network.FeedForward();
            currentImage++;
        }
    }
}

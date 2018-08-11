using System;
using System.Collections.Generic;

namespace ANN_Visualization.src
{
    enum Connections { All = 0, Positive = 1, Negative = 2 };
    class NetworkVisualizer
    {

        public NeuralNetworkCS.MnistData mnistData;
        public NeuralNetworkCS.Network network;
        public List<int> networkSize;
        public int currentImage;
        public float connectionOpacityFactor;
        public float maxConnectionOpacityFactor;
        public float minConnectionOpacityFactor;
        public Connections connectionViewState;

        public NetworkVisualizer()
        {
            mnistData = new NeuralNetworkCS.MnistData();
            mnistData.LoadAll();
            networkSize = NeuralNetworkCS.NetworkUtility.SizeSavedNetwork(@"NetworkData/network.dat");
            network = new NeuralNetworkCS.Network(networkSize,NeuralNetworkCS.Activation.Sigmoid);
            network.LoadNetwork(@"NetworkData/network.dat");
            currentImage = 0;
            connectionOpacityFactor = 10f;
            maxConnectionOpacityFactor = 100f;
            minConnectionOpacityFactor = 1f;
            connectionViewState = Connections.All;
        }

        public List<Neuron> GenerateVisualNeurons(float radius, int height, int width)
        {
            float vertSpacing = 4f * radius;
            float horizSpacing = width / networkSize.Count;
            var neuronList = new List<Neuron>();
            float x = horizSpacing;
            float y = 0f;
            float prevCenter = networkSize[1] * vertSpacing / 2f;
            float nextCenter = 0f;
            for (int i = 1; i < networkSize.Count; i++)
            {
                nextCenter = networkSize[i] * vertSpacing / 2f;
                y = prevCenter - nextCenter;
                for(int j = 0; j < networkSize[i]; j++)
                {
                    neuronList.Add(new Neuron(new SFML.System.Vector2f(x,y), radius));
                    y += vertSpacing;
                }
                prevCenter = y / 2f;
                x += horizSpacing;
            }
            return neuronList;
        }

        public List<Connection> GenerateVisualConnections(float width, ref List<Neuron> neurons)
        {
            var connectionList = new List<Connection>();
            int neuronCount = 0;
            int toNeuronOffset = 0;
            for (int column = 1; column < networkSize.Count - 1; column++)
            {
                toNeuronOffset += networkSize[column];
                for (int current = 0; current < networkSize[column]; current++)
                {
                    for (int next = 0; next < networkSize[column + 1]; next++)
                    {
                        connectionList.Add(new Connection(neurons[neuronCount], neurons[toNeuronOffset + next], width));
                    }
                    neuronCount++;
                }
            }
            return connectionList;
        }

        public void Visualize(ref List<Neuron> neurons, ref List<Connection> connections)
        {
            network.SetInputLayer(mnistData.TestImages.Column(currentImage));
            network.FeedForward();

            List<float> activations = network.GetHiddenActivations();
            List<float> weights = network.GetHiddenWeights();
            if (activations.Count != neurons.Count)
            {
                Console.WriteLine("Visualizer Error: neuron/activation size arent equal");
                return;
            }
            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].ChangeActivation(activations[i]);
            }

            if (weights.Count != connections.Count)
            {
                Console.WriteLine("Visualizer Error: connection/weight size arent equal");
                return;
            }
            
            for (int i = 0; i < connections.Count; i++)
            {
                if (connectionViewState == Connections.Negative && weights[i] >= 0)
                {
                    continue;
                }
                if (connectionViewState == Connections.Positive && weights[i] < 0)
                {
                    continue;
                }
                connections[i].ChangeWeight(weights[i], connectionOpacityFactor);
            }
        }

        public void VisualizePrevious(ref List<Neuron> neurons, ref List<Connection> connections)
        {
            if (currentImage == 0)
            {
                Console.WriteLine("Visualizer Error: At the first image");
                return;
            }
            currentImage--;
            Console.WriteLine("Visualizer: Visualizing image {0} with label {1}", currentImage, mnistData.TestLabels[currentImage]);
            Visualize(ref neurons, ref connections);
        }

        public void VisualizeNext(ref List<Neuron> neurons, ref List<Connection> connections)
        {
            if (currentImage == mnistData.TestLabels.Count - 1)
            {
                Console.WriteLine("Visualizer Error: At the last image");
                return;
            }
            currentImage++;
            Console.WriteLine("Visualizer: Visualizing image {0} with label {1}", currentImage, mnistData.TestLabels[currentImage]);
            Visualize(ref neurons, ref connections);
        }

        public void IncreaseOpacityFactor(ref List<Neuron> neurons, ref List<Connection> connections)
        {
            if (connectionOpacityFactor == maxConnectionOpacityFactor)
            {
                Console.WriteLine("Visualizer: At max opacity");
                return;
            }
            connectionOpacityFactor = connectionOpacityFactor + 1f;
            Visualize(ref neurons, ref connections);
        }

        public void DecreaseOpacityFactor(ref List<Neuron> neurons, ref List<Connection> connections)
        {
            if (connectionOpacityFactor == minConnectionOpacityFactor)
            {
                Console.WriteLine("Visualizer: At min opacity");
                return;
            }
            connectionOpacityFactor = connectionOpacityFactor - 1f;
            Visualize(ref neurons, ref connections);
        }

        public void VisualizeDigit(ref DigitImage digitImage)
        { 
            float[] pixels = mnistData.TestImages.Column(currentImage).ToArray();
            int label = (int)(mnistData.TestLabels[currentImage]);
            int predicting = network.mNeurons[network.lastLayerIndex].MaximumIndex();
           
            digitImage.Update(pixels,label, predicting);
        }
    }
}

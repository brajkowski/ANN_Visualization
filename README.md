# ANN Visualization

## Description
A proof-of-concept, interactive visualization tool that allows for the exploration of deep artificial neural networks (ANN's).  This tool loads a pre-trained ANN capable of identifying handwritten digits (MNIST).  Users can flip through the MNIST testing set and visualize neuron activations, neuron biases, and the connection weights that form the network.

## System Requirements
- Supported Operating Systems
	- Windows 10 (x64)

- Minimum Hardware Requirements
	- 512 MB of RAM
	- 100 MB of available hard disk space

## Build Instructions
1. Clone repository and open solution in Visual Studio
2. Install MathNet.Numerics 4.5.1: ``PM> Install-Package MathNet.Numerics -Version 4.5.1``
3. Ensure build configuration is set to x64
4. Build in Visual Studio

## Visualization Controls
- Navigate through MNIST test images
	- Left and Right arrow keys

- Change connection opacity
	- Up and Down arrow keys

- Change connection view type
	- G: View only positive connections
	- R: View only negative connections
	- B: View all connections

- Filter neurons
	- Right click on neuron to hide
	- A: Unhide all hidden neurons

- Additional Information
	- Left click on neuron to toggle its bias value
	- C: Hide all bias values

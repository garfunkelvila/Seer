﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network {
    public class NeuronLayerGroup {
        public NeuronLayer[] NeuronLayers;
        public double[] Outputs;
        #region Constructors
        /// <summary>
        /// Holds an array of Neuron layers.
        /// </summary>
        /// <param name="nLayers"></param>
        public NeuronLayerGroup (NeuronLayer[] nLayers) {
            NeuronLayers = nLayers;
        }
        #endregion
        public double[] Decide (double[] Inputs) {
            double[] outputBuffer;
            for (int n = 0; n < NeuronLayers[0].neurons.Length; n++) {
                for (int d = 0; d < NeuronLayers[0].neurons[n].Dendrites.Length; d++) {
                    NeuronLayers[0].neurons[n].Dendrites[d] = Inputs[d];
                }
            }
            outputBuffer = new double[NeuronLayers[0].neurons.Length];
            for (int oB = 0; oB < outputBuffer.Length; oB++) {
                outputBuffer[oB] = NeuronLayers[0].neurons[oB].Axon();
            }

            //Get outputs then feed into next layer
            for (int nL = 1; nL < NeuronLayers.Length; nL++) {
                //My first time xD this thing is slow for low count
                //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.for?view=netframework-4.7.2#System_Threading_Tasks_Parallel_For_System_Int32_System_Int32_System_Action_System_Int32__
                Parallel.For(0, NeuronLayers[nL].neurons.Length, n => {
                    for (int d = 0; d < NeuronLayers[nL].neurons[n].Dendrites.Length; d++) {
                        NeuronLayers[nL].neurons[n].Dendrites[d] = outputBuffer[d];
                    }
                });
                outputBuffer = new double[NeuronLayers[nL].neurons.Length];
                //--------------------------------------------------------------------------
                Parallel.For(0, outputBuffer.Length, oB => {
                    outputBuffer[oB] = NeuronLayers[nL].neurons[oB].Axon();
                });
            }
            return outputBuffer;
        }
        /*public override int GetHashCode () {
            //Will be used to check if nlg have the same with other
        }*/
    }
}
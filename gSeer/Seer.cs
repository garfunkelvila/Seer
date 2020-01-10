﻿//  gSeer, a C# Artificial Neural Network Library
//  Copyright (C) 2018  Garfunkel Vila
//  
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 3 of the License, or any later version.
//  
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
//  
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library. If not,
//  see<https://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gSeer.Neuron;

namespace gSeer {
    public class Seer {
        public NeuronLayerGroup LayerGroups { get; private set; }

        /// <summary>
        /// This seer creates a fully connected network. If you want to create a custom one, you may want to use Neuron, NeuronLayer and NeuronLayergroup to create what you want
        /// </summary>
        /// <param name="inputCount"></param>
        /// <param name="outputCount"></param>
        /// <param name="numLayers"></param>
        public Seer (int inputCount, int outputCount, int numLayers) {
            NeuronLayer[] nL = new NeuronLayer[numLayers];
            // If single layer
            if (numLayers == 1) {
                nL[0] = new NeuronLayer(inputCount, outputCount);
                LayerGroups = new NeuronLayerGroup(nL);
                return;
            }
            // If multilayer
            int hn = (int)Math.Ceiling(outputCount * 1.1d); // 1.1d to make the hidden layer a bit fat
            nL[0] = new NeuronLayer(inputCount, hn);        // Add the first one
            for (int nli = 1; nli < numLayers - 1; nli++) { // Skip the last
                nL[nli] = new NeuronLayer(hn, hn);
            }
            nL[numLayers - 1] = new NeuronLayer(hn, outputCount);   // Add the last
            LayerGroups = new NeuronLayerGroup(nL);     // add the layers to the group
        }

        public float[] Predict (float[] Sensories) {
            return LayerGroups.Predict(Sensories);
            //This thing is supposed to raise an event when finished. But instead, I just directly accesed its field xD
            //Will add event feature soon
        }

        public void Train (TrainingData[] td, int iteration) {
            for (int i = 0; i < iteration; i++) {
                //This is a cpu intensive process, should be in a seperate thread
                BackPropagation bp = new BackPropagation();
                LayerGroups = bp.BackPropagate(LayerGroups, td);
            }
        }

        public float[] getError () {
            float[] _rBuffer = new float[LayerGroups.NeuronLayers[LayerGroups.NeuronLayers.Length - 1].neurons.Length];
            for (int n = 0; n < LayerGroups.NeuronLayers[LayerGroups.NeuronLayers.Length - 1].neurons.Length; n++) {
                _rBuffer[n] = LayerGroups.NeuronLayers[LayerGroups.NeuronLayers.Length - 1].neurons[n].Error;
            }
            return _rBuffer;
        }

        private NeuronLayerGroup MutateWith (NeuronLayerGroup nlg) {
            Genetics ga = new Genetics();
            return ga.Mutate(this.LayerGroups, nlg);
        }
        /// <summary>
        /// This will be used for while seer mutation
        /// </summary>
        /// <param name="seer"></param>
        /// <returns></returns>
        public Seer MutateWith (Seer seer) {
            throw new NotImplementedException();
        }
    }
}

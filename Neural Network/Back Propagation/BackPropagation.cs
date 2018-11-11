﻿//  Copyright (C) 2018  Garfunkel Vila
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Will come back here soon
//THIS THING IS FROM A YOUTUBE CHANNEL
//I don't know Calculus that well xD
//https://www.youtube.com/watch?v=tIeHLnjs5U8&t
//and
//https://www.youtube.com/watch?v=gQLKufQ35VE&list=PLxt59R_fWVzT9bDxA76AHm3ig0Gg9S3So&index=11
namespace Neural_Network {
    class BackPropagation : Activations {
        //Objective
        //Inputs: NeuronLayerGroup. If CNS is done, train with motor neuron, exclude internal/connecting output neuron
        //Output: Update NLG it directly

        NeuronLayerGroup nlg;
        NeuronLayerGroup nlgBuffer;
        //Temporaries
        TrainingData[] Targets;
        TrainingData Target;
        double sTarget;
        //-----------

        double learning_rate = 0.1;
        int TrainIteration = 50000;

        //Cost for 1 target for now (multiple neurons)
        //C[0] = Preditions[i] - Target.Targets[i] ^ 2
        double OutputCost (TrainingData trainingData) {
            double _rBuffer = 0;
            for (int i = 0; i < trainingData.Target.Length; i++) {
                _rBuffer += Math.Pow(trainingData.Target[i] - nlg.Prediction[i], 2);
            }
            return _rBuffer;
        }
        double avCost (TrainingData[] trainingData) {
            double _rBuffer = 0;
            for (int td = 0; td < trainingData.Length; td++) {
                for (int i = 0; i < trainingData[td].Target.Length; i++) {
                    _rBuffer += Math.Pow(trainingData[td].Target[i] - nlg.Prediction[i], 2);
                }
            }
            return _rBuffer / trainingData.Count();
        }
        
        //Dericative of Cost with respect to LayerOutput        C0 -> a(L)
        // 2 * (pred - target)
        double[] d_CostPred () {
            //The thing is this is being called per layer? for that I don't know what will be in the target
            double[] _rBuffer = new double[nlg.Prediction.Length];
            for (int i = 0; i < nlg.Prediction.Length; i++) {
                _rBuffer[i] = 2 * (Target.Target[i] - nlg.Prediction[i]);
            }
            return _rBuffer;
        }
        double[] d_CostPred (int layerIndex) {
            //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
            double[] _rBuffer = new double[nlg.NeuronLayers[layerIndex].Axons.Length];
            for (int i = 0; i < _rBuffer.Length; i++) {
                _rBuffer[i] = 2 * (Target.Target[i] - nlg.NeuronLayers[layerIndex].Axons[i]);
            }
            return _rBuffer;
        }

        void outputLayerBP () {
            //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
            double[] dCP = d_CostPred(nlg.NeuronLayers.Length); //Select the output index
            double d_Cost;
            int nl = nlg.NeuronLayers.Length; //Maybe -1

            for (int n = 0; n < nlg.NeuronLayers[nl].neurons.Length; n++) { //Neuron loop
                d_Cost = dCP[n] * LogisticPrime(nlg.NeuronLayers[nl].neurons[n].z) * nlg.NeuronLayers[nl].neurons[n].Bias;
                nlgBuffer.NeuronLayers[nl].neurons[n].Bias -= learning_rate * d_Cost;   //Bias -= learningRate * (dcost_dpred * dpred_dz * Input)

                for (int w = 0; w < nlg.NeuronLayers[nl].neurons[n].Weights.Length; w++) { //Weight loop
                    d_Cost = dCP[n] * LogisticPrime(nlg.NeuronLayers[nl].neurons[n].z) * nlg.NeuronLayers[nl].neurons[n].Dendrites[w];
                    nlgBuffer.NeuronLayers[nl].neurons[n].Weights[w] -= learning_rate * d_Cost;
                }
            }
        }
        void hiddenLayerBP () {
            //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
        }

        void UpdateLayerGroup () {

        }
    }
}

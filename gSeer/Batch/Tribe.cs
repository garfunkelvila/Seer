﻿//  ChaoticSeer, a C# Artificial Neural Network Library
//  Copyright (C) 2020  Garfunkel Vila
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

using gSeer.Data_Structures;
using gSeer.Genetic_Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gSeer.Util;

namespace gSeer.Batch {
	public abstract class Tribe {
		public GeneHashSet<ChaoticSeer> Species { get; private set; }
		public NeatCNS Neat { get; }
		public int MAX_POPULATION { get; }
		/// <summary>
		/// Create a batch of species filled with Genomes
		/// </summary>
		/// <param name="inputSize"></param>
		/// <param name="outputSize"></param>
		/// <param name="maxPopulation">Target population</param>
		/// <param name="maxNodes">Target nodes</param>
		public Tribe(int inputSize, int outputSize, int maxPopulation, int maxNodes = 10) {
			MAX_POPULATION = maxPopulation;
			Neat = new NeatCNS(inputSize, outputSize, maxNodes);
			Species = new GeneHashSet<ChaoticSeer>();
			for (int i = 0; i < maxPopulation; i++) {
				Species.Add(new ChaoticSeer(Neat) {
					Identity = i
				});
			}
		}
		[Obsolete("Not tested yet")]
		public Tribe(NeatCNS neat, int maxPopulation) {
			MAX_POPULATION = maxPopulation;
			Neat = neat;
			Species = new GeneHashSet<ChaoticSeer>();
			for (int i = 0; i < maxPopulation; i++) {
				Species.Add(new ChaoticSeer(Neat) {
					Identity = i
				});
			}
		}
		#region EVOLUTION
		/// <summary>
		/// Natural Selection
		/// </summary>
		public abstract void Purge();
		/// <summary>
		/// Reproduce by mating with others
		/// </summary>
		public abstract void Reproduce();
		/// <summary>
		/// Mutate self, like self evolve
		/// </summary>
		public abstract void Mutate();
		#endregion
		public abstract void Train(TrainingData td);
		#region DECISIONS
		/// <summary>
		/// Fill their score based on their predictions
		/// </summary>
		/// <param name="td"></param>
		public abstract void Evaluate(TrainingData[] td);

		public abstract void Evaluate(TrainingDatas td);

		public abstract float[][] Decisions();
		#endregion
	}
}
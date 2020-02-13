﻿using gSeer.Data_Structures;
using gSeer.Genetic_Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gSeer {
	/// <summary>
	/// Might rename this to Tribe
	/// </summary>
	public class Tribe {
		public GeneHashSet<ChaoticSeer> _Species;
		public ChaoticSeer Representative;
		/// <summary>
		/// Create a batch of species filled with Genomes
		/// </summary>
		/// <param name="inputSize"></param>
		/// <param name="outputSize"></param>
		public Tribe(int inputSize, int outputSize, int maxPopulation) {
			_Species = new GeneHashSet<ChaoticSeer>();
			for (int i = 0; i < maxPopulation; i++) {
				ChaoticSeer _seer;
				_seer = new ChaoticSeer(inputSize, outputSize);
				_Species.Add(_seer);
			}
			Representative = _Species[0];
		}
		/// <summary>
		/// Create a batch of species filled with Genomes
		/// </summary>
		public Tribe(NeatCNS cns, int maxPopulation) {
			_Species = new GeneHashSet<ChaoticSeer>();
			for (int i = 0; i < maxPopulation; i++) {
				_Species.Add(new ChaoticSeer(cns));
			}
			Representative = _Species[0];
		}
		#region EVOLUTION
		public void Evolve() {
			//Populate();	// Base of natural selection // Create population
			Kill();	//Basically like going extinct for now // Base of natural selection
			//RemoveExtinct();
			Reproduce();	// Load up population by mating. Spread on some passing of genes
			Mutate();		// Evolve each species internally // Mutated babies
			Calculate();    // Get their predictions, and probably 
			Evaluate();		// set their scores
		}
		/// <summary>
		/// Natural Selection
		/// </summary>
		private void Kill() {
			//TODO: add somehting to make the fittest survive
			for (int i = 0; i < _Species.Count; i++) {
				if(_Species[i].SURVIVAL_THRESHOLD > Util.GetRngF()) {
					_Species.RemoveAt(i);
				}
			}
		}
		/// <summary>
		/// Reproduce by mating with others
		/// </summary>
		private void Reproduce() {
			//Use mutate with to fill population
			//GeneHashSet<ChaoticSeer> selector = _Species;

			// Mutate with 10 times for now
			// TODO: add something to prevent mutation with self
			for (int i = 0; i < 10; i++) {
				_Species[i].MateWith(_Species.Random);
			}
		}
		/// <summary>
		/// Mutate self, like self evolve
		/// </summary>
		private void Mutate() {
			foreach (ChaoticSeer seer in _Species) {
				seer.Mutate();
			}
		}
		private void Calculate() {
			throw new NotImplementedException();
		}
		private void Evaluate() {
			// Basically just give them their score
			throw new NotImplementedException();
		}
		#endregion
	}
}
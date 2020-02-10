﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gSeer.Data_Structures;
namespace gSeer.Genetic_Algorithm {
    /// <summary>
    /// Might rename this to country
    /// </summary>
    public class Species {
        public GeneHashSet<Client> Clients { get; private set; } = new GeneHashSet<Client>();
        public Client Representative { get; private set; }
        public float Score { get; private set; }

        public Species(Client representative) {
            Representative = representative;
            Representative.Species = this;
            Clients.Add(representative);
        }

        public bool Put(Client client) {
            if (client.Distance(Representative) < Representative.Genome.Neat.CP) {
                client.Species = this;
                Clients.Add(client);
                return true;
            }
            return false;
        }
        public void ForcePut(Client client) {
            client.Species = this;
            Clients.Add(client);
        }
        /// <summary>
        /// Make the specie extinct
        /// </summary>
        public void GoExtinct() {
            foreach (Client item in Clients.Data) {
                item.Species = null;
            }
        }
        public void EvalueateScore() {
            float y = 0;
            foreach (Client item in Clients.Data) {
                y += item.Score;
            }
            Score = y / Clients.Count;
        }

        public void Reset() {
            Representative = Clients.Random;
            foreach (Client item in Clients.Data) {
                item.Species = null;
            }
            Clients.Clear();
            Clients.Add(Representative);
            Representative.Species = this;
            Score = 0;
        }

        public void Kill(float percentage) {
            Clients.Data.Sort();

            float ammount = percentage * Clients.Count;
            for (int i = 0; i < ammount; i++) {
                // This flow assumes the top is the crappiest, will fix soon
                // TODO: fix sorting of this thing
                Clients[0].Species = null;
                Clients.RemoveAt(0);
            }
        }
        /// <summary>
        /// Create offspring.
        /// Create random client and breed
        /// </summary>
        /// <returns></returns>
        public ChaoticSeer Breed() {
            Client C1 = Clients.Random;
            Client C2 = Clients.Random;

            if (C1.Score > C2.Score)
                return ChaoticSeer.CrossOver(C1.Genome,C2.Genome);
            return ChaoticSeer.CrossOver(C2.Genome, C1.Genome);
        }
    }
}
﻿using Chaotic_Seer.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chaotic_Seer.NEAT {
	class NodeGene : INode{
		public int Innovation { get; set; } // Even this thing is in list, it is used for hashing
        public NeuronTypes Type { get; set; }

        public NodeGene(NodeGene neuron) {
			Innovation = neuron.Innovation;
			Type = neuron.Type;
		}
		public override bool Equals(object obj) {
			if (!GetType().Equals(obj.GetType())) return false;
			NodeGene other = obj as NodeGene;
			return
				Innovation == other.Innovation &&
				Type == other.Type;
		}

		public override int GetHashCode() {
			return Innovation;
		}
	}
}
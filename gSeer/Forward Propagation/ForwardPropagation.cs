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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gSeer.Data_Structures;
using gSeer.GeneticAlgorithm;

namespace gSeer.Forward_Propagation {
	public abstract class ForwardPropagation {
		public abstract List<CalcNode> InputNodes { get; set; }
		public abstract List<CalcNode> HiddenNodes { get; set; }
		public abstract List<CalcNode> OutputNodes { get; set; }
		public abstract float[] Output(params float[] input);
	}
}

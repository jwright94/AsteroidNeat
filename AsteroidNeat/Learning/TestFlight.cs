using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Worlds;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using TicTacToeEvolution.Learning;

namespace AsteroidNeat.Learning
{
    class TestFlight : SimpleNeatExperiment
    {
        public override IPhenomeEvaluator<IBlackBox> PhenomeEvaluator => new ShipEvaluator();
        public override int InputCount => 5 + 8;
        public override int OutputCount => 3;
        public override bool EvaluateParents => true;
    }
}

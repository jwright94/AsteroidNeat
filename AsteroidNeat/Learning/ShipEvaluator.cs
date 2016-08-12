using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AsteroidNeat.Worlds;
using SharpNeat.Core;
using SharpNeat.Phenomes;

namespace AsteroidNeat.Learning
{
    class ShipEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        public FitnessInfo Evaluate(IBlackBox phenome)
        {
            double compositeScore = 0;
            int trials = 3;

            for (int trial = 0; trial < trials; trial++)
            {
                GameplayWorld gameplayWorld = new GameplayWorld()
                {
                    Width = 800,
                    Height = 800
                };

                gameplayWorld.IsBackground = true;

                gameplayWorld.InitializeAI(phenome);

                while (!gameplayWorld.GameOver)
                {
                    // Simulate game at 30fps
                    gameplayWorld.Update(1f/15f);

                    // Sleep so my CPU doesn't catch fire
                    //Thread.Sleep(30);
                }

                compositeScore += gameplayWorld.Score / 1000000.0;
            }

            compositeScore /= (double) trials;

            return new FitnessInfo(compositeScore, compositeScore);
        }

        public void Reset()
        {

        }

        public ulong EvaluationCount { get; }
        public bool StopConditionSatisfied => false;
    }
}

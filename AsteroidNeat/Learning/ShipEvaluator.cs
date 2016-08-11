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

            for (int trial = 0; trial < 3; trial++)
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
                    gameplayWorld.Update(1f/30f);

                    // Sleep so my CPU doesn't catch fire
                    //Thread.Sleep(30);
                }

                compositeScore += gameplayWorld.Score / 1000000.0;
            }
            return new FitnessInfo(compositeScore, compositeScore);
        }

        public void Reset()
        {

        }

        public ulong EvaluationCount { get; }
        public bool StopConditionSatisfied => false;
    }
}

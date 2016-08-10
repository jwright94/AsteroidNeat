using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Worlds;
using SharpNeat.Core;
using SharpNeat.Phenomes;

namespace AsteroidNeat.Learning
{
    class ShipEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        public FitnessInfo Evaluate(IBlackBox phenome)
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
            }

            return new FitnessInfo(gameplayWorld.Score, gameplayWorld.Score);
        }

        public void Reset()
        {

        }

        public ulong EvaluationCount { get; }
        public bool StopConditionSatisfied => false;
    }
}

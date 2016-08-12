using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsteroidNeat.Worlds;

namespace HeadlessShipTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            ExperimentWorld world = new ExperimentWorld()
            {
                Width = 800,
                Height = 800
            };

            while (true)
            {
                world.Update(1f/30f);
                Thread.Sleep(1000/30);
            }
        }
    }
}

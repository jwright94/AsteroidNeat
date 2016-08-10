using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using AsteroidNeat.Learning;
using Microsoft.Xna.Framework.Graphics;
using SharpNeat.Genomes.Neat;

namespace AsteroidNeat.Worlds
{
    class ReplayWorld : GameplayWorld
    {
        private bool initialized = false;

        public ReplayWorld()
        {
            
        }

        private void Init()
        {
            NeatGenome genome = null;

            // Try to load the genome from the XML document.
            try
            {
                using (XmlReader xr = XmlReader.Create("bestPilot.xml"))
                    genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false)[0];
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error loading file: {0}", exception);
                GameOver = true;
                return;
            }

            var experiment = new TestFlight();

            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("asteroids.config.xml");
            experiment.Initialize("Asteroids", xmlConfig.DocumentElement);

            var genomeDecoder = experiment.CreateGenomeDecoder();

            var brain = genomeDecoder.Decode(genome);

            InitializeAI(brain);

            initialized = true;
        }

        public override World Update(float dt)
        {
            if(!initialized)
                Init();

            if (GameOver)
            {
                return new WorldSelector();    
            }

            return base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}

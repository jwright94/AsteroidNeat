using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using AsteroidNeat.Entities;
using AsteroidNeat.Entities.Ships;
using AsteroidNeat.Learning;
using log4net.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpNeat.Core;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using TicTacToeEvolution;

namespace AsteroidNeat.Worlds
{
    class ExperimentWorld : World
    {
        private NeatEvolutionAlgorithm<NeatGenome> _ea;
        private Player player;

        public ExperimentWorld()
        {
            Thread bgThread = new Thread(Initialize);
            bgThread.Name = "Learning Thread";
            bgThread.Start();
        }

        public void Initialize()
        {
            // Initialise log4net (log to console).
            XmlConfigurator.Configure(new FileInfo("log4net.properties"));

            // Experiment classes encapsulate much of the nuts and bolts of setting up a NEAT search.
            TestFlight experiment = new TestFlight();

            // Load config XML.
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("asteroids.config.xml");
            experiment.Initialize("Asteroids", xmlConfig.DocumentElement);

            // Create evolution algorithm and attach update event.
            _ea = experiment.CreateEvolutionAlgorithm();
            _ea.UpdateEvent += new EventHandler(ea_UpdateEvent);

            // Start algorithm (it will run on a background thread).
            _ea.StartContinue();
        }

        private string statusText = "";

        private void ea_UpdateEvent(object sender, EventArgs e)
        {
            lock (statusText)
            {
                statusText = $"gen={_ea.CurrentGeneration:N0} bestFitness={_ea.Statistics._maxFitness:N6}";
                Console.WriteLine(statusText);
            }
            // Save the best genome to file
            var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
            doc.Save("bestPilot.xml");
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            DrawUI(sb);
        }

        private void DrawUI(SpriteBatch sb)
        {
            lock (statusText)
            {
                sb.DrawString(Resources.Font, "Experiment 0/0\n" + statusText, Vector2.Zero, Resources.ForegroundColor);
            }
        }
    }
}

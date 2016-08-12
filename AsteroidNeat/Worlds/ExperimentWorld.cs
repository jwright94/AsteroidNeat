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
using Microsoft.Xna.Framework.Input;
using SharpNeat.Core;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using TicTacToeEvolution;

namespace AsteroidNeat.Worlds
{
    public class ExperimentWorld : World
    {
        private const string PilotFileName = "bestPilot.xml";
        private const string LatestPopulationFile = "latestPopulation.xml";
        private const float SaveInterval = 5 * 60;

        private TestFlight experiment = new TestFlight();
        private NeatEvolutionAlgorithm<NeatGenome> _ea;
        private Player player;

        
        private float timeSinceLastSave;

        public ExperimentWorld()
        {
            /*
            Thread bgThread = new Thread(Initialize);
            bgThread.Name = "Learning Thread";
            bgThread.Start();
            */
            Initialize();
        }

        public void Initialize()
        {
            // Initialise log4net (log to console).
            XmlConfigurator.Configure(new FileInfo("log4net.properties"));

            // Load config XML.
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("asteroids.config.xml");
            experiment.Initialize("Asteroids", xmlConfig.DocumentElement);

            // Check for previous best genome and continue experiment
            if (File.Exists(LatestPopulationFile))
            {
                var population = experiment.LoadPopulation(new XmlTextReader(LatestPopulationFile));
                Console.WriteLine("Reloaded previous best generation");
                _ea = experiment.CreateEvolutionAlgorithm(population);
            }

            if(_ea == null)
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

        public override World Update(float dt)
        {
            timeSinceLastSave += dt;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || timeSinceLastSave > SaveInterval)
            {
                _ea.RequestPauseAndWait();

                using (var xmlWriter = new XmlTextWriter(LatestPopulationFile, Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    experiment.SavePopulation(xmlWriter, _ea.GenomeList);
                }

                _ea.StartContinue();
                timeSinceLastSave = 0;
            }
            return base.Update(dt);
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

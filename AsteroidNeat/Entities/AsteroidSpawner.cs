using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Entities
{
    class AsteroidSpawner : GameObject
    {
        private static Random random = new Random();
        private float spawnTimer;

        public override void Update(float dt)
        {
            spawnTimer -= dt;
            if (spawnTimer < 0)
            {
                spawnTimer = random.Next(3000, 10000)/1000f;
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            var spawnLocation = new Vector2();

            var vertical = random.NextDouble() > 0.5;
            var useAlternative = random.NextDouble() > 0.5;

            if (vertical)
            {
                spawnLocation = new Vector2(
                    useAlternative ? 0 : world.Width, 
                    random.Next(0, world.Height));
            }
            else
            {
                spawnLocation = new Vector2(
                    random.Next(0, world.Width),
                    useAlternative ? 0 : world.Height);
            }

            var asteroidRadius = random.Next(16, 128);

            var spawnVelocity = new Vector2(
                (float)(random.NextDouble() - 0.5 * 2.0),
                (float)(random.NextDouble() - 0.5 * 2.0));

            world.Add(new Asteroid()
            {
                Position = spawnLocation,
                Radius = asteroidRadius,
                Velocity = spawnVelocity,
                Rotation = (float)random.NextDouble() * 360
            });
        }

        public override void Draw(SpriteBatch sb)
        {

        }
    }
}

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
            if (spawnTimer < 0 && world.AsteroidCount == 0)
            {
                spawnTimer = random.Next(5000, 10000)/1000f;
                for (int i = 0; i < 5; i++)
                    SpawnAsteroid(64);
            }
        }

        private int CountAsteroids()
        {
            return world.gameObjects.Cast<Asteroid>().Count();
        }

        private void SpawnAsteroid(int asteroidRadius)
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

            var spawnVelocity = new Vector2(
                (float)((random.NextDouble() - 0.5) * 2.0),
                (float)((random.NextDouble() - 0.5) * 2.0));

            spawnVelocity *= random.Next(200, 300);

            world.Add(new Asteroid()
            {
                Position = spawnLocation,
                Radius = asteroidRadius,
                Velocity = spawnVelocity,
                Rotation = (float)(random.NextDouble() * Math.PI * 2.0)
            });

            world.AsteroidCount++;
        }

        public override void Draw(SpriteBatch sb)
        {

        }
    }
}

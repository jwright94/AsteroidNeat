using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpNeat.Phenomes;

namespace AsteroidNeat.Entities.Ships
{
    class AIShip : Ship
    {
        private IBlackBox brain;

        public AIShip(IBlackBox brain)
        {
            this.brain = brain;
        }

        public override void Update(float dt)
        {
            brain.InputSignalArray[0] = 1;
            brain.InputSignalArray[1] = MathHelper.WrapAngle(Rotation) / Math.PI;

            int scans = 4;

            for (int scan = 0; scan < scans; scan++)
            {
                var angle = (2.0*Math.PI)/ (double)scans *(double)scan;
                var result = Sense((float)angle, world.Width);

                brain.InputSignalArray[2 + scan] = result;
            }

            brain.Activate();

            Throttle = (float)brain.OutputSignalArray[0];
            Tilt = (float)brain.OutputSignalArray[1];
            Fire = brain.OutputSignalArray[2] >= 0.5;

            base.Update(dt);
        }

        public float Sense(float direction, float maxDistance)
        {
            var directionVector = new Vector3((float)Math.Cos(direction), (float)Math.Sin(direction), 0f);
            Ray r = new Ray(new Vector3(Position, 0), directionVector);

            BoundingSphere sphere = new BoundingSphere();
            float? distance;

            float shortestDistance = float.MaxValue;

            foreach (var gameObject in world.gameObjects)
            {
                var asteroid = gameObject as Asteroid;
                if (asteroid != null)
                {
                    sphere.Center = new Vector3(asteroid.Position, 0);
                    sphere.Radius = asteroid.Radius;

                    r.Intersects(ref sphere, out distance);

                    if (distance.HasValue)
                        shortestDistance = Math.Min(shortestDistance, distance.Value);
                }
            }

            return Math.Min(shortestDistance, maxDistance) / maxDistance;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}

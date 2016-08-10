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
            var angle = MathHelper.WrapAngle(Rotation)/Math.PI;
            var currInput = 0;

            brain.InputSignalArray[currInput++] = 1;

            brain.InputSignalArray[currInput++] = Math.Cos(angle);
            brain.InputSignalArray[currInput++] = Math.Sin(angle);

            brain.InputSignalArray[currInput++] = Velocity.X;
            brain.InputSignalArray[currInput++] = Velocity.Y;

            int scans = 8;

            for (int scan = 0; scan < scans; scan++)
            {
                var senseAngle = (2.0*Math.PI)/ (double)scans *(double)scan;
                var result = Sense((float)senseAngle, world.Width);

                brain.InputSignalArray[currInput++] = result;
            }

            brain.Activate();

            Throttle = (float)MathHelper.Clamp((float)brain.OutputSignalArray[0], -1, 1);
            Tilt = (float)MathHelper.Clamp((float)brain.OutputSignalArray[1], -1, 1);
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

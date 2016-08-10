using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Entities
{
    class Asteroid : PhysicsObject
    {
        private static Random random = new Random();

        public Asteroid()
        {
            Radius = 128;
            Velocity += new Vector2(10, 5);
        }

        public override void Update(float dt)
        {
            Rotation += (float)(Math.PI/8f*dt);
            base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {
            Vector2 scale = new Vector2(Radius / Resources.Asteroid.Width, Radius / Resources.Asteroid.Height);

            sb.Draw(Resources.Asteroid, 
                position: Position,
                origin: new Vector2(Resources.Asteroid.Width, Resources.Asteroid.Height) / 2f, 
                color: Resources.ForegroundColor,
                rotation: Rotation,
                scale: scale * 2.4f);

            base.Draw(sb);
        }

        public void SplitApart()
        {
            world.Remove(this);
            Resources.ExplosionSfx.Play();

            world.Score += Radius*100;

            if(Radius <= 16) return;

            var breakDirection = new Vector2(
                (float)(random.NextDouble() - 0.5 * 2.0),
                (float)(random.NextDouble() - 0.5 * 2.0));

            var breakSpeed = random.Next(100, 200);

            world.Add(new Asteroid()
            {
                Position = Position,
                Rotation = Rotation,
                Velocity = breakDirection*(float)breakSpeed,
                Radius = Radius / 2f
            });

            world.Add(new Asteroid()
            {
                Position = Position,
                Rotation = Rotation,
                Velocity = -breakDirection * (float)breakSpeed,
                Radius = Radius / 2f
            });
        }
    }
}

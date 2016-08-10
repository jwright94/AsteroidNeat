using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Entities.Ships
{
    class Ship : PhysicsObject
    {

        public bool IsDead;

        public float Throttle;
        public float Tilt;
        public bool Fire;

        public float Speed;
        public float RotationalSpeed;
        public float FireRate;

        private float fireTimer;

        public Ship()
        {
            Speed = 256 + 128;
            RotationalSpeed = 1.5f * (float)Math.PI;
            Radius = 12;
            FireRate = 1f / 4f;
            Rotation = (float)-Math.PI/2f;
        }

        public override void Update(float dt)
        {
            Rotation += Tilt * RotationalSpeed * dt;
            Velocity += new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * Speed * Throttle * dt;

            if (Fire)
            {
                fireTimer -= dt;
                if (fireTimer < 0)
                {
                    FireWeapon();
                    fireTimer = FireRate;
                }
            }

            // Physics
            DoCollision();

            base.Update(dt);
        }

        private void DoCollision()
        {
            foreach (var gameObject in world.gameObjects)
            {
                var asteroid = gameObject as Asteroid;
                if (asteroid != null)
                {
                    var collisionInfo = CheckCollision(asteroid);
                    if (collisionInfo.IsColliding)
                    {
                        IsDead = true;
                        world.Remove(this);
                    }
                }
            }
        }

        private void FireWeapon()
        {
            var bulletVector = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            var bullet = new Bullet()
            {
                Radius = 8,
                Position = Position,
                Velocity = bulletVector * 512 + Velocity
            };

            world.Add(bullet);

            if(!world.IsBackground)
                Resources.ShootSfx.Play();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Resources.Ship,
                position: Position,
                origin: new Vector2(Resources.Ship.Width, Resources.Ship.Height) / 2f,
                color: Resources.ForegroundColor,
                rotation: Rotation);

            base.Draw(sb);
        }
    }
}

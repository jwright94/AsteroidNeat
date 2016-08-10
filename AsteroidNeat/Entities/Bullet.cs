using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Entities
{
    class Bullet : PhysicsObject
    {
        private float lifeTimer = 2f;

        public override void Update(float dt)
        {
            DoCollision();
            lifeTimer -= dt;
            if(lifeTimer <= 0)
                world.Remove(this);

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
                        world.Remove(this);
                        asteroid.SplitApart();
                    }
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            Vector2 scale = new Vector2(Radius / Resources.Bullet.Width, Radius / Resources.Bullet.Height);

            sb.Draw(Resources.Bullet,
                position: Position,
                origin: new Vector2(Resources.Bullet.Width, Resources.Bullet.Height) / 2f,
                color: Resources.ForegroundColor,
                rotation: Rotation,
                scale: scale);

            base.Draw(sb);
        }
    }
}

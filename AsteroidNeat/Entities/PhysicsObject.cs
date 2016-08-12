using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Entities
{
    public class PhysicsObject : GameObject
    {

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius     { get; set; }
        public float Rotation   { get; set; }


        public override void Update(float dt)
        {
            Position += Velocity*dt;

            WrapCoordinates();
        }

        private void WrapCoordinates()
        {
            var newPos = new Vector2(Position.X % world.Width, Position.Y % world.Height);

            if (newPos.X < 0)
                newPos.X += world.Width;

            if (newPos.Y < 0)
                newPos.Y += world.Height;

            Position = newPos;
        }

        public override void Draw(SpriteBatch sb)
        {

        }

        public override void OnAdd()
        {
            
        }

        public override void OnRemove()
        {
            
        }

        public CollisionInfo CheckCollision(PhysicsObject obj)
        {
            var collisionInfo = new CollisionInfo();

            var delta = this.Position - obj.Position;
            var distance = delta.Length();
            var minimumDistance = Radius + obj.Radius;

            collisionInfo.IsColliding = (distance < minimumDistance);
            collisionInfo.Depth = distance - minimumDistance;
            collisionInfo.Direction = delta;

            return collisionInfo;
        }
    }
}

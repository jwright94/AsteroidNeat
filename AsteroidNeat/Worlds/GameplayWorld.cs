using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Entities;
using AsteroidNeat.Entities.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpNeat.Phenomes;

namespace AsteroidNeat.Worlds
{
    class GameplayWorld : World
    {
        private Ship ship;

        public GameplayWorld()
        {
            
        }

        public bool GameOver
        { get; set; }

        public Ship Ship => ship;

        public void Initialize()
        {
            ship = new Player()
            {
                Position = new Vector2(Width / 2f, Height / 2f)
            };

            Add(ship);
            Add(new AsteroidSpawner());
        }

        public void InitializeAI(IBlackBox brain)
        {
            ship = new AIShip(brain)
            {
                Position = new Vector2(Width / 2f, Height / 2f)
            };

            Add(ship);
            Add(new AsteroidSpawner());
        }

        public override World Update(float dt)
        {
            Score += 10;
            GameOver = ship.IsDead;
            return base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            DrawUI(sb);
        }

        private void DrawUI(SpriteBatch sb)
        {
            sb.DrawString(Resources.Font, $"Score: {(Score)}", Vector2.Zero, Resources.ForegroundColor);
        }
    }
}

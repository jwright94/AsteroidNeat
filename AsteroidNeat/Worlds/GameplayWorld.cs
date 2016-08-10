using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Entities;
using AsteroidNeat.Entities.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Worlds
{
    class GameplayWorld : World
    {
        private Player player;

        public GameplayWorld()
        {
            
        }

        public void Initialize()
        {
            player = new Player()
            {
                Position = new Vector2(Width / 2f, Height / 2f),
                world = this
            };
            Add(player);
            Add(new AsteroidSpawner());
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach(var go in gameObjects)
                go.Draw(sb);

            DrawUI(sb);
        }

        private void DrawUI(SpriteBatch sb)
        {
            sb.DrawString(Resources.Font, $"Score: {(Score)}", Vector2.Zero, Resources.ForegroundColor);
        }
    }
}

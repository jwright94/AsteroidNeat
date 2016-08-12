using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidNeat.Worlds
{
    class WorldSelector : World
    {
        public override World Update(float dt)
        {
            var ks = Keyboard.GetState();
            var keys = ks.GetPressedKeys();

            if (keys.Length < 1) return this;

            switch (keys[0])
            {
                case Keys.D1:
                    var gameplayWorld = new GameplayWorld()
                    {
                        Width = 800,
                        Height = 800
                    };
                    gameplayWorld.Initialize();
                    return gameplayWorld;
                case Keys.D2:
                    var experimentWorld = new ExperimentWorld()
                    {
                        Width = 800,
                        Height = 800
                    };
                    return experimentWorld;
                case Keys.D3:
                    var replayWorld = new ReplayWorld()
                    {
                        Width = 800,
                        Height = 800
                    };
                    return replayWorld;
                default:
                    return this;
            }

            return base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Resources.Font, "1. Manual Play\n2. Train\n3. Replay", Vector2.Zero, Resources.ForegroundColor);
            base.Draw(sb);
        }
    }
}

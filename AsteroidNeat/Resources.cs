using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat
{
    class Resources
    {
        public static Texture2D Ship;
        public static Texture2D Asteroid;
        public static Texture2D Bullet;

        public static SoundEffect ShootSfx;
        public static SoundEffect ExplosionSfx;

        public static SpriteFont Font;

        public static readonly Color BackgroundColor = Color.Black;
        public static readonly Color ForegroundColor = Color.Lime;

        public static void Load(ContentManager content)
        {
            Ship = content.Load<Texture2D>("ship");
            Asteroid = content.Load<Texture2D>("asteroid");
            Bullet = content.Load<Texture2D>("bullet");

            ShootSfx = content.Load<SoundEffect>("shoot");
            ExplosionSfx = content.Load<SoundEffect>("explosion");

            Font = content.Load<SpriteFont>("font");

        }
    }
}

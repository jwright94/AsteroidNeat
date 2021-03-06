﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Worlds;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Entities
{
    public abstract class GameObject
    {
        public World world;

        public abstract void Update(float dt);
        public abstract void Draw(SpriteBatch sb);

        public abstract void OnAdd();
        public abstract void OnRemove();
    }
}
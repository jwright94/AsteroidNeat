using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidNeat.Entities.Ships
{
    class Player : Ship
    {

        public override void Update(float dt)
        {
            HandleInput();
            base.Update(dt);
        }

        private void HandleInput()
        {
            Tilt = Throttle = 0;

            var ks = Keyboard.GetState();

            // Input
            if (ks.IsKeyDown(Keys.A))
                Tilt = -1;
            else if (ks.IsKeyDown(Keys.D))
                Tilt = 1;

            if (ks.IsKeyDown(Keys.S))
                Throttle = -1;
            else if (ks.IsKeyDown(Keys.W))
                Throttle = 1;

            Fire = ks.IsKeyDown(Keys.Space);
        }
    }
}

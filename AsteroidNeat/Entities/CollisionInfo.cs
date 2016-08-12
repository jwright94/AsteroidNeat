using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AsteroidNeat.Entities
{
    public class CollisionInfo
    {
        public bool IsColliding     { get; set; }
        public float Depth          { get; set; }
        public Vector2 Direction    { get; set; }
    }
}

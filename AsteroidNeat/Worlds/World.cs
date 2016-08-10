using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat.Worlds
{
    abstract class World
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public float Score { get; set; }
        public bool IsBackground { get; set; }

        public readonly List<GameObject> gameObjects = new List<GameObject>();

        private List<GameObject> removeMe = new List<GameObject>();
        private List<GameObject> addMe = new List<GameObject>();

        public void Add(GameObject gameObject)
        {
            gameObject.world = this;
            addMe.Add(gameObject);
        }

        public void Remove(GameObject gameObject)
        {
            removeMe.Add(gameObject);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            foreach (var go in gameObjects)
                go.Draw(sb);
        }

        public virtual World Update(float dt)
        {
            foreach (var go in gameObjects)
                go.Update(dt);

            HandleAddsAndDeletes();

            return this;
        }

        private void HandleAddsAndDeletes()
        {
            gameObjects.RemoveAll(removeMe.Contains);
            gameObjects.AddRange(addMe);

            removeMe.Clear();
            addMe.Clear();
        }

    }
}

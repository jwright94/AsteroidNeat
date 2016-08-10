using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidNeat.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidNeat
{
    class World
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public float Score { get; set; }

        public List<GameObject> gameObjects = new List<GameObject>();

        private List<GameObject> removeMe = new List<GameObject>();
        private List<GameObject> addMe = new List<GameObject>();

        private Player player;

        public World()
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

        public void Add(GameObject gameObject)
        {
            gameObject.world = this;
            addMe.Add(gameObject);
        }

        public void Remove(GameObject gameObject)
        {
            removeMe.Add(gameObject);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach(var go in gameObjects)
                go.Draw(sb);

            DrawUI(sb);
        }

        public void Update(float dt)
        {
            foreach (var go in gameObjects)
                go.Update(dt);

            HandleAddsAndDeletes();
        }

        private void HandleAddsAndDeletes()
        {
            gameObjects.RemoveAll(removeMe.Contains);
            gameObjects.AddRange(addMe);

            removeMe.Clear();
            addMe.Clear();
        }

        private void DrawUI(SpriteBatch sb)
        {
            sb.DrawString(Resources.Font, $"Score: {(Score)}", Vector2.Zero, Resources.ForegroundColor);
        }
    }
}

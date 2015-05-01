using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame.Content {

    public class Animatable {
        private float totalElapsed;

        public Animatable() {
        }

        protected Animatable(Texture2D[] textures) {
            this.textures = textures;
            this.framecount = textures.Count();
            this.totalElapsed = 0;
            this.timePerFrame = (float)1f / 4f;
            this.frame = 0;
        }

        protected int frame { get; set; }

        protected int framecount { get; set; }

        protected Texture2D[] textures { get; set; }

        protected float timePerFrame { get; set; }

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y) {
            spriteBatch.Draw(textures[frame], new Vector2(x, y), Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position) {
            spriteBatch.Draw(textures[frame], position, Color.White);
        }

        public virtual void UpdateFrame(float elapsed) {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame) {
                frame++;
                frame = frame % framecount;
                totalElapsed -= timePerFrame;
            }
        }
    }
}
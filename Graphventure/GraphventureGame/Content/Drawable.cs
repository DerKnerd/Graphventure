using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame.Content {

    public class Drawable {
        protected Texture2D Texture;

        protected Drawable() {
        }

        protected Drawable(Texture2D texture) {
            Texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y) {
            Draw(spriteBatch, new Vector2(x, y));
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination) {
            spriteBatch.Draw(Texture, destination, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }
}
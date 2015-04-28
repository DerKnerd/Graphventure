using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame {

    public enum ScreenType {
        Fight,
        Lost,
        Map,
        Welcome,
        Won
    }

    public abstract class Screen {

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract void Initialize();

        public abstract void LoadContent(ContentManager contentmanager);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);
    }
}
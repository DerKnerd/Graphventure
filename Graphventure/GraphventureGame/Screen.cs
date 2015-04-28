using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame {

    public abstract class Screen {

        public abstract void LoadContent(ContentManager contentmanager);

        public abstract void UnloadContent();

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }

    internal enum ScreenType {
        Map,
        Fight
    }
}
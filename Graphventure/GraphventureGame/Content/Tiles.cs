using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame.Content {

    public static class Tiles {
        public static Tile Grass;
        public static Tile Wall;

        public static void LoadTiles(ContentManager contentmanager) {
            Grass = Tile.FromTexture(contentmanager.Load<Texture2D>("Tiles/grass"));
            Wall = Tile.FromTexture(contentmanager.Load<Texture2D>("Tiles/wall"));
        }
    }

    public class Tile : Drawable {

        private Tile(Texture2D texture)
            : base(texture) {
        }

        public static Tile FromTexture(Texture2D texture) {
            return new Tile(texture);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y) {
            Draw(spriteBatch, new Rectangle(x * 40, y * 32, 40, 32));
        }
    }
}
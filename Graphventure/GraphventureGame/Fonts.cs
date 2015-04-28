using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame {

    public static class Fonts {

        public static SpriteFont Halo { get; private set; }

        public static SpriteFont Welcome { get; private set; }

        public static void LoadFonts(ContentManager contentmanager) {
            Halo = contentmanager.Load<SpriteFont>("Fonts/halo");
            Welcome = contentmanager.Load<SpriteFont>("Fonts/welcome");
        }
    }
}
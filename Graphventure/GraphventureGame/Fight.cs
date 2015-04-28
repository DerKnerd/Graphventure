using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graphventure.GraphventureGame {

    public class Fight : Screen {
        private static short FightingCount = 0;

        public Fight() {
            FightingCount++;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
        }

        public override void Initialize() {
        }

        public override void LoadContent(ContentManager contentmanager) {
        }

        public override void UnloadContent() {
        }

        public override void Update(GameTime gameTime) {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W)) {
                Graphventure.CurrentAdventure.EndFight(true);
            }
            if (keyboardState.IsKeyDown(Keys.L)) {
                Graphventure.CurrentAdventure.EndFight(false);
            }
        }
    }
}
using System;
using Graphventure.GraphventureGame.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graphventure.GraphventureGame {

    public class Fight : Screen {
        private byte enemyHealth = 100;
        private byte playerHealth = 100;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            Sprites.EnemyFighting.Draw(spriteBatch, new Vector2(16, 16));
            Sprites.PlayerFighting.Draw(spriteBatch, new Vector2(360, 232));
        }

        public override void Initialize() {
        }

        public void StartFight() {
            playerHealth = 100;
            enemyHealth = Convert.ToByte(new Random((int)DateTime.Now.Ticks).Next(75, byte.MaxValue));
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
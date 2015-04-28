using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graphventure.GraphventureGame {

    public class Won : Screen {

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            var graphicsdevice = spriteBatch.GraphicsDevice;
            var line1 = @"You won the fight \o/";
            var line2 = "Hit return to continue";
            {
                var fontorigin = Fonts.Welcome.MeasureString(line1);
                var fontpos = new Vector2(graphicsdevice.Viewport.Width / 2, graphicsdevice.Viewport.Height / 2);
                fontpos.X = fontpos.X + (fontorigin.X / 2);
                spriteBatch.DrawString(Fonts.Welcome, line1, fontpos, Color.Black, 0, fontorigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            {
                var fontorigin = Fonts.Halo.MeasureString(line2);
                var fontpos = new Vector2(graphicsdevice.Viewport.Width / 2, graphicsdevice.Viewport.Height / 2);
                fontpos.X = fontpos.X + (fontorigin.X / 2);
                fontpos.Y = fontpos.Y + fontorigin.Y;
                spriteBatch.DrawString(Fonts.Halo, line2, fontpos, Color.Black, 0, fontorigin, 1.0f, SpriteEffects.None, 0.5f);
            }
        }

        public override void Initialize() {
        }

        public override void LoadContent(ContentManager contentmanager) {
        }

        public override void UnloadContent() {
        }

        public override void Update(GameTime gameTime) {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter)) {
                Graphventure.CurrentAdventure.ShowMapAfterFight(true);
            }
        }
    }
}
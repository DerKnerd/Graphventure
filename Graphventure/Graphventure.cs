using Graphventure.GraphventureGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graphventure {

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Graphventure : Game {
        private Screen currentScreen;
        private ScreenType currentScreenType;
        private GraphicsDeviceManager graphics;
        private Map map;
        private SpriteBatch spriteBatch;

        public Graphventure()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 640;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred);
            currentScreen.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            map = Map.Parse("maps/demo.txt");
            map.LoadContent(Content);
            currentScreen = map;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            var keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            setScreen();
            currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        private void setScreen() {
            switch (currentScreenType) {
                case ScreenType.Map:
                    currentScreen = map;
                    break;

                case ScreenType.Fight:
                    break;
            }
        }
    }
}
using Graphventure.GraphventureGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graphventure {

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Graphventure : Game {
        private static Graphventure currentAdventure;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        #region Screens

        private Screen currentScreen;
        private ScreenType currentScreenType;
        private Fight fight;
        private Lost lost;
        private Map map;
        private Welcome welcome;
        private Won won;

        #endregion Screens

        public Graphventure()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 640;
            graphics.IsFullScreen = true;
            currentAdventure = this;
        }

        public static Graphventure CurrentAdventure { get { return currentAdventure; } }

        public void BeginFight() {
            currentScreenType = ScreenType.Fight;
            fight = new Fight();
        }

        public void EndFight(bool won) {
            if (won) {
                currentScreenType = ScreenType.Won;
            } else {
                currentScreenType = ScreenType.Lost;
            }
        }

        public void ShowMapAfterFight(bool won) {
            currentScreenType = ScreenType.Map;
            map.EndFight(won);
        }

        public void Start() {
            currentScreenType = ScreenType.Map;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Wheat);

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
            currentScreenType = ScreenType.Welcome;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Fonts.LoadFonts(Content);
            map = Map.Parse("maps/demo.txt");
            welcome = new Welcome();
            won = new Won();
            lost = new Lost();

            map.LoadContent(Content);
            currentScreen = welcome;
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
                    currentScreen = fight;
                    break;

                case ScreenType.Won:
                    currentScreen = won;
                    break;

                case ScreenType.Lost:
                    currentScreen = lost;
                    break;
            }
        }
    }
}
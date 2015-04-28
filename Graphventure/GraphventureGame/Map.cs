using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Graphventure.GraphventureGame {

    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    public class Map : Screen {
        private readonly Vector2 Speed = new Vector2(40, 32);

        private Texture2D enemyTexture;

        private Texture2D grassTexture;

        private Vector2 oldPosition;
        private KeyboardState oldState;

        private Texture2D playerTexture;

        private Texture2D wallTexture;

        private Map() {
            Position = new Vector2(0, 0);
        }

        public string Data { get; set; }

        public string[][] MapData { get; set; }

        public Vector2 Position { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public static Map Parse(string path) {
            var map = new Map();
            var stream = TitleContainer.OpenStream(Path.Combine("Content", path));
            using (var sr = new StreamReader(stream)) {
                map.Data = sr.ReadToEnd();
            }
            var lines = map.Data.Split(new string[1] { Environment.NewLine }, StringSplitOptions.None);
            var mapHeight = lines.Count();
            map.MapData = new string[mapHeight][];
            for (int i = 0; i < lines.Count(); i++) {
                var elements = lines[i].Split(';');
                map.MapData[i] = elements;
            }
            return map;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gametime) {
            this.SpriteBatch = spriteBatch;
            for (int y = 0; y < MapData.Count(); y++) {
                var current = MapData[y];
                for (int x = 0; x < current.Count(); x++) {
                    drawTile(x, y);
                    switch (current[x]) {
                        case "e":
                            drawEnemy(x, y);
                            break;

                        case "w":
                            drawWall(x, y);
                            break;
                    }
                }
            }
            drawPlayer();
        }

        public void EndFight(bool won) {
            if (won) {
                MapData[(int)Position.Y][(int)Position.X] = ".";
            } else {
                Position = oldPosition;
            }
        }

        public override void Initialize() {
        }

        public override void LoadContent(ContentManager content) {
            playerTexture = content.Load<Texture2D>("Sprites/player");
            enemyTexture = content.Load<Texture2D>("Sprites/enemy");
            grassTexture = content.Load<Texture2D>("Tiles/grass");
            wallTexture = content.Load<Texture2D>("Tiles/wall");
        }

        public void Move(Direction pdirection) {
            var direction = Vector2.Zero;
            var x = this.Position.X;
            var y = this.Position.Y;
            switch (pdirection) {
                case Direction.Up:
                    if (y > 0 && y <= 15) {
                        direction = new Vector2(0, -1);
                    }
                    break;

                case Direction.Down:
                    if (y < 15 && y >= 0) {
                        direction = new Vector2(0, 1);
                    }
                    break;

                case Direction.Left:
                    if (x > 0 && x <= 15) {
                        direction = new Vector2(-1, 0);
                    }
                    break;

                case Direction.Right:
                    if (x < 15 && x >= 0) {
                        direction = new Vector2(1, 0);
                    }
                    break;
            }
            oldPosition = Position;
            Position += direction;
            if (MapData[(int)Position.Y][(int)Position.X] == "w") {
                Position = oldPosition;
            }
            if (MapData[(int)Position.Y][(int)Position.X] == "e") {
                Graphventure.CurrentAdventure.BeginFight();
            }
        }

        public override void UnloadContent() {
        }

        public override void Update(GameTime gameTime) {
            updateInput(Keyboard.GetState());
        }

        private bool checkKey(KeyboardState keyboardState, Keys key) {
            if (keyboardState.IsKeyDown(key)) {
                if (!oldState.IsKeyDown(key)) {
                    return true;
                }
            }
            return false;
        }

        private void drawEnemy(int x, int y) {
            drawTexture(enemyTexture, x, y);
        }

        private void drawPlayer() {
            drawTile((int)Position.X, (int)Position.Y);
            var p = Position * new Vector2(40, 32);
            this.SpriteBatch.Draw(playerTexture, p, Color.White);
        }

        private void drawTexture(Texture2D texture, int x, int y) {
            var rectangle = new Rectangle(x * 40, y * 32, 40, 32);
            this.SpriteBatch.Draw(texture, rectangle, Color.White);
        }

        private void drawTile(int x, int y) {
            drawTexture(grassTexture, x, y);
        }

        private void drawWall(int x, int y) {
            drawTexture(wallTexture, x, y);
        }

        private void updateInput(KeyboardState keyboardState) {
            if (checkKey(keyboardState, Keys.Up)) {
                Move(Direction.Up);
            }
            if (checkKey(keyboardState, Keys.Down)) {
                Move(Direction.Down);
            }

            if (checkKey(keyboardState, Keys.Left)) {
                Move(Direction.Left);
            }
            if (checkKey(keyboardState, Keys.Right)) {
                Move(Direction.Right);
            }

            oldState = keyboardState;
        }
    }
}
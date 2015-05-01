using System;
using System.IO;
using System.Linq;
using Graphventure.GraphventureGame.Content;
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

        private Direction direction = Direction.Down;
        private Vector2 oldPosition;
        private KeyboardState oldState;
        private byte walkingIndex = 0;

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
                    Tiles.Grass.Draw(spriteBatch, x, y);
                    switch (current[x]) {
                        case "e":
                            Sprites.EnemyWalking.Draw(spriteBatch, new Vector2(x, y) * Speed);
                            break;

                        case "w":
                            Tiles.Wall.Draw(spriteBatch, x, y);
                            break;
                    }
                }
            }
            Sprites.PlayerWalking[direction].Draw(spriteBatch, Position * Speed);
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

        public override void Update(GameTime gameTime) {
            updateInput(Keyboard.GetState());
            Sprites.PlayerWalking[direction].UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private bool checkKey(KeyboardState keyboardState, Keys key) {
            if (keyboardState.IsKeyDown(key)) {
                if (!oldState.IsKeyDown(key)) {
                    return true;
                }
            }
            return false;
        }

        private void updateInput(KeyboardState keyboardState) {
            if (checkKey(keyboardState, Keys.Up)) {
                direction = Direction.Up;
                Move(direction);
            }
            if (checkKey(keyboardState, Keys.Down)) {
                direction = Direction.Down;
                Move(direction);
            }

            if (checkKey(keyboardState, Keys.Left)) {
                direction = Direction.Left;
                Move(direction);
            }
            if (checkKey(keyboardState, Keys.Right)) {
                direction = Direction.Right;
                Move(direction);
            }

            oldState = keyboardState;
        }
    }
}
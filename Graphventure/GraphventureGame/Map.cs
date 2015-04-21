using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame {

    public class Map {

        private Map() {
            Position = new Vector2(0, 0);
        }

        public string Data { get; set; }

        public string[][] MapData { get; set; }

        public ContentManager Content { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public Vector2 Position { get; set; }

        private Vector2 ArrayPosition;

        private readonly Vector2 Speed = new Vector2(40, 32);

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

        public void DrawMap(SpriteBatch spriteBatch, ContentManager content, GameTime gametime) {
            this.SpriteBatch = spriteBatch;
            this.Content = content;
            for (int y = 0; y < MapData.Count(); y++) {
                var current = MapData[y];
                for (int x = 0; x < current.Count(); x++) {
                    switch (current[x]) {
                        case "e":
                            drawEnemy(x, y);
                            break;

                        case "P":
                        case ".":
                            drawTile(x, y);
                            break;

                        case "w":
                            drawWall(x, y);
                            break;
                    }
                }
            }
            drawPlayer();
        }

        public void Move(Direction pdirection, GameTime gametime) {
            var direction = Vector2.Zero;
            var x = this.ArrayPosition.X;
            var y = this.ArrayPosition.Y;
            switch (pdirection) {
                case Direction.Up:
                    if (y > 0 && y < 16) {
                        direction = new Vector2(0, -1);
                    }
                    break;

                case Direction.Down:
                    if (y < 16 && y >= 0) {
                        direction = new Vector2(0, 1);
                    }
                    break;

                case Direction.Left:
                    if (x > 0 && x < 16) {
                        direction = new Vector2(-1, 0);
                    }
                    break;

                case Direction.Right:
                    if (x < 16 && x >= 0) {
                        direction = new Vector2(1, 0);
                    }
                    break;
            }
            var elapsedSeconds = (float)gametime.ElapsedGameTime.TotalSeconds;
            var oldArrayPosition = ArrayPosition;
            ArrayPosition += direction * elapsedSeconds;
            ArrayPosition.X = (float)Math.Round(ArrayPosition.X, 2);
            ArrayPosition.Y = (float)Math.Round(ArrayPosition.Y, 2);
            var arrayPosXFloor = (int)Math.Floor(ArrayPosition.X);
            var arrayPosYFloor = (int)Math.Floor(ArrayPosition.Y);

            var arrayPosXCeiling = (int)Math.Ceiling(ArrayPosition.X);
            var arrayPosYCeiling = (int)Math.Ceiling(ArrayPosition.Y);

            try {
                if (MapData[arrayPosYFloor][arrayPosXFloor] != "w" &&
                    MapData[arrayPosYCeiling][arrayPosXCeiling] != "w" &&
                    MapData[arrayPosYFloor][arrayPosXCeiling] != "w" &&
                    MapData[arrayPosYCeiling][arrayPosXFloor] != "w") {
                    Position += direction * Speed * elapsedSeconds;
                } else {
                    ArrayPosition = oldArrayPosition;
                }
            } catch {
                ArrayPosition = oldArrayPosition;
            }
        }

        private void drawPlayer() {
            var tile = Position / Speed;
            var x = tile.X;
            var y = tile.Y;
            drawTile((int)x, (int)y);
            var texture = Content.Load<Texture2D>("Sprites/player");
            this.SpriteBatch.Draw(texture, new Vector2(Position.X, Position.Y), Color.White);
        }

        private void drawEnemy(int x, int y) {
            drawTile(x, y);
            drawTexture(Content.Load<Texture2D>("Sprites/enemy"), x, y);
        }

        private void drawTile(int x, int y) {
            drawTexture(Content.Load<Texture2D>("Tiles/grass"), x, y);
        }

        private void drawWall(int x, int y) {
            drawTexture(Content.Load<Texture2D>("Tiles/wall"), x, y);
        }

        private void drawTexture(Texture2D texture, int x, int y) {
            var rectangle = new Rectangle(x * 40, y * 32, 40, 32);
            this.SpriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }
}
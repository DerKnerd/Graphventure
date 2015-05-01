using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame.Content {

    public static class Sprites {
        public static Sprite EnemyFighting;
        public static Sprite EnemyWalking;
        public static Sprite PlayerFighting;
        public static Dictionary<Direction, Sprite> PlayerWalking;

        public static void LoadSprites(ContentManager contentmanager) {
            PlayerWalking = new Dictionary<Direction, Sprite> {
                { Direction.Up, loadPlayerWalk(Direction.Up, contentmanager) },
                { Direction.Down, loadPlayerWalk(Direction.Down, contentmanager) },
                { Direction.Right, loadPlayerWalk(Direction.Right, contentmanager) },
                { Direction.Left, loadPlayerWalk(Direction.Left, contentmanager) }
            };
            EnemyWalking = Sprite.FromTextures(new Texture2D[1] { contentmanager.Load<Texture2D>("Sprites/enemy") });
            EnemyFighting = Sprite.FromTextures(new Texture2D[1] { contentmanager.Load<Texture2D>("sprites/enemy_fight") });
            PlayerFighting = Sprite.FromTextures(new Texture2D[1] { contentmanager.Load<Texture2D>("sprites/player_fight") });
        }

        private static Sprite loadPlayerWalk(Direction direction, ContentManager contentmanager) {
            var textures = new Texture2D[4];
            for (int i = 0; i < 4; i++) {
                textures[i] = contentmanager.Load<Texture2D>(string.Format("Sprites/player_walk_{0}_{1}", direction, i + 1));
            }
            return Sprite.FromTextures(textures);
        }
    }

    public class Sprite : Animatable {

        private Sprite(Texture2D[] textures)
            : base(textures) {
        }

        public static Sprite FromTextures(Texture2D[] textures) {
            return new Sprite(textures);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y) {
            base.Draw(spriteBatch, x, y);
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position) {
            base.Draw(spriteBatch, position);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphventure.GraphventureGame {

    internal enum TileCollision {
        Passable,
        Impassable,
        Enemy
    }

    internal struct Tile {
        public Texture2D Texture;
        public TileCollision Collision;

        public const int Width = 40;
        public const int Height = 32;

        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision) {
            Texture = texture;
            Collision = collision;
        }
    }
}
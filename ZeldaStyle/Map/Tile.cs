using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaStyle.Map {
    class Tile {
        public static int SIZE = 16;
        public int XPos;
        public int YPos;

        public Vector2 Position { get { return new Vector2(XPos * SIZE, YPos * SIZE); } }

        public Texture2D Tileset;

        public Rectangle Frame;

        public Tile() {
            XPos = 0;
            YPos = 0;
            Tileset = null;
            Frame = Rectangle.Empty;
        }
    }
}

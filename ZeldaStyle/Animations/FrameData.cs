using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeldaStyle.Animations {
    public struct FrameData {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public FrameData(int x, int y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}

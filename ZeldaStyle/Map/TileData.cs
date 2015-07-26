using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZeldaStyle.Animations;

namespace ZeldaStyle {
    class TileData {
        public bool Animated;
        //public 
        public List<FrameData> frames;

        public TileData() {
            Animated = false;
        }
    }
}

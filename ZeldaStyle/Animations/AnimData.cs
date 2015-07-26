using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZeldaStyle.Animations {
    public class AnimData {
        public string Name;
        public int FrameCount;
        public List<FrameData> FrameData;
        public float FPS;

        public AnimData(int frameCount, float fps, List<FrameData> data, string name) {
            FrameData = data;
            FrameCount = frameCount;
            FPS = fps;
            Name = name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using ZeldaStyle.Animations;

namespace Editor {
    class AnimGenerator {
        public AnimGenerator() {

        }

        public string GenerateAnimStrip(string name, int frameWidth, int frameHeight, int frameCount, int startX, int startY, float FPS, bool vertical = false) {
            //AnimData data = new AnimData();
            List<FrameData> frames = new List<FrameData>();
            for (int i = 0; i < frameCount; i++) {
                FrameData frame;
                if (vertical) {
                    frame = new FrameData(startX, startY + i * frameHeight, frameWidth, frameHeight);
                }
                else {
                    frame = new FrameData(startX + i * frameWidth, startY, frameWidth, frameHeight);
                }
                frames.Add(frame);
            }

            AnimData data = new AnimData(frameCount, FPS, frames, name);

            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}

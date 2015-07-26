using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZeldaStyle.Map {
    class AnimatedTile : Tile {


        float period;
        List<Rectangle> frames;
        float counter;
        //Rectangle currentFrame;
        int currentFrame;

        public AnimatedTile(float FPS) {
            counter = 0;
            period = 1 / FPS;
        }

        public void Update(float dt) {
            counter += dt;
            if (counter >= period) {
                counter = 0;
                currentFrame++;
                if (currentFrame >= frames.Count) {
                    currentFrame = 0;
                }
            }
        }

        public Rectangle GetFrame() {
            return frames[currentFrame];
        }
    }
}

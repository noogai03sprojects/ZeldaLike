using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZeldaStyle.Components {
    /// <summary>
    /// Used internally within AnimationController and defined in JSON.
    /// </summary>
    public class Animation {
        //public Rectangle TextureRect { get; private set; }

        public float FPS { get { return 1 / period; } set { period = 1 / value; } }
        private float period;

        private List<Rectangle> frames;

        private float counter;

        public int currentFrame;

        public string Name;

        public Animation(float fps, List<Rectangle> frames, string name) {
            period = 1 / fps;
            this.frames = frames;
            Name = name;
        }

        public void Update(float deltaTime) {
            //Console.WriteLine("Animation update called! deltaTime = " + deltaTime);
            counter += deltaTime;

            if (counter >= period) {
                //Console.WriteLine("Frame++!");
                currentFrame++;
                if (currentFrame >= frames.Count) {
                    currentFrame = 0;
                }
                counter = 0;
            }
        }

        /// <summary>
        /// Returns an updated Rectangle representing the current frame of animation in texture space.
        /// </summary>
        /// <returns>Rectangle representing sourcerect</returns>
        public Rectangle GetRectangle() {
            return frames[currentFrame];
        }

        /// <summary>
        /// Reset animation; used when switching animation
        /// </summary>
        public void Reset() {
            counter = 0;
            currentFrame = 0;
        }
   
       
    }
}

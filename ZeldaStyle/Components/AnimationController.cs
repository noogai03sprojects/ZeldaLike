using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ZeldaStyle.Components;
using System.IO;
using ZeldaStyle.Animations;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace ZeldaStyle.Components {
    /// <summary>
    /// Manages the addition and control of animations on a GameObject. GameObject MUST have a Sprite component already.
    /// </summary>
    public class AnimationController : Component {

        private Sprite sprite;

        public Dictionary<string, Animation> Animations;
        Animation currentAnimation;

        /// <summary>
        /// Whether or not the animation is running.
        /// </summary>
        public bool Running = true;

        /// <summary>
        /// Loads a new animation for use in the AnimationController. Obviously must be relevant to current sprite sheet.
        /// </summary>
        /// <param name="path">path of JSON animation to load</param>
        /// <param name="addContent">Adds "Content\\" by defauly - this can be disabled</param>
        public void LoadAnimation(string path, bool addContent = true) {
            string json;
            if (addContent)
                json = File.ReadAllText("Content\\" + path);
            else
                json = File.ReadAllText("Content\\" + path);
            AnimData data = JsonConvert.DeserializeObject<AnimData>(json);
            
            List<Rectangle> frames = new List<Rectangle>();
            foreach (FrameData frame in data.FrameData) {
                frames.Add(new Rectangle(frame.X, frame.Y, frame.Width, frame.Height));
            }

            Animation animation = new Animation(data.FPS, frames, data.Name);
            Animations.Add(data.Name, animation);
        }

        public AnimationController() {
            Animations = new Dictionary<string, Animation>();
            //currentAnimation = Animations.
        }

        /// <summary>
        /// CALL THIS AFTER ATTACHING A SPRITE
        /// </summary>
        public void Initialize() {
            sprite = GetComponent<Sprite>();
        }

        public override void Update(float deltaTime) {
            //Console.WriteLine("AnimationController update called! frame = " + currentAnimation.currentFrame);
            if (Running)
                currentAnimation.Update(deltaTime);
        }

        /// <summary>
        /// Switches animation. Will reset current animation and start from frame 0 so be sure to use SetFrame if needed.
        /// </summary>
        /// <param name="name">Name of animation to change to as defined in JSON.</param>
        public void SwitchAnimation(string name) {
            //Console.WriteLine("Switched animation to: " + name);
            if (currentAnimation != null)
                currentAnimation.Reset();
            currentAnimation = Animations[name];
            //Console.WriteLine("Current animation name is: " + currentAnimation.Name);
        }

        /// <summary>
        /// Change the frame of the current animation.
        /// </summary>
        /// <param name="frame">Frame to change to.</param>
        public void SetFrame(int frame) {
            currentAnimation.currentFrame = frame;
        }

        /// <summary>
        /// Doesn't matter when this is called as long as it's before Sprite.Draw.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void PreDraw(SpriteBatch spriteBatch) {
            sprite.SetRectangle(currentAnimation.GetRectangle());
            //Console.WriteLine("AnimationController draw called! frame = " + currentAnimation.GetRectangle());
        }
    }
}

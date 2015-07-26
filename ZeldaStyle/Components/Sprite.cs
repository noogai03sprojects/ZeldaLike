using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZeldaStyle.Components
{
    public class Sprite : Component
    {
        private Texture2D sprite;
        private int width;
        private int height;
        public bool visible = true;
        private Vector2 position;

        private Nullable<Rectangle> textureRect;

        private SpriteEffects effects;

        public Sprite(Texture2D texture, int width, int height, Vector2 position) {
            this.sprite = texture;
            this.width = width;
            this.height = height;
            this.position = position;

            textureRect = null;
            //textureRect = new Rectangle(20, 20, 50, 50);
        }
        public Sprite(Texture2D texture, Vector2 position) {
            this.sprite = texture;
            this.width = texture.Width;
            this.height = texture.Height;
            this.position = position;

            //textureRect = new Rectangle(20, 20, 50, 50);
        }

        public void Move(float X, float Y) {
            position.X += X;
            position.Y += Y;
        }

        public override void Update(float deltaTime) {
            
        }

        public void SetFlipped(bool flipped) {
            effects = flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public void SetRectangle(Rectangle rect) {
            textureRect = rect;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            //Console.WriteLine(position);
            if (visible)
                spriteBatch.Draw(sprite, position, textureRect, Color.White, 0, Vector2.Zero, 1, effects, 0);
        }
    }
}

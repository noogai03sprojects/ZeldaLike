using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using ZeldaStyle.CustomEventArgs;

namespace ZeldaStyle
{
    class ManagerInput : Component
    {
        //private static KeyboardState s_keyState;
        //private static List<Input> pressedInputs;
        private KeyboardState keyState;
        private KeyboardState lastKeyState;

        private Keys lastKey;

        /// <summary>
        /// If set, blocks user input from ever reaching its components. e.g. for cutscenes.
        /// </summary>
        public static bool BlockInput { get; set; }

        /// <summary>
        /// Internal event to be fired; not accessed directly
        /// </summary>
        private static event EventHandler<NewInputEventArgs> fireNewInput;

        /// <summary>
        /// Public version to be accessed by rest of code
        /// </summary>
        public static event EventHandler<NewInputEventArgs> FireNewInput {
            add {
                fireNewInput += value;
            }
            remove {
                fireNewInput -= value;
            }
        }

        public ManagerInput() {
            BlockInput = false;
        }

        public override void Update(float deltaTime) {
            lastKeyState = keyState;
            keyState = Keyboard.GetState();
            //s_keyState = keyState;

            checkKeyState(Keys.Left, Input.Left);
            checkKeyState(Keys.Up, Input.Up);
            checkKeyState(Keys.Right, Input.Right);
            checkKeyState(Keys.Down, Input.Down);
            checkKeyState(Keys.A, Input.A);
            checkKeyState(Keys.B, Input.B);
        }        

        /// <summary>
        /// Internally check if a key is pressed, and if it is, fire an event to all attached Components with Input packaged in the EventArgs.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fireInput"></param>
        private void checkKeyState(Keys key, Input fireInput) {
            if (keyState.IsKeyDown(key)) {
                if (!BlockInput) {
                    if (fireNewInput != null) {
                        fireNewInput(this, new NewInputEventArgs(fireInput));
                        lastKey = key;
                    }
                }
            }
        }

        //public static bool KeyDown(Keys key) {
            //return s_keyState.IsKeyDown(key);
        //}
    }
}

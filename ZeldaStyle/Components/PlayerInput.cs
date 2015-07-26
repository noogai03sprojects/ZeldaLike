using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ZeldaStyle.CustomEventArgs;

namespace ZeldaStyle.Components {
    public class PlayerInput : Component {
        private float moveSpeed = 2.0f;


        /// <summary>
        /// Direction player is facing. Use ChangeDirection.
        /// </summary>
        public Direction PlayerDirection { get; private set; }

        /// <summary>
        /// Current state of player. Do not change; use ChangeState.
        /// </summary>
        public PlayerState State { get; private set; }

        /// <summary>
        /// Reference to AnimationController component.
        /// </summary>
        private AnimationController animController;

        /// <summary>
        /// Reference to Sprite component.
        /// </summary>
        private Sprite sprite;

        /// <summary>
        /// For tracking if player is moving
        /// </summary>
        private bool movingFlag;

        /// <summary>
        /// For handling the situations where multiple movement keys are held down.
        /// </summary>
        private HashSet<Input> inputs;

        /// <summary>
        /// Same as inputs.
        /// </summary>
        private HashSet<Input> lastInputs;


        public PlayerInput() {
            ManagerInput.FireNewInput += new EventHandler<NewInputEventArgs>(ManagerInput_FireNewInput);
        }

        /// <summary>
        /// Called internally.
        /// </summary>
        /// <param name="_object"></param>
        public override void Initialize(GameObject _object) {
            base.Initialize(_object);
            animController = GetComponent<AnimationController>();
            sprite = GetComponent<Sprite>();
            PlayerDirection = Direction.Down;
            State = PlayerState.Standing;
            inputs = new HashSet<Input>();
            lastInputs = new HashSet<Input>();
        }


        public override void StartUpdate(float deltaTime) {
            //for tracking whether player is moving
            movingFlag = false;            
            //Console.WriteLine("moving flag set to false");
            inputs.Clear();
        }

        public override void LastUpdate(float deltaTime) {
            if (!movingFlag) {
                ChangeState(PlayerState.Standing);
            }
            if (State == PlayerState.Walking && !lastInputs.Contains(directionToInput(PlayerDirection))) {
                ChangeDirection(inputToDirection(lastInputs.First()));
            }
            lastInputs = inputs;
        }
        
        public override void Awake() {
            ChangeDirection(Direction.Down);
            ChangeState(PlayerState.Standing);
        
            base.Awake();
        }

        /// <summary>
        /// Hooks onto the input chain to process input as it arrives.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ManagerInput_FireNewInput(object sender, NewInputEventArgs e) {            
            
            //Sprite sprite = GetComponent<Sprite>();

            //if (sprite == null)
                //throw new Exception("GetComponent went wrong!");
            
            //tell program that player has moved this frame
            movingFlag = true;

            //add current input to HashSet
            inputs.Add(e.Input);

            //move the character
            switch (e.Input) {
                case Input.Left:
                    sprite.Move(-moveSpeed, 0);
                    break;
                case Input.Right:
                    sprite.Move(moveSpeed, 0);
                    break;
                case Input.Up:
                    sprite.Move(0, -moveSpeed);
                    break;
                case Input.Down:
                    sprite.Move(0, moveSpeed);
                    break;
            }

            //ie already moving in a direction keep his animation playing
            if (State != PlayerState.Standing) {
                return;
            }

            //handle direection changes and hence play animations 
            switch (e.Input) {
                case Input.Left:
                    
                    //sprite.Move(-moveSpeed, 0);
                    ChangeDirection(Direction.Left);
                    ChangeState(PlayerState.Walking);
                    break;
                case Input.Right:
                    
                    //sprite.Move(moveSpeed, 0);
                    ChangeDirection(Direction.Right);
                    ChangeState(PlayerState.Walking);
                    break;
                case Input.Up:
                    
                    //sprite.Move(0, -moveSpeed);
                    ChangeDirection(Direction.Up);
                    ChangeState(PlayerState.Walking);
                    break;
                case Input.Down:
                    
                    //sprite.Move(0, moveSpeed);
                    ChangeDirection(Direction.Down);
                    ChangeState(PlayerState.Walking);
                    break;
                case Input.A:
                    break;
                case Input.B:
                    break;
            }
        }

        /// <summary>
        /// Helper function.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <returns></returns>
        private Direction inputToDirection(Input input) {
            switch (input) {
                case Input.Up:
                    return Direction.Up;
                    
                case Input.Down:
                    return Direction.Down;
                    
                case Input.Left:
                    return Direction.Left;

                case Input.Right:
                    return Direction.Right;

                default:
                    return Direction.None;
            }
        }

        /// <summary>
        /// Helper function.
        /// </summary>
        /// <param name="direction">Direction to convert.</param>
        /// <returns></returns>
        private Input directionToInput(Direction direction) {
            switch (direction) {
                case Direction.Up:
                    return Input.Up;

                case Direction.Down:
                    return Input.Down;

                case Direction.Left:
                    return Input.Left;

                case Direction.Right:
                    return Input.Right;

                default:
                    return Input.None;
            }
        }

        /// <summary>
        /// Changes the state of the player.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(PlayerState state) {
            switch (state) {
                case PlayerState.Standing:
                    this.State = PlayerState.Standing;

                    //switch to stand animation and pause
                    animController.Running = false;
                    animController.SwitchAnimation("stand");
                    
                    sprite.SetFlipped(false);

                    //Make the player sprite face in whatever direction he is facing
                    switch (PlayerDirection) {
                        case Direction.Down:
                            animController.SetFrame(0);
                            break;
                        case Direction.Left:
                            animController.SetFrame(1);
                            //animController.
                            break;
                        case Direction.Up:
                            animController.SetFrame(2);
                            break;
                        case Direction.Right:
                            animController.SetFrame(1);
                            sprite.SetFlipped(true);
                            break;
                    }
                    break;
                case PlayerState.Walking:
                    animController.Running = true;
                    this.State = PlayerState.Walking;
                    //ChangeDirection(PlayerDirection);
                    break;
            }
        }


        /// <summary>
        /// Changes Direction of player.
        /// </summary>
        /// <param name="direction"></param>
        public void ChangeDirection(Direction direction) {
            //this.direction = direction;

            if (this.PlayerDirection != direction) {
                this.PlayerDirection = direction;
                sprite.SetFlipped(false);

                switch (direction) {
                    case Direction.Down:
                        animController.SwitchAnimation("walk_down");
                        break;
                    case Direction.Left:
                        animController.SwitchAnimation("walk_side");
                        sprite.SetFlipped(false);
                        break;
                    case Direction.Right:
                        animController.SwitchAnimation("walk_side");
                        sprite.SetFlipped(true);
                        break;
                    case Direction.Up:
                        animController.SwitchAnimation("walk_up");
                        break;
                }
            }

            Console.WriteLine("Direction changed to " + PlayerDirection);
        }

        public override void Update(float deltaTime) {
            //Console.WriteLine("PlayerInput Update!");
            //ChangeState(PlayerState.Standing);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            
        }
    }
}

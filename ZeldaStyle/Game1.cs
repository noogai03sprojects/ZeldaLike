#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ZeldaStyle.Components;
using ZeldaStyle.Animations;
using ZeldaStyle.Map;
#endregion

namespace ZeldaStyle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameObject player;
        ManagerInput managerInput;

        RenderTarget2D rt;

        MapManager mapManager;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 320;
            this.graphics.PreferredBackBufferHeight = 240;

            rt = new RenderTarget2D(GraphicsDevice, 160, 120);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new GameObject();
            managerInput = new ManagerInput();
            mapManager = new MapManager();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.AddComponent(new Sprite(Content.Load<Texture2D>("linkSheet.png"), new Vector2(10, 10)));
            

            AnimationController controller = new AnimationController();
            player.AddComponent(controller);

            PlayerInput input = (PlayerInput)player.AddComponent(new PlayerInput());

            controller.LoadAnimation("json\\walk_down.json");
            controller.LoadAnimation("json\\walk_side.json");
            controller.LoadAnimation("json\\walk_up.json");
            controller.LoadAnimation("json\\stand.json");
            controller.Initialize();            
            input.Awake();

            //mapManager.LoadMap("Content\\maps\\map1", Content);
           
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.StartUpdate(dt);
            managerInput.Update(dt);
            //Console.WriteLine(gameTime.ElapsedGameTime.TotalSeconds);
            

            player.Update(dt);

            player.EndUpdate(dt);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(rt);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, null, null);

            spriteBatch.Draw(rt, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2f, 0, 0f);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

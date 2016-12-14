using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameHSeSurvival
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont Font;
        
        private Camera camera;
        private Enemies_Repository repo = new Enemies_Repository();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera(GraphicsDevice.Viewport);
            Texture2D player_texture = Content.Load<Texture2D>("студент.png");
            Texture2D block_texture = Content.Load<Texture2D>("блок.png");
            Texture2D teacher1_texture = Content.Load<Texture2D>("учитель.png");
            Texture2D teacher2_texture = Content.Load<Texture2D>("учительница.png");
            Texture2D coin_texture = Content.Load<Texture2D>("монетка.png");
            repo.SetValues(player_texture, block_texture, teacher1_texture, teacher2_texture, coin_texture, spriteBatch);
            Font = Content.Load<SpriteFont>("Font");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
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

            base.Update(gameTime);
            repo.Player.Update(gameTime);
            camera.Update(repo.Player.Sprite_vector, repo.Board.columns * 64, repo.Board.rows * 64);
            repo.Board.Update();
            repo.CollisionsTeachers(repo.Player);
            repo.CollisionsCoins();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            string helloWords = string.Format("Created by\nNastya Kostina\nand\nVasiliy Sdobnov\n\n\nWe are so glad\nyou decided to play\nthis disaster.\n\n\nSpace - Jump\n<- - Move Left\n-> - Move Right");//\nWe are so glad\nyou decided to play\nthis disaster.\nPress right and left\n\to replace Mario.\nSpace for jumping.");

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);
            base.Draw(gameTime);
            repo.Draw(spriteBatch, Font);
            spriteBatch.End();
        }
    }
}

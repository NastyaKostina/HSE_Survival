using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        private Repository repo = new Repository();
        private Texture2D background;

        public int finalScore;
        public double finalTime;
        public bool wonTheGame = false;
        
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
            this.IsMouseVisible = true;
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

            Dictionary<string, Texture2D> Values = new Dictionary<string, Texture2D>();
            Values.Add("студент", Content.Load<Texture2D>("студент.png"));
            Values.Add("блок", Content.Load<Texture2D>("блок.png"));
            Values.Add("учитель", Content.Load<Texture2D>("учитель.png"));
            Values.Add("учительница", Content.Load<Texture2D>("учительница.png"));
            Values.Add("монетка", Content.Load<Texture2D>("монетка.png"));
            Values.Add("шапочка", Content.Load<Texture2D>("шапочка.png"));
            Values.Add("бомба",Content.Load<Texture2D>("бомба.png"));
            background = Content.Load<Texture2D>("fon.png");

            repo.SetValues(Values,spriteBatch);
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
            camera.Update(repo.Player.Sprite_vector, repo.Board.columns * 64, repo.Board.rows * 64);
            repo.Player.Update(gameTime);
            repo.Board.Update();
            foreach (var bomb in repo.Bombs) bomb.Update(gameTime);
            repo.Collisisons(gameTime);
            if (repo.Hat.Collision(repo.Player))
            {
                wonTheGame = true;
                Exit();
            }
            finalScore = repo.Player.Score;
            finalTime += gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);
            base.Draw(gameTime);
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            repo.Draw(spriteBatch, Font, gameTime);
            spriteBatch.End();
        }
    }
}

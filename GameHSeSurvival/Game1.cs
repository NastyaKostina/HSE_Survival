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

        const int ground_level = 576;

        private Player player;
        private Board board;
        private Camera camera;
        private Enemies_Repository repo = new Enemies_Repository();

        private SpriteFont debugfont;
        
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
            player = new Player(player_texture, new Vector2(550, ground_level - player_texture.Height), spriteBatch); //sounds
            Texture2D block_texture = Content.Load<Texture2D>("блок.png");
            board = new Board(spriteBatch, block_texture, 87, 10);

            Texture2D teacher1 = Content.Load<Texture2D>("учитель.png");
            Texture2D teacher2 = Content.Load<Texture2D>("учительница.png");
            var teacher_1 = new Teacher(teacher1, new Vector2(3840, ground_level - teacher1.Height), spriteBatch);
            repo.Teachers.Add(teacher_1);
            var teacher_2 = new Teacher(teacher2, new Vector2(3200, ground_level - teacher2.Height), spriteBatch);
            repo.Teachers.Add(teacher_2);
            var teacher_3 = new Teacher(teacher2, new Vector2(4544, ground_level - teacher2.Height - 192), spriteBatch);
            repo.Teachers.Add(teacher_3);
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
            player.Update(gameTime);
            camera.Update(player.Sprite_vector, board.columns * 64, board.rows * 64);
            board.Update();
            repo.Collisions(player);
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
            board.Draw();
            player.Draw();
            repo.DrawTeacher();
            spriteBatch.End();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_HSE
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D monster;
        Texture2D mario, ground;
        Vector2 position = new Vector2(200, 420);
        Vector2 positionmario = new Vector2(50, 420 - 32), positionground = new Vector2(-1, 452);
        Vector2 bul_pos = new Vector2(70, 450 - 32);
        float speed = 4f;
        float speendmario = 5f;
        Color color = Color.CornflowerBlue;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            monster = Content.Load<Texture2D>("monster");
            mario = Content.Load<Texture2D>("mario");
            ground = Content.Load<Texture2D>("ground");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            position.X += speed;
            if (position.X > ((Window.ClientBounds.Width - monster.Width) - 200) || position.X - 100 < 0)
                speed *= -1;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                positionmario.X -= speendmario;
                bul_pos.X -= speendmario;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                positionmario.X += speendmario;
                bul_pos.X += speendmario;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                
            }

            if (positionmario.X < 0)
            {
                positionmario.X = 0;
            }
            if (positionmario.X > Window.ClientBounds.Width - mario.Width)
            {
                positionmario.X = Window.ClientBounds.Width - mario.Width;
            }

            if (Collide())
            {
                GraphicsDevice.Clear(Color.Red);
            }
            else
                color = Color.CornflowerBlue;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            spriteBatch.Begin();
            spriteBatch.Draw(monster, position, Color.White);
            spriteBatch.Draw(mario, positionmario, Color.White);
            spriteBatch.Draw(ground, positionground, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool Collide()
        {
            Rectangle mario1 = new Rectangle((int)positionmario.X,
                (int)positionmario.Y, mario.Width, mario.Height);
            Rectangle monster1 = new Rectangle((int)position.X,
                (int)position.Y, monster.Width, monster.Height);

            return mario1.Intersects(monster1);
        }
    }
}

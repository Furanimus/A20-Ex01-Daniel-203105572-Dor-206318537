using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;
       
        private Texture2D m_Background;
        private const int k_BGWidth = 1024;
        private const int k_BGHeight = 768;

        //Player Ship parameters
        private Texture2D m_PlayerShip;
        private Vector2 m_PlayerShipPosition;
        private int m_PlayerLives = 3;
        private bool m_IsDead = false;
        private const int k_PlayerVelocity = 110;
        private const int k_PlayerShipWidth = 32;
        private const int k_PlayerShipHeight = 32;

        //Enemies parameters
        private const int k_EnemyWidth = 32;
        private const int k_EnemyHeight = 32;

        //Mothership parameters
        private const int k_MothershipWidth = 120;
        private const int k_MothershipHeight = 32;
        
        //Bullets - class


        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.PreferredBackBufferWidth = k_BGWidth;  // set this value to the desired width of your window
            m_Graphics.PreferredBackBufferHeight = k_BGHeight;   // set this value to the desired height of your window
            m_Graphics.ApplyChanges();
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
            m_PlayerShipPosition = new Vector2(k_BGWidth - 64, k_BGHeight - 64); // Starting Position of player ship

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            m_Background = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_PlayerShip = Content.Load<Texture2D>(@"Sprites\Ship01_32x32");


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // UpdateLeftmostEnemyBound();
            // UpdateRightmostEnemyBound();
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
            {
                if (m_PlayerShipPosition.X <= k_BGWidth - k_PlayerShipWidth)
                {
                    m_PlayerShipPosition.X += k_PlayerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (state.IsKeyDown(Keys.Left))
            {
                if (m_PlayerShipPosition.X >= 0)
                {
                    m_PlayerShipPosition.X -= k_PlayerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            m_SpriteBatch.Begin();
            m_SpriteBatch.Draw(m_Background, new Vector2(0, 0), Color.White);
            m_SpriteBatch.End();

            m_SpriteBatch.Begin();
            m_SpriteBatch.Draw(m_PlayerShip, m_PlayerShipPosition, Color.White);
            m_SpriteBatch.End();


            
            base.Draw(gameTime);
        }
    }
}

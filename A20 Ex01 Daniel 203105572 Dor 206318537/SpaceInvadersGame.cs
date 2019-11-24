using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : Game
     {
          private const int k_EnemiesRows = 5;
          private const int k_EnemiesCols = 9;
          private readonly GameEnvironment r_GameEnvironment;
          private readonly EntityFactory r_EntityFactory;
          private readonly Player r_Player;
          private readonly Enemy[,] r_Enemies;
          private readonly GraphicsDeviceManager m_Graphics;
          private EnemyManager m_EnemyManager;
          private SpriteBatch m_SpriteBatch;
          private int m_EnemyDeathCounter = 0; //TODO

          public SpaceInvadersGame()
          {
               m_Graphics = new GraphicsDeviceManager(this);
               r_GameEnvironment = new GameEnvironment();
               r_EntityFactory = new EntityFactory(r_GameEnvironment);

               m_Graphics.PreferredBackBufferWidth = r_GameEnvironment.WindowWidth;
               m_Graphics.PreferredBackBufferHeight = r_GameEnvironment.WindowHeight;
               r_Player = r_EntityFactory.Create(typeof(Player)) as Player;
               r_Enemies = new Enemy[k_EnemiesRows, k_EnemiesCols];

               m_Graphics.ApplyChanges();
               Content.RootDirectory = "Content";
          }

          protected override void Initialize()
          {
               base.Initialize();
          }

          protected override void LoadContent()
          {
               m_SpriteBatch = new SpriteBatch(GraphicsDevice);

               r_GameEnvironment.Background = Content.Load<Texture2D>(r_GameEnvironment.BackgroundPath);
               r_Player.Graphics = Content.Load<Texture2D>(r_Player.GraphicsPath);
               r_Player.Position = new Vector2(r_GameEnvironment.WindowWidth - r_Player.Width * 2,
                    r_GameEnvironment.WindowHeight - r_Player.Height * 2);

               m_EnemyManager = new EnemyManager(Content, k_EnemiesRows, k_EnemiesCols);
          }

          protected override void UnloadContent()
          {
          }

          protected override void Update(GameTime i_GameTime)
          {
               if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
               Exit();

               r_Player.KeyboardState = Keyboard.GetState();
               r_Player.GameTime = i_GameTime;
               r_Player.Move();

               base.Update(i_GameTime);
          }

          protected override void Draw(GameTime gameTime)
          {
               GraphicsDevice.Clear(Color.White);

               m_SpriteBatch.Begin();
               m_SpriteBatch.Draw(r_GameEnvironment.Background, new Vector2(0, 0), Color.White);
               m_SpriteBatch.End();

               m_SpriteBatch.Begin();
               m_SpriteBatch.Draw(r_Player.Graphics, r_Player.Position, Color.White);
               m_SpriteBatch.End();

               m_EnemyManager.Draw(m_SpriteBatch);

               base.Draw(gameTime);
          }
     }
}

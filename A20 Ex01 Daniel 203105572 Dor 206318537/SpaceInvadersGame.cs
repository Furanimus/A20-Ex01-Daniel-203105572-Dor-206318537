using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : Game
     {
          private readonly GameEnvironment r_GameEnvironment;
          private readonly EntityFactory r_EntityFactory;
          private readonly Player r_Player;
          private readonly Enemy[,] r_Enemies;
          private const int k_EnemiesRows = 5;
          private const int k_EnemiesCols = 9;
          private const int k_NumOfPinkEnemiesRows = 1;
          private const int k_NumOfLightBlueEnemiesRows = 2;
          private const int k_NumOfYellowEnemiesRows = 2;
          private const float k_EnemiesOffset = 0.6f;

          GraphicsDeviceManager m_Graphics;
          SpriteBatch m_SpriteBatch;

          public SpaceInvadersGame()
          {
               m_Graphics = new GraphicsDeviceManager(this);
               r_GameEnvironment = new GameEnvironment();
               r_EntityFactory = new EntityFactory(r_GameEnvironment);

               m_Graphics.PreferredBackBufferWidth = r_GameEnvironment.WindowWidth;  // set this value to the desired width of your window
               m_Graphics.PreferredBackBufferHeight = r_GameEnvironment.WindowHeight;   // set this value to the desired height of your window
               r_Player = r_EntityFactory.Create(typeof(Player)) as Player;
               r_Enemies = new Enemy[5, 9];

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

               float enemyY = 96;
               float enemyX = 0;

               for(int row = 0; row < k_EnemiesRows; row++)
               {
                    for(int col = 0; col < k_EnemiesCols; col++)
                    {
                         if(row < k_NumOfPinkEnemiesRows)
                         {
                              r_Enemies[row, col] = r_EntityFactory.Create(typeof(EnemyPink)) as Enemy;
                         }
                         else if(row < k_NumOfPinkEnemiesRows + k_NumOfLightBlueEnemiesRows)
                         {
                              r_Enemies[row, col] = r_EntityFactory.Create(typeof(EnemyLightBlue)) as Enemy;
                         }
                         else
                         {
                              r_Enemies[row, col] = r_EntityFactory.Create(typeof(EnemyYellow)) as Enemy;
                         }

                         r_Enemies[row, col].Graphics = Content.Load<Texture2D>(r_Enemies[row, col].GraphicsPath);
                         r_Enemies[row, col].Position = new Vector2(enemyX, enemyY);
                         enemyX += r_Enemies[row, col].Width + r_Enemies[row, col].Width * k_EnemiesOffset;
                    }

                    enemyY += r_Enemies[row, 0].Height + r_Enemies[row, 0].Height * k_EnemiesOffset;
                    enemyX = 0;
               }
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

               for(int row = 0; row < 5; row++)
               {
                    for (int col = 0; col < 9; col++)
                    {
                         m_SpriteBatch.Begin();

                         if (row < 1)
                         {
                              m_SpriteBatch.Draw(r_Enemies[row, col].Graphics, r_Enemies[row, col].Position, Color.Pink);
                         }
                         else if(row < 3)
                         {
                              m_SpriteBatch.Draw(r_Enemies[row, col].Graphics, r_Enemies[row, col].Position, Color.LightBlue);
                         }
                         else
                         {
                              m_SpriteBatch.Draw(r_Enemies[row, col].Graphics, r_Enemies[row, col].Position, Color.LightYellow);
                         }

                         m_SpriteBatch.End();
                    }
               }

               base.Draw(gameTime);
          }
     }
}

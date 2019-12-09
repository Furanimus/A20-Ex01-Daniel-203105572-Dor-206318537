using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {
          private const int k_EnemiesRows = 5;
          private const int k_EnemiesCols = 9;
          private readonly GameEnvironment r_GameEnvironment;
          private readonly ISpriteFactory r_SpriteFactory;
          private readonly Player r_Player;
          private readonly GraphicsDeviceManager r_Graphics;
          private SpriteBatch m_SpriteBatch;

          public SpaceInvadersGame()
          {
               r_Graphics = new GraphicsDeviceManager(this);
               Content.RootDirectory = "Content";

               r_GameEnvironment = Singelton<GameEnvironment>.Instance;
               r_SpriteFactory = Singelton<SpriteFactory>.Instance;
               r_SpriteFactory.Game = this;

               r_Graphics.PreferredBackBufferWidth = r_GameEnvironment.WindowWidth;
               r_Graphics.PreferredBackBufferHeight = r_GameEnvironment.WindowHeight;
               r_Player = r_SpriteFactory.Create(typeof(Player)) as Player;

               r_Graphics.ApplyChanges();
          }

          public EnemyManager EnemyManager { get; private set; }

          protected override void Initialize()
          {
               IsMouseVisible = true;
               m_SpriteBatch = new SpriteBatch(GraphicsDevice);
               EnemyManager = new EnemyManager(this, k_EnemiesRows, k_EnemiesCols);
               r_GameEnvironment.Background = new Background(this);

               this.Components.Add(r_GameEnvironment.Background);
               this.Components.Add(EnemyManager.MotherShip);
               this.Components.Add(r_Player);

               foreach (Enemy enemy in EnemyManager.EnemiesMatrix)
               {
                    this.Components.Add(enemy);
               }

               base.Initialize();
          }

          protected override void LoadContent()
          {
               r_Player.SpriteBatch = m_SpriteBatch;

               foreach (IGameComponent component in this.Components)
               {
                    (component as Sprite).SpriteBatch = m_SpriteBatch;
               }

               base.LoadContent();
          }

          protected override void Update(GameTime i_GameTime)
          {
               bool isGameOver = CheckIfGameOver();
               if (isGameOver || Keyboard.GetState().IsKeyDown(Keys.Escape))
               {
                    // if (isGameOver)
                    // {
                    // //DisplayExitMsg();
                    // }
                    Exit();
               }

               GameTime = i_GameTime;

               EnemyManager.UpdateMatrixDirection();
               EnemyManager.EnemiesTryAttack();
               EnemyManager.DestroyPlayerIfBulletsOrEnemiesCollidedWithPlayer(r_Player);

               Window.Title = r_Player.Score.ToString();

               base.Update(i_GameTime);
          }

          protected override void Draw(GameTime gameTime)
          {
               GraphicsDevice.Clear(Color.White);
               m_SpriteBatch.Begin();
               base.Draw(gameTime);
               m_SpriteBatch.End();
          }

          private bool CheckIfGameOver()
          {
               return r_Player.Lives == 0 || EnemyManager.IsEnemyCollidedWithPlayer;
          }
     }
}
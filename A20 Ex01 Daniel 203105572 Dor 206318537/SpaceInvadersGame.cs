using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex01_Daniel_203105572_Dor_206318537.Enums;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {
          private const int k_EnemiesRows = 5;
          private const int k_EnemiesCols = 9;
          //private readonly ISpriteFactory r_SpriteFactory;
          private Player m_Player;
          private readonly GraphicsDeviceManager r_Graphics;
          private readonly Background r_Background;
          private SpriteBatch m_SpriteBatch;
          MonitorForm m_MonitorForm;

          public SpaceInvadersGame()
          {
               r_Graphics = new GraphicsDeviceManager(this);
               Content.RootDirectory = "Content";
               r_Background = new Background(this);

               r_Graphics.PreferredBackBufferWidth = (int)r_Background.Width;
               r_Graphics.PreferredBackBufferHeight = (int)r_Background.Height;
               //r_SpriteFactory = Singelton<SpriteFactory>.Instance;
               //r_SpriteFactory.Game = this;

               //r_SpriteFactory.Create(typeof(Player)) as Player;
               m_MonitorForm = new MonitorForm();
               r_Graphics.ApplyChanges();
          }

        //  public EnemyManager EnemyManager { get; private set; }

          protected override void Initialize()
          {

               m_SpriteBatch = new SpriteBatch(GraphicsDevice);
               m_Player = new Player(this, Keys.H, Keys.K, Keys.U, true);
               m_MonitorForm.Show();
               //EnemyManager = new EnemyManager(this, k_EnemiesRows, k_EnemiesCols);
               //r_GameEnvironment.Background = new Background(this);

               //this.Components.Add(r_GameEnvironment.Background);
               //this.Components.Add(EnemyManager.MotherShip);
               base.Initialize();

               //foreach (Enemy enemy in EnemyManager.EnemiesMatrix)
               //{
               //     this.Components.Add(enemy);
               //}
          }

          protected override void LoadContent()
          {

               foreach (GameComponent component in this.Components)
               {
                    if (component is Sprite)
                    {
                         (component as Sprite).SpriteBatch = m_SpriteBatch;
                    }
               }

               base.LoadContent();
          }

          protected override void Update(GameTime i_GameTime)
          {
               bool isGameOver = CheckIfGameOver();

               if (isGameOver || Keyboard.GetState().IsKeyDown(Keys.Escape))
               {
                    if (isGameOver)
                    {
                         DisplayExitMsg();
                    }

                    Exit();
               }

               //EnemyManager.UpdateMatrixDirection();
               //EnemyManager.EnemiesTryAttack();
               //EnemyManager.DestroyPlayerIfBulletsOrEnemiesCollidedWithPlayer(r_Player);

               Window.Title = "Space Invaders - Score: " + m_Player.Score + " | Lives: " + m_Player.Lives;


               base.Update(i_GameTime);
          }

          private void DisplayExitMsg()
          {
               string score = "Your score is: " + m_Player.Score.ToString();
               string title = string.Empty;

               //if (r_Player.Lives == 0 || EnemyManager.IsEnemyCollidedWithPlayer)
               //{
               //     title = "Game over";
               //}
               //else if (EnemyManager.EnemiesDestroyed == EnemyManager.StartingEnemyCount)
               //{
               //     title = "You won";
               //}

               System.Windows.Forms.MessageBox.Show(score, title);
          }

          protected override void Draw(GameTime gameTime)
          {
               GraphicsDevice.Clear(Color.White);
               m_SpriteBatch.Begin();
               base.Draw(gameTime);
               m_SpriteBatch.End();

               string inputToString = (this.Services.GetService(typeof(IInputManager)) as IInputManager).ToString();
               m_MonitorForm.MonitorText = inputToString;
          }

          private bool CheckIfGameOver()
          {
               return false;
               //return r_Player.Lives == 0 || EnemyManager.IsEnemyCollidedWithPlayer || 
               //     EnemyManager.EnemiesDestroyed == EnemyManager.StartingEnemyCount;
          }
     }
}
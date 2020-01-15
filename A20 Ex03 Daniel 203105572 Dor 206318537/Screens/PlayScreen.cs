using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Screens
{
     public class PlayScreen : GameScreen
     {
          private const string k_GameTitle = "Space Invaders";
          private const string k_GameOverTitle = "Game Over";
          private const string k_WinnerMsg = "The winner is Player {0}!";
          private const string k_TieMsg = "It's a tie!";
          private const string k_GameOverMsg = @"Player 1 Score is: {0}.
Player 2 Score is: {1}.
{2}";
          private LivesManager m_LivesManager;
          private ScoreManager m_ScoreManager;
          private InputManager m_InputManager;
          private Player m_Player1;
          private Player m_Player2;
          private readonly Background r_Background;
          private readonly GraphicsDeviceManager r_Graphics;
          private EnemyManager m_EnemyManager;
          private BarrierManager m_BarrierManager;

          public PlayScreen(Game i_Game) 
               : base(i_Game)
          {
               r_Graphics = new GraphicsDeviceManager(this.Game);
               r_Background = new Background(this);
               r_Graphics.PreferredBackBufferWidth = (int)r_Background.Width;
               r_Graphics.PreferredBackBufferHeight = (int)r_Background.Height;
               r_Graphics.ApplyChanges();
               this.BlendState = BlendState.NonPremultiplied;
          }

          public override void Initialize()
          {
               initServices();
               initPlayers();
               initDrawableManagers();

               base.Initialize();
          }

          private void initDrawableManagers()
          {
               m_EnemyManager = new EnemyManager(this);
               m_BarrierManager = new BarrierManager(this, m_Player1.StartingPosition.Y, m_Player1.Height);

               m_EnemyManager.MatrixReachedBottomWindow += OnGameOver;
               m_EnemyManager.AllEnemiesDied += OnGameOver;
               m_LivesManager.AllPlayersDied += OnGameOver;
          }

          private void initPlayers()
          {
               m_Player1 = new Player(@"Sprites\Ship01_32x32", this);
               m_Player1.StartingPosition = new Vector2(GraphicsDevice.Viewport.Width - (m_Player1.Width * 2), GraphicsDevice.Viewport.Height - (m_Player1.Height * 2));
               m_Player1.IsMouseControllable = true;

               m_Player2 = new Player(@"Sprites\Ship02_32x32", this);
               m_Player2.StartingPosition = m_Player1.StartingPosition - new Vector2(m_Player2.Width, 0);
               m_Player2.MoveLeftKey = Microsoft.Xna.Framework.Input.Keys.A;
               m_Player2.MoveRightKey = Microsoft.Xna.Framework.Input.Keys.D;
               m_Player2.ShootKey = Microsoft.Xna.Framework.Input.Keys.W;
               m_Player2.GroupRepresentative = m_Player1;

               this.m_Player1.CollidedWithEnemy += OnGameOver;
               this.m_Player2.CollidedWithEnemy += OnGameOver;

               m_LivesManager.AddPlayer(m_Player1);
               m_LivesManager.AddPlayer(m_Player2);
               m_ScoreManager.AddPlayer(m_Player1, Color.Blue);
               m_ScoreManager.AddPlayer(m_Player2, Color.Green);
          }

          private void initServices()
          {
               m_LivesManager = new LivesManager(this);
               m_ScoreManager = new ScoreManager(this);
               m_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as InputManager;
          }

          private void OnGameOver()
          {
               string winMsg = getWinnerMsg();
               string message = string.Format(k_GameOverMsg, m_Player1.Score, m_Player2.Score, winMsg);
               System.Windows.Forms.MessageBox.Show(message, k_GameOverTitle, MessageBoxButtons.OK);
               this.Game.Exit();
          }

          private string getWinnerMsg()
          {
               string winner = null;

               if (m_Player1.Score > m_Player2.Score)
               {
                    winner = string.Format(k_WinnerMsg, 1);
               }
               else if (m_Player1.Score < m_Player2.Score)
               {
                    winner = string.Format(k_WinnerMsg, 2);
               }
               else
               {
                    winner = k_TieMsg;
               }

               return winner;
          }

          public override void Update(GameTime i_GameTime)
          {
               if (m_InputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
               {
                    this.Game.Exit();
               }

               this.Game.Window.Title = k_GameTitle;
               base.Update(i_GameTime);
          }

          public override void Draw(GameTime gameTime)
          {
               this.GraphicsDevice.Clear(Color.White);

               base.Draw(gameTime);
          }
     }
}

using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {
          private readonly Background r_Background;
          private readonly GraphicsDeviceManager r_Graphics;
          private Player m_Player1;
          private Player m_Player2;

          public SpaceInvadersGame()
          {
               Content.RootDirectory                = "Content";
               r_Background                         = new Background(this);
               r_Graphics                           = new GraphicsDeviceManager(this);
               r_Graphics.PreferredBackBufferWidth  = (int)r_Background.Width;
               r_Graphics.PreferredBackBufferHeight = (int)r_Background.Height;
               r_Graphics.ApplyChanges();
          }

          protected override void Initialize()
          {
               m_Player1 = new Player(@"Sprites\Ship01_32x32", this);
               m_Player1.StartingPosition      = new Vector2(GraphicsDevice.Viewport.Width - (m_Player1.Width * 2), GraphicsDevice.Viewport.Height - (m_Player1.Height * 2));
               m_Player1.IsMouseControllable   = true;
                                               
               m_Player2                       = new Player(@"Sprites\Ship02_32x32", this);
               m_Player2.StartingPosition      = m_Player1.StartingPosition - new Vector2(m_Player2.Width * 2, 0);
               m_Player2.MoveLeftKey           = Microsoft.Xna.Framework.Input.Keys.A;
               m_Player2.MoveRightKey          = Microsoft.Xna.Framework.Input.Keys.D;
               m_Player2.ShootKey              = Microsoft.Xna.Framework.Input.Keys.W;
               m_Player2.GroupRepresentative   = m_Player1;

               EnemyManager enemyManager = new EnemyManager(this);
               BarrierManager barrierManager   = new BarrierManager(this, m_Player1.StartingPosition.Y, m_Player1.Height);

               enemyManager.MatrixReachedBottomWindow += () => this.Exit();
               enemyManager.AllEnemiesDied += OnGameOver;

               this.LivesManager.AllPlayersDied += OnGameOver;
               this.LivesManager.AddPlayer(m_Player1);
               this.LivesManager.AddPlayer(m_Player2);
               this.ScoreManager.AddPlayer(m_Player1, Color.Blue);
               this.ScoreManager.AddPlayer(m_Player2, Color.Green);

               base.Initialize();
          }

          private void OnGameOver()
          {
               string title = "Game Over";
               string winMsg = getWinnerMsg();
               string message = string.Format(
@"Player 1 Score is: {0}.
Player 2 Score is: {1}.
{2}", 
m_Player1.Score, m_Player2.Score, winMsg);
               System.Windows.Forms.MessageBox.Show(message, title, MessageBoxButtons.OK);
               Exit();
          }

          private string getWinnerMsg()
          {
               string winner = null;

               if(m_Player1.Score > m_Player2.Score)
               {
                    winner = "the winner is Player 1!";
               }
               else if (m_Player1.Score < m_Player2.Score)
               {
                    winner = "The winner is Player 2!";
               }
               else
               {
                    winner = "It's a tie!";
               }

               return winner;
          }

          protected override void LoadContent()
          {
               base.LoadContent();
          }

          protected override void Update(GameTime i_GameTime)
          {
               if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
               {
                    Exit();
               }

               Window.Title = "Space Invaders";

               base.Update(i_GameTime);
          }

          protected override void Draw(GameTime gameTime)
          {
               this.GraphicsDevice.Clear(Color.White);
               this.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
               base.Draw(gameTime);
               this.SpriteBatch.End();
          }

     }
}
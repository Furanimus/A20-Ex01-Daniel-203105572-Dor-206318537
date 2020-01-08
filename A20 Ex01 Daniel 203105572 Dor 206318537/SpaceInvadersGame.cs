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
          private readonly Background r_Background;
          private readonly GraphicsDeviceManager r_Graphics;
          private Player m_Player1;
          private Player m_Player2;
          private EnemyManager m_EnemyManager;
          private SpriteBatch m_SpriteBatch;
          //MonitorForm m_MonitorForm;

          public SpaceInvadersGame()
          {
               //m_MonitorForm = new MonitorForm();
               Content.RootDirectory                = "Content";
               r_Background                         = new Background(this);
               r_Graphics                           = new GraphicsDeviceManager(this);
               r_Graphics.PreferredBackBufferWidth  = (int)r_Background.Width;
               r_Graphics.PreferredBackBufferHeight = (int)r_Background.Height;
               r_Graphics.ApplyChanges();
          }

          protected override void Initialize()
          {
               m_SpriteBatch                 = new SpriteBatch(GraphicsDevice);
               m_EnemyManager                = new EnemyManager(this);
               
               m_Player1                     = new Player(@"Sprites\Ship01_32x32", this);
               m_Player1.StartingPosition    = new Vector2(GraphicsDevice.Viewport.Width - (m_Player1.Width * 2), GraphicsDevice.Viewport.Height - (m_Player1.Height * 2));
               m_Player1.IsMouseControllable = true;

               m_Player2                     = new Player(@"Sprites\Ship02_32x32", this);
               m_Player2.StartingPosition    = m_Player1.StartingPosition - new Vector2(m_Player2.Width * 2, 0);
               m_Player2.MoveLeftKey         = Keys.A;
               m_Player2.MoveRightKey        = Keys.D;
               m_Player2.ShootKey            = Keys.W;
               //m_MonitorForm.Show();

               base.Initialize();
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
                    }

                    Exit();
               }

               Window.Title = "Space Invaders";

               base.Update(i_GameTime);
          }

          protected override void Draw(GameTime gameTime)
          {
               GraphicsDevice.Clear(Color.White);
               m_SpriteBatch.Begin();
               base.Draw(gameTime);
               m_SpriteBatch.End();

               string inputToString = (this.Services.GetService(typeof(IInputManager)) as IInputManager).ToString();
               //m_MonitorForm.MonitorText = inputToString;
          }

          private bool CheckIfGameOver()
          {
               return false;
          }
     }
}
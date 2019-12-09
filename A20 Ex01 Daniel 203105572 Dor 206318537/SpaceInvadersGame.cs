using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {          
          private const int k_EnemiesRows = 5;
          private const int k_EnemiesCols = 9;              
          private readonly GameEnvironment r_GameEnvironment;
          private readonly ISpriteFactory r_SpriteFactory;
          private readonly Player r_Player;
          private readonly GraphicsDeviceManager m_Graphics;
          private EnemyManager m_EnemyManager;
          private SpriteBatch m_SpriteBatch;

          public SpaceInvadersGame()
          {
               m_Graphics = new GraphicsDeviceManager(this);
               Content.RootDirectory = "Content";

               r_GameEnvironment = Singelton<GameEnvironment>.Instance;
               r_SpriteFactory = Singelton<SpriteFactory>.Instance;
               r_SpriteFactory.Game = this;

               m_Graphics.PreferredBackBufferWidth = r_GameEnvironment.WindowWidth;
               m_Graphics.PreferredBackBufferHeight = r_GameEnvironment.WindowHeight;
               r_Player = r_SpriteFactory.Create(typeof(Player)) as Player;
               
               m_Graphics.ApplyChanges();
          }
          public EnemyManager EnemyManager
          {
               get
               {
                    return m_EnemyManager;
               }
          }

          protected override void Initialize()
          {
               IsMouseVisible = true;
               m_SpriteBatch = new SpriteBatch(GraphicsDevice);
               m_EnemyManager = new EnemyManager(this, k_EnemiesRows, k_EnemiesCols);
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
               if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
               {
                    Exit();
               }

               GameTime = i_GameTime;
               r_Player.CurrKBState = Keyboard.GetState();
               r_Player.CurrMouseState = Mouse.GetState();
               m_EnemyManager.UpdateMatrixDirection();
               m_EnemyManager.EnemiesTryAttack();
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
     }
}
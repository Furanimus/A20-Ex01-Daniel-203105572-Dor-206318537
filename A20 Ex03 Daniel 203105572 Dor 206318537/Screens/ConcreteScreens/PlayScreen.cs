using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.BaseModels;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class PlayScreen : GameScreen
     {
          private const string k_GameTitle        = "Space Invaders";
          private const string k_Player1AssetName = @"Sprites\Ship01_32x32";
          private const string k_Player2AssetName = @"Sprites\Ship02_32x32";
          private readonly Background r_Background;
          private IPlayersManager m_PlayersManager;
          private IInputManager m_InputManager;
          private EnemyManager m_EnemyManager;
          private BarrierManager m_BarrierManager;
          private IGameSettings r_GameSettings;

          public PlayScreen(Game i_Game) 
               : base(i_Game)
          {
               r_GameSettings = this.Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;
               r_Background = new Background(this);
               r_GameSettings.GraphicsDeviceManager.PreferredBackBufferWidth = (int)r_Background.Width;
               r_GameSettings.GraphicsDeviceManager.PreferredBackBufferHeight = (int)r_Background.Height;
               r_GameSettings.GraphicsDeviceManager.ApplyChanges();
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
               m_BarrierManager = new BarrierManager(this, m_PlayersManager[0].StartingPosition.Y, m_PlayersManager[0].Height);

               m_EnemyManager.MatrixReachedBottomWindow += OnGameOver;
               m_EnemyManager.AllEnemiesDied            += OnGameOver;
               m_PlayersManager.AllPlayersDied          += OnGameOver;
          }

          private void initPlayers()
          {
               m_PlayersManager.AddPlayer(k_Player1AssetName);
               m_PlayersManager.AddPlayer(k_Player2AssetName);
               
               Player player2              = m_PlayersManager.GetLastAddedPlayer() as Player;
               player2.MoveLeftKey         = Microsoft.Xna.Framework.Input.Keys.A;
               player2.MoveRightKey        = Microsoft.Xna.Framework.Input.Keys.D;
               player2.ShootKey            = Microsoft.Xna.Framework.Input.Keys.W;
               player2.RepresentativeColor = Color.Green;
               player2.GroupRepresentative = m_PlayersManager[0];
          }

          private void initServices()
          {
               m_PlayersManager = new PlayersManager(this);
               m_PlayersManager.PlayerCollided += playersManager_PlayerCollided;
               m_InputManager   = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
          }

          private void playersManager_PlayerCollided(BasePlayer i_Player, ICollidable2D i_CollidedWith)
          {
               Enemy enemy = i_CollidedWith as Enemy;

               if(enemy != null)
               {
                    OnGameOver();
               }
          }

          private void OnGameOver()
          {
               this.Game.Exit();
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
     }
}

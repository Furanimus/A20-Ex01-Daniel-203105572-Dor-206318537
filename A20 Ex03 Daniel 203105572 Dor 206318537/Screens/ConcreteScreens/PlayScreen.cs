using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using Microsoft.Xna.Framework.Input;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{
     public class PlayScreen : GameScreen
     {
          private const string k_GameTitle        = "Space Invaders";
          private const string k_Player1AssetName = @"Sprites\Ship01_32x32";
          private const string k_Player2AssetName = @"Sprites\Ship02_32x32";
          private readonly Background r_Background;
          private readonly LevelTransitionScreen r_LevelTransition;
          private readonly PauseScreen r_PauseScreen;
          private EnemyManager m_EnemyManager;
          private BarrierManager m_BarrierManager;
          private IPlayersManager m_PlayersManager;
          private IInputManager m_InputManager;
          private IGameSettings r_GameSettings;
          private int m_Level = 1;

          public PlayScreen(Game i_Game)
               : base(i_Game)
          {
               r_LevelTransition = new LevelTransitionScreen(this.Game);
               r_PauseScreen     = new PauseScreen(this.Game);
               r_Background      = new Background(this);
               r_GameSettings    = this.Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;
               r_GameSettings.GraphicsDeviceManager.PreferredBackBufferWidth = (int)r_Background.Width;
               r_GameSettings.GraphicsDeviceManager.PreferredBackBufferHeight = (int)r_Background.Height;
               r_GameSettings.GraphicsDeviceManager.ApplyChanges();
          }

          public override void Initialize()
          {
               r_LevelTransition.StateChanged += levelTransition_StateChanged;
               r_PauseScreen.StateChanged     += pauseScreen_StateChanged;
               this.Game.Window.Title         = k_GameTitle;

               initServices();
               initPlayers();
               initDrawableManagers();

               base.Initialize();
          }

          private void pauseScreen_StateChanged(object i_Sender, StateChangedEventArgs i_Args)
          {
               if(i_Args.CurrentState == eScreenState.Closed)
               {
                    this.ScreensManager.SetCurrentScreen(this);
               }
          }

          private void initDrawableManagers()
          {
               m_EnemyManager   = new EnemyManager(this);
               m_BarrierManager = new BarrierManager(this, m_PlayersManager[0].StartPosition.Y, m_PlayersManager[0].Height);

               m_EnemyManager.MatrixReachedBottomWindow += OnGameOver;
               m_EnemyManager.AllEnemiesDied            += enemyManager_AllEnemiesDied;
               m_PlayersManager.AllPlayersDied          += OnGameOver;
          }

          private void enemyManager_AllEnemiesDied()
          {
               m_Level++;
               m_EnemyManager.UpdateLevelDifficulty();
               m_BarrierManager.UpdateLevelDifficulty();
               m_PlayersManager.Reset();
               m_EnemyManager.Reset();
               m_BarrierManager.Reset();
               showLevelTransition();
          }

          private void showLevelTransition()
          {
               if (r_LevelTransition.CurrentLevel < m_Level)
               {
                    r_LevelTransition.CurrentLevel++;
                    this.ScreensManager.SetCurrentScreen(r_LevelTransition);
               }
          }

          private void levelTransition_StateChanged(object i_Sender, StateChangedEventArgs i_Args)
          {
               if (i_Args.CurrentState == eScreenState.Closed)
               {
                    this.ScreensManager.SetCurrentScreen(this);
                    m_EnemyManager.Reset();
               }
          }

          private void initPlayers()
          {
               m_PlayersManager.AddPlayer(k_Player1AssetName);
               m_PlayersManager.AddPlayer(k_Player2AssetName);

               Player player2 = m_PlayersManager.GetLastAddedPlayer() as Player;
               player2.MoveLeftKey = Keys.A;
               player2.MoveRightKey = Keys.D;
               player2.ShootKey = Keys.W;
               player2.RepresentativeColor = Color.Green;
               player2.GroupRepresentative = m_PlayersManager[0];
          }

          private void initServices()
          {
               m_PlayersManager = new PlayersManager(this);
               m_PlayersManager.PlayerCollided += playersManager_PlayerCollided;
               m_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
          }

          private void playersManager_PlayerCollided(BasePlayer i_Player, ICollidable2D i_CollidedWith)
          {
               Enemy enemy = i_CollidedWith as Enemy;

               if (enemy != null)
               {
                    OnGameOver();
               }
          }

          private void OnGameOver()
          {
               ExitScreen();
          }

          public override void Update(GameTime i_GameTime)
          {
               if (m_InputManager.KeyPressed(Keys.Escape))
               {
                    this.Game.Exit();
               }

               if (m_InputManager.KeyPressed(Keys.P))
               {
                    this.ScreensManager.SetCurrentScreen(r_PauseScreen);
               }

               showLevelTransition();

               base.Update(i_GameTime);
          }
     }
}

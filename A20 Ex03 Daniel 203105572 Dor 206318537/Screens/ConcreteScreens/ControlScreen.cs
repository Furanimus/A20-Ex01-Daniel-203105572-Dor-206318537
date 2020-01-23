using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Menus.ConcreteMenus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{
     public abstract class ControlScreen : GameScreen
     {
          private bool isDeactivated;

          public ControlScreen(Game i_Game) 
               : base(i_Game)
          {
               MainMenuScreen = new MainMenuScreen(i_Game);
          }

          public MainMenuScreen MainMenuScreen { get; private set; }

          protected virtual void GetPlayerInput()
          {
               if (InputManager.KeyPressed(Keys.Enter) && !isDeactivated)
               {
                    OnEnterClicked();
               }

               if (InputManager.KeyPressed(Keys.Escape))
               {
                    OnEscClicked();
               }

               if (InputManager.KeyPressed(Keys.M) && !isDeactivated)
               {
                    OnKeysMClicked();
               }
          }

          protected virtual void OnEnterClicked()
          {
               ExitScreen();
          }

          protected virtual void OnEscClicked()
          {
               this.Game.Exit();
          }

          protected virtual void OnKeysMClicked()
          {
               OpenMainMenu();
          }

          protected virtual void OpenMainMenu()
          {
               isDeactivated = true;
               this.ScreensManager.SetCurrentScreen(MainMenuScreen);
               MainMenuScreen.StateChanged += MainMenu_StateChanged;
          }

          protected virtual void MainMenu_StateChanged(object i_Sender, StateChangedEventArgs i_Args)
          {
               if (i_Args.CurrentState == eScreenState.Closed)
               {
                    isDeactivated = false;
                    this.ExitScreen();
               }
          }

          public override void Update(GameTime gameTime)
          {
               base.Update(gameTime);

               GetPlayerInput();
               DoTransition();
          }

          protected virtual void DoTransition()
          {
          }
     }
}

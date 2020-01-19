using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {
          public SpaceInvadersGame()
          {
               Content.RootDirectory = "Content";
               ScreensMananger screensManager = new ScreensMananger(this);
               screensManager.Push(new PlayScreen(this));
               screensManager.SetCurrentScreen(new WelcomeScreen(this));
               this.IsMouseVisible = true;
          }
     }
}
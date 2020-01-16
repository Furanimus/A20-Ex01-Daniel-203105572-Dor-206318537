using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex01_Daniel_203105572_Dor_206318537.Screens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {

          public SpaceInvadersGame()
          {
               Content.RootDirectory = "Content";
               ScreensMananger screensMananger = new ScreensMananger(this);
               screensMananger.Push(new PlayScreen(this));
               screensMananger.SetCurrentScreen(new WelcomeScreen(this));
          }
     }
}
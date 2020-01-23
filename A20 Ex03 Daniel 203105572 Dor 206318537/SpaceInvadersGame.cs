﻿using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537
{
     public class SpaceInvadersGame : BaseGame
     {
          private const string k_BackgroundMusic = "BGMusic";

          public SpaceInvadersGame()
          {
               Content.RootDirectory = "Content";
               PlayScreen playScreen = new PlayScreen(this);
               ScreensManager screensManager = new ScreensManager(this);
               screensManager.Push(new GameOverScreen(playScreen, this));
               screensManager.Push(playScreen);
               screensManager.SetCurrentScreen(new WelcomeScreen(this));
               this.IsMouseVisible = true;

          }

          protected override void LoadContent()
          {
               SoundManager.PlayMusic(k_BackgroundMusic);

               base.LoadContent();
          }
     }
}
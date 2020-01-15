﻿using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
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
               screensMananger.SetCurrentScreen(new PlayScreen(this));
          }
     }
}
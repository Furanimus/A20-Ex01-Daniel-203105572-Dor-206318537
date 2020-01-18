using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus.ConcreteMenus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models.Menus.ConcreteMenus
{
     public class MainMenu : Menu
     {
          private const int k_NextPosX = 50;
          private const string k_OnePlayerPlayText = "Players: One";
          private const string k_TwoPlayerPlayText = "Players: Two";
          private const string k_PlayText = "Play";

          public MainMenu(GameScreen i_GameScreen) 
               : base(i_GameScreen)
          {
          }

          public override void Initialize()
          {
               this.Position = new Vector2(k_NextPosX, this.Game.GraphicsDevice.Viewport.Height / 2);

               base.Initialize();
          }

          protected override void AddItems()
          {
               SubMenu soundSettingsMenu = new SoundSettingsMenu(this, this.GameScreen);

               this.AddMenuItem(k_OnePlayerPlayText, playersCount_Clicked);
               this.AddMenuItem("Sound Settings", soundSettings_Clicked, soundSettingsMenu);
               this.AddMenuItem(k_PlayText, play_Clicked);
          }

          private void soundSettings_Clicked(MenuItem i_MenuItem)
          {
               if (r_InputManager.KeyPressed(Keys.Enter))
               {
                    this.Visible = false;

                    if(i_MenuItem.LinkedMenu != null)
                    {
                         i_MenuItem.LinkedMenu.Visible = true;
                    }
               }
          }

          private void playersCount_Clicked(MenuItem i_MenuItem)
          {
               if(r_InputManager.KeyPressed(Keys.PageDown) || r_InputManager.KeyPressed(Keys.PageUp))
               {
                    if(i_MenuItem.StrokeSpriteFont.Text == k_OnePlayerPlayText)
                    {
                         i_MenuItem.StrokeSpriteFont.Text = k_TwoPlayerPlayText;
                         GameSettings.PlayersCount = 2;
                    }
                    else
                    {
                         i_MenuItem.StrokeSpriteFont.Text = k_OnePlayerPlayText;
                         GameSettings.PlayersCount = 1;
                    }
               }
          }

          private void play_Clicked(MenuItem i_MenuItem)
          {
               if(r_InputManager.KeyPressed(Keys.Enter))
               {
                    this.GameScreen.ExitScreen();
               }
          }
     }
}

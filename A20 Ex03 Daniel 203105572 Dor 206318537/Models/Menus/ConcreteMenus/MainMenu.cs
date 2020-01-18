﻿using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
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

               MenuItem playersCountMenuItem = new MenuItem(k_OnePlayerPlayText, this.GameScreen);
               playersCountMenuItem.BindActionToKeys(playersCount_Clicked, Keys.PageDown, Keys.PageUp);
               playersCountMenuItem.BindActionToMouseWheel(playersCount_Clicked);

               MenuItem soundSettingsMenuItem = new MenuItem("Sound Settings", this.GameScreen, soundSettingsMenu);
               soundSettingsMenuItem.BindActionToKeys(soundSettings_Clicked, Keys.Enter);
               soundSettingsMenuItem.BindActionToMouseButtons(soundSettings_Clicked, eInputButtons.Left);

               MenuItem playMenuItem = new MenuItem(k_PlayText, this.GameScreen);
               playMenuItem.BindActionToKeys(play_Clicked, Keys.Enter);
               playMenuItem.BindActionToMouseButtons(play_Clicked, eInputButtons.Left);

               this.AddMenuItem(playersCountMenuItem);
               this.AddMenuItem(soundSettingsMenuItem);
               this.AddMenuItem(playMenuItem);
          }

          private void soundSettings_Clicked(MenuItem i_MenuItem)
          {
               this.Visible = false;

               if(i_MenuItem.LinkedMenu != null)
               {
                    i_MenuItem.LinkedMenu.Visible = true;
               }
          }

          private void playersCount_Clicked(MenuItem i_MenuItem)
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

          private void play_Clicked(MenuItem i_MenuItem)
          {
               if(r_InputManager.KeyPressed(Keys.Enter) 
                    || r_InputManager.ButtonPressed(eInputButtons.Left))
               {
                    this.GameScreen.ExitScreen();
               }
          }
     }
}

﻿using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus;
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
          private const string k_OnePlayerPlayText = "Players: {0}";
          private const string k_PlayText = "Play";
          private const string k_One = "One";
          private const string k_Two = "Two";
          private const string k_SoundSettingsMenuItem = "Sound Settings";
          private const string k_ScreenSettingsMenuItem = "Screen Settings";

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
               MenuItem playersCountMenuItem = new MenuItem(string.Format(k_OnePlayerPlayText, k_One), this.GameScreen);
               playersCountMenuItem.BindActionToKeys(playersCount_Clicked, Keys.PageDown, Keys.PageUp);
               playersCountMenuItem.BindActionToMouseButtons(playersCount_Clicked, eInputButtons.Right);
               playersCountMenuItem.BindActionToMouseWheel(playersCount_Clicked);

               SubMenu soundSettingsMenu = new SoundSettingsMenu(this, this.GameScreen);
               MenuItem soundSettingsMenuItem = new MenuItem(k_SoundSettingsMenuItem, this.GameScreen, soundSettingsMenu);
               soundSettingsMenuItem.BindActionToKeys(subMenu_Clicked, Keys.Enter);
               soundSettingsMenuItem.BindActionToMouseButtons(subMenu_Clicked, eInputButtons.Left);

               SubMenu screenSettings = new ScreenSettings(this, this.GameScreen);
               MenuItem screenSettingsMenuItem = new MenuItem(k_ScreenSettingsMenuItem, this.GameScreen, screenSettings);
               screenSettingsMenuItem.BindActionToKeys(subMenu_Clicked, Keys.Enter);
               screenSettingsMenuItem.BindActionToMouseButtons(subMenu_Clicked, eInputButtons.Left);

               MenuItem playMenuItem = new MenuItem(k_PlayText, this.GameScreen);
               playMenuItem.BindActionToKeys(play_Clicked, Keys.Enter);
               playMenuItem.BindActionToMouseButtons(play_Clicked, eInputButtons.Left);

               this.AddMenuItem(playersCountMenuItem);
               this.AddMenuItem(screenSettingsMenuItem);
               this.AddMenuItem(soundSettingsMenuItem);
               this.AddMenuItem(playMenuItem);
          }

          private void subMenu_Clicked(MenuItem i_MenuItem)
          {
               if (i_MenuItem.LinkedMenu != null)
               {
                    this.Visible = false;
                    i_MenuItem.LinkedMenu.Visible = true;
               }
          }

          private void playersCount_Clicked(MenuItem i_MenuItem)
          {
               bool isToggle = GameSettings.PlayersCount == 2;
               string playerCount = isToggle ? k_One : k_Two;
               int complement = 3;

               i_MenuItem.StrokeSpriteFont.Text = string.Format(k_OnePlayerPlayText, playerCount);
               GameSettings.PlayersCount = complement - GameSettings.PlayersCount;
          }

          private void play_Clicked(MenuItem i_MenuItem)
          {
               this.GameScreen.ExitScreen();
          }
     }
}

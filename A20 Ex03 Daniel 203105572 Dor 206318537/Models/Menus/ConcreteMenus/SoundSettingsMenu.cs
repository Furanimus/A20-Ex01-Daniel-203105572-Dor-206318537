﻿using A20_Ex03_Daniel_203105572_Dor_206318537.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using Microsoft.Xna.Framework.Input;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus.ConcreteMenus
{
     public class SoundSettingsMenu : SubMenu
     {
          private const string k_BackgroundMusicVolumeText = "Background Music Volume: {0}";
          private const string k_SoundEffectVolumeText = "Sound Effect Volume: {0}";
          private const string k_ToggleSoundText = "Toggle Sound: {0}";
          private const int k_ChangeVolumeBy = 10;
          private const int k_MaxVolume = 100;

          public SoundSettingsMenu(Menu i_PrevMenu, GameScreen i_GameScreen) 
               : base(i_PrevMenu, i_GameScreen)
          {
          }

          protected override void AddItems()
          {
               MenuItem backgroundVolumeMeniItem 
                    = new MenuItem(string.Format(k_BackgroundMusicVolumeText, GameSettings.BackgroundMusicVolume), this.GameScreen);
               MenuItem soundEffectVolumeMenuItem 
                    = new MenuItem(string.Format(k_SoundEffectVolumeText, GameSettings.SoundEffectsVolume), this.GameScreen);
               MenuItem toggleSoundMenuItem 
                    = new MenuItem(string.Format(k_ToggleSoundText, GameSettings.IsSound ? "On" : "Off"), this.GameScreen);

               backgroundVolumeMeniItem.BindActionToKeys(backgroundMusicVolume_Clicked, Keys.PageDown, Keys.PageUp);
               backgroundVolumeMeniItem.BindActionToMouseWheel(backgroundMusicVolume_Clicked);

               soundEffectVolumeMenuItem.BindActionToKeys(soundEffectVolume_Clicked, Keys.PageDown, Keys.PageUp);
               soundEffectVolumeMenuItem.BindActionToMouseWheel(soundEffectVolume_Clicked);

               toggleSoundMenuItem.BindActionToKeys(toggleSound_Clicked, Keys.PageDown, Keys.PageUp);
               toggleSoundMenuItem.BindActionToMouseWheel(toggleSound_Clicked);

               this.AddMenuItem(backgroundVolumeMeniItem);
               this.AddMenuItem(soundEffectVolumeMenuItem);
               this.AddMenuItem(toggleSoundMenuItem);

               base.AddItems();
          }

          private void toggleSound_Clicked(MenuItem i_ActionItem)
          {
               GameSettings.IsSound = !GameSettings.IsSound;
               i_ActionItem.StrokeSpriteFont.Text = string.Format(k_ToggleSoundText, GameSettings.IsSound ? "On" : "Off");
          }

          private void soundEffectVolume_Clicked(MenuItem i_ActionItem)
          {
               updateSoundEffectVolumeInSettings();
               i_ActionItem.StrokeSpriteFont.Text = string.Format(k_SoundEffectVolumeText, GameSettings.SoundEffectsVolume);
          }

          private void backgroundMusicVolume_Clicked(MenuItem i_ActionItem)
          {
               updateBackgroundMusicVolumeInSettings();
               i_ActionItem.StrokeSpriteFont.Text = string.Format(k_BackgroundMusicVolumeText, GameSettings.BackgroundMusicVolume);
          }

          private void updateSoundEffectVolumeInSettings()
          {
               if (r_InputManager.KeyPressed(Keys.PageDown) 
                    || r_InputManager.ScrollWheelDelta < 0)
               {
                    if (GameSettings.SoundEffectsVolume >= k_ChangeVolumeBy)
                    {
                         GameSettings.SoundEffectsVolume -= k_ChangeVolumeBy;
                    }
               }
               else if (r_InputManager.KeyPressed(Keys.PageUp) 
                    || r_InputManager.ScrollWheelDelta > 0)
               {
                    if (GameSettings.SoundEffectsVolume <= k_MaxVolume - k_ChangeVolumeBy)
                    {
                         GameSettings.SoundEffectsVolume += k_ChangeVolumeBy;
                    }
               }
          }

          private void updateBackgroundMusicVolumeInSettings()
          {
               if (r_InputManager.KeyPressed(Keys.PageDown)
                    || r_InputManager.ScrollWheelDelta < 0)
               {
                    if (GameSettings.BackgroundMusicVolume >= k_ChangeVolumeBy)
                    {
                         GameSettings.BackgroundMusicVolume -= k_ChangeVolumeBy;
                    }
               }
               else if (r_InputManager.KeyPressed(Keys.PageUp)
                    || r_InputManager.ScrollWheelDelta > 0)
               {
                    if (GameSettings.BackgroundMusicVolume <= k_MaxVolume - k_ChangeVolumeBy)
                    {
                         GameSettings.BackgroundMusicVolume += k_ChangeVolumeBy;
                    }
               }
          }
     }
}
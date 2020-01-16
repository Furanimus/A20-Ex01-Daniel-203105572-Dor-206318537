﻿using A20_Ex03_Daniel_203105572_Dor_206318537.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
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
               this.AddMenuItem(string.Format(k_BackgroundMusicVolumeText, GameSettings.BackgroundMusicVolume), backgroundMusicVolume_Clicked);
               this.AddMenuItem(string.Format(k_SoundEffectVolumeText, GameSettings.SoundEffectsVolume), soundEffectVolume_Clicked);
               this.AddMenuItem(string.Format(k_ToggleSoundText, GameSettings.IsSound ? "On" : "Off"), toggleSound_Clicked);

               base.AddItems();
          }

          private void toggleSound_Clicked(MenuItem i_ActionItem, Keys i_ClickedKey)
          {
               updateToggleSoundInSettings(i_ClickedKey);
               i_ActionItem.StrokeSpriteFont.Text = string.Format(k_ToggleSoundText, GameSettings.IsSound ? "On" : "Off");
          }

          private void soundEffectVolume_Clicked(MenuItem i_ActionItem, Keys i_ClickedKey)
          {
               updateSoundEffectVolumeInSettings(i_ClickedKey);
               i_ActionItem.StrokeSpriteFont.Text = string.Format(k_SoundEffectVolumeText, GameSettings.SoundEffectsVolume);
          }

          private void backgroundMusicVolume_Clicked(MenuItem i_ActionItem, Keys i_ClickedKey)
          {
               updateBackgroundMusicVolumeInSettings(i_ClickedKey);
               i_ActionItem.StrokeSpriteFont.Text = string.Format(k_BackgroundMusicVolumeText, GameSettings.BackgroundMusicVolume);
          }

          private void updateToggleSoundInSettings(Keys i_ClickedKey)
          {
               if (i_ClickedKey == Keys.PageDown || i_ClickedKey == Keys.PageUp)
               {
                    GameSettings.IsSound = !GameSettings.IsSound;
               }
          }

          private void updateSoundEffectVolumeInSettings(Keys i_ClickedKey)
          {
               if (i_ClickedKey == Keys.PageDown)
               {
                    if (GameSettings.SoundEffectsVolume >= k_ChangeVolumeBy)
                    {
                         GameSettings.SoundEffectsVolume -= k_ChangeVolumeBy;
                    }
               }
               else if (i_ClickedKey == Keys.PageUp)
               {
                    if (GameSettings.SoundEffectsVolume <= k_MaxVolume - k_ChangeVolumeBy)
                    {
                         GameSettings.SoundEffectsVolume += k_ChangeVolumeBy;
                    }
               }
          }

          private void updateBackgroundMusicVolumeInSettings(Keys i_ClickedKey)
          {
               if (i_ClickedKey == Keys.PageDown)
               {
                    if (GameSettings.BackgroundMusicVolume >= k_ChangeVolumeBy)
                    {
                         GameSettings.BackgroundMusicVolume -= k_ChangeVolumeBy;
                    }
               }
               else if (i_ClickedKey == Keys.PageUp)
               {
                    if (GameSettings.BackgroundMusicVolume <= k_MaxVolume - k_ChangeVolumeBy)
                    {
                         GameSettings.BackgroundMusicVolume += k_ChangeVolumeBy;
                    }
               }
          }
     }
}

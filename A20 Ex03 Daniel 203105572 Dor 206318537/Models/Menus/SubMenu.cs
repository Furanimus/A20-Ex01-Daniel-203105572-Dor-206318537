﻿using Microsoft.Xna.Framework.Input;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;

namespace A20_ex03_Daniel_203105572_Dor_206318537.Models.Menus
{
     public class SubMenu : Menu
     {
          private readonly Menu r_PrevMenu;

          public SubMenu(StrokeSpriteFont i_Title, Menu i_PrevMenu, GameScreen i_GameScreen) 
               : base(i_Title, i_GameScreen)
          {
               r_PrevMenu = i_PrevMenu;
               this.Position = i_PrevMenu.Position;
          }

          public SubMenu(string i_Title, Menu i_PrevMenu, GameScreen i_GameScreen)
               : this(new StrokeSpriteFont(i_Title, i_GameScreen), i_PrevMenu, i_GameScreen)
          {
          }

          private void done_CheckMouseOrKBState(MenuItem i_MenuItem)
          {
               if(r_InputManager.KeyPressed(Keys.Enter) 
                    || r_InputManager.ButtonPressed(eInputButtons.Left))
               {
                    r_PrevMenu.Visible = true;
                    this.Visible = false;
               }
          }

          protected override void AddItems()
          {
               this.AddMenuItem("Done", done_CheckMouseOrKBState);
               this.Visible = false;
          }
     }
}

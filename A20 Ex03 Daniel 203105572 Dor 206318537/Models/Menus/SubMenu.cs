using A20_Ex03_Daniel_203105572_Dor_206318537.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus
{
     public class SubMenu : Menu
     {
          private readonly Menu r_PrevMenu;

          public SubMenu(Menu i_PrevMenu, GameScreen i_GameScreen) 
               : base(i_GameScreen)
          {
               r_PrevMenu = i_PrevMenu;
               this.StartingPosition = i_PrevMenu.StartingPosition;
          }

          private void done_Clicked(MenuItem i_MenuItem, Keys i_ClickedKey)
          {
               if(i_ClickedKey == Keys.Enter)
               {
                    r_PrevMenu.Visible = true;
                    this.Visible = false;
               }
          }

          protected override void AddItems()
          {
               this.AddMenuItem("Done", done_Clicked);
               this.Visible = false;
          }
     }
}

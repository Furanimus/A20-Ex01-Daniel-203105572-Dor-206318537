using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Menus.ConcreteMenus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{

     public class MainMenuScreen : GameScreen
     {
          private MainMenu m_MainMenu;

          public MainMenuScreen(Game i_Game) 
               : base(i_Game)
          {
               m_MainMenu = new MainMenu(this);
               this.IsOverlayed = true;
               this.UseGradientBackground = true;
               this.BlackTintAlpha = 0.65f;
               this.UseFadeTransition = true;
               this.ActivationLength = TimeSpan.FromSeconds(0.5f);
               this.DeactivationLength = TimeSpan.FromSeconds(0.5f);
               this.BlendState = BlendState.NonPremultiplied;
          }

          public override void Update(GameTime i_GameTime)
          {
               base.Update(i_GameTime);

               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    m_MainMenu.Opacity = this.TransitionPosition;
               }
          }
     }
}

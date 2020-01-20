using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{
     public class PauseScreen : GameScreen
     {
          private readonly IInputManager r_InputManager;
          private readonly StrokeSpriteFont r_ResumeText;

          public PauseScreen(Game i_Game) 
               : base(i_Game)
          {
               r_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
               r_ResumeText = new StrokeSpriteFont("Press R to resume", this);
               this.IsOverlayed = true;
               this.BlackTintAlpha = 0.55f;
               this.UseFadeTransition = true;
               this.ActivationLength = TimeSpan.FromSeconds(0.5f);
               this.DeactivationLength = TimeSpan.FromSeconds(0.5f);
               this.BlendState = BlendState.NonPremultiplied;

               this.Add(r_ResumeText);
          }

          public override void Initialize()
          {
               base.Initialize();

               r_ResumeText.PositionOrigin = r_ResumeText.SourceRectangleCenter;
               r_ResumeText.RotationOrigin = r_ResumeText.SourceRectangleCenter;
               r_ResumeText.Position = CenterOfViewPort;
          }

          public override void Update(GameTime i_GameTime)
          {
               if (r_InputManager.KeyPressed(Keys.R))
               {
                    ExitScreen();
               }

               doTransition();

               base.Update(i_GameTime);
          }

          private void doTransition()
          {
               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    r_ResumeText.Opacity = this.TransitionPosition;
               }
          }
     }
}

using A20_Ex01_Daniel_203105572_Dor_206318537.Screens.Animators.ConcreteAnimator;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Screens
{
     public class WelcomeScreen : GameScreen
     {
          private const string k_WelcomeAssetName = @"Sprites\WelcomeMessage";
          private const string k_InstructionsAssetName = @"Sprites\Instructions";
          private const int k_Space = 50;
          private Sprite m_WelcomeMessage;
          private Background m_Background;
          private Sprite m_Instructions;

          public WelcomeScreen(Game i_Game)
              : base(i_Game)
          {
               m_WelcomeMessage = new Sprite(k_WelcomeAssetName, this);
               m_Instructions = new Sprite(k_InstructionsAssetName, this);
               m_Background = new Background(this);
               m_Background.Opacity = 1;

               this.Add(m_WelcomeMessage);
               this.Add(m_Instructions);
               this.DeactivationLength = TimeSpan.FromSeconds(1);
               this.UseFadeTransition = false;
               this.BlendState = BlendState.NonPremultiplied;
          }

          public override void Initialize()
          {
               base.Initialize();

               m_WelcomeMessage.Animations.Add(new PulseAnimator(TimeSpan.Zero, 1.05f, 0.7f));
               m_WelcomeMessage.Animations.Enabled = true;
               m_WelcomeMessage.PositionOrigin = m_WelcomeMessage.SourceRectangleCenter;
               m_WelcomeMessage.RotationOrigin = m_WelcomeMessage.SourceRectangleCenter;
               m_WelcomeMessage.Position = CenterOfViewPort;

               m_Instructions.PositionOrigin = m_WelcomeMessage.SourceRectangleCenter;
               m_Instructions.RotationOrigin = m_WelcomeMessage.SourceRectangleCenter;
               m_Instructions.Position = CenterOfViewPort + new Vector2(0, m_WelcomeMessage.Height + k_Space);
          }

          public override void Update(GameTime gameTime)
          {
               base.Update(gameTime);

               if (InputManager.KeyPressed(Keys.Enter))
               {
                    ExitScreen();
               }

               if(InputManager.KeyPressed(Keys.Escape))
               {
                    this.Game.Exit();
               }

               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    m_Background.Opacity = this.TransitionPosition;
                    m_WelcomeMessage.Opacity = this.TransitionPosition;
                    m_Instructions.Opacity = this.TransitionPosition;
               }
          }
     }
}

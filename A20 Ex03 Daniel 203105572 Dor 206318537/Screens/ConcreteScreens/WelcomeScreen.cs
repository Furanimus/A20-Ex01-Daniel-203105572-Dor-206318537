using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.Animators.ConcreteAnimator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.ConcreteScreens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class WelcomeScreen : GameScreen
     {
          private const string k_WelcomeAssetName = @"Sprites\WelcomeMessage";
          private const string k_InstructionsAssetName = @"Sprites\Instructions";
          private const float k_WelcomePulsePerSec = 0.7f;
          private const float k_WelcomeTargetScale = 1.05f;
          private const int k_Space = 50;
          private Sprite m_WelcomeMessage;
          private Background m_Background;
          private Sprite m_Instructions;
          private MainMenuScreen m_MainMenu;
          private bool isDeactivated;

          public WelcomeScreen(Game i_Game)
              : base(i_Game)
          {
               m_MainMenu = new MainMenuScreen(this.Game);
               m_WelcomeMessage = new Sprite(k_WelcomeAssetName, this);
               m_Instructions = new Sprite(k_InstructionsAssetName, this);
               m_Background = new Background(this);

               this.UseFadeTransition = true;
               this.ActivationLength = TimeSpan.FromSeconds(0.5f);
               this.DeactivationLength = TimeSpan.FromSeconds(0.5f);

               this.Add(m_WelcomeMessage);
               this.Add(m_Instructions);

               this.BlendState = BlendState.NonPremultiplied;
               m_WelcomeMessage.BlendState = BlendState.NonPremultiplied;
               m_Instructions.BlendState = BlendState.NonPremultiplied;
          }

          public override void Initialize()
          {
               base.Initialize();

               m_WelcomeMessage.Animations.Add(new PulseAnimator(TimeSpan.Zero, k_WelcomeTargetScale, k_WelcomePulsePerSec));
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

               if (InputManager.KeyPressed(Keys.Enter) && !isDeactivated)
               {
                    ExitScreen();
               }

               if(InputManager.KeyPressed(Keys.Escape))
               {
                    this.Game.Exit();
               }

               if(InputManager.KeyPressed(Keys.M) && !isDeactivated)
               {
                    openMainMenu();
               }

               doTransition();
          }

          private void openMainMenu()
          {
               isDeactivated = true;
               this.ScreensManager.SetCurrentScreen(m_MainMenu);
               m_MainMenu.StateChanged += mainMenu_StateChanged;
          }

          private void doTransition()
          {
               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    m_Background.Opacity = this.TransitionPosition;
                    m_WelcomeMessage.Opacity = this.TransitionPosition;
                    m_Instructions.Opacity = this.TransitionPosition;
               }
          }

          private void mainMenu_StateChanged(object sender, StateChangedEventArgs i_Args)
          {
               if (i_Args.CurrentState == eScreenState.Closed)
               {
                    isDeactivated = false;
                    this.ScreensManager.SetCurrentScreen(this);
               }
          }
     }
}

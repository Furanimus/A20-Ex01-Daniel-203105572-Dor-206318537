using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{
     public class GameOverScreen : GameScreen
     {
          private const string k_GameOverAssetName = @"Sprites\GameOverMessage";
          private const float k_PulseTargetScale = 1.05f;
          private const float k_PulsePerSec = 0.7f;
          private const int k_ActivationLength = 1;
          private Sprite m_GameOverMessage;
          private Background m_Background;

          public GameOverScreen(Game i_Game)
          : base(i_Game)
          {
               m_Background = new Background(this);
               m_Background.TintColor = Color.Red;
               m_GameOverMessage = new Sprite(k_GameOverAssetName, this);
               this.Add(m_GameOverMessage);

               this.ActivationLength = TimeSpan.FromSeconds(k_ActivationLength);
          }

          public override void Initialize()
          {
               base.Initialize();

               m_GameOverMessage.Animations.Add(new PulseAnimator(TimeSpan.Zero, k_PulseTargetScale, k_PulsePerSec));
               m_GameOverMessage.Animations.Enabled = true;
               m_GameOverMessage.PositionOrigin = m_GameOverMessage.SourceRectangleCenter;
               m_GameOverMessage.RotationOrigin = m_GameOverMessage.SourceRectangleCenter;
               m_GameOverMessage.Position = CenterOfViewPort;
          }

          public override void Update(GameTime gameTime)
          {
               base.Update(gameTime);

               if (InputManager.KeyPressed(Keys.Escape))
               {
                    ExitScreen();
               }

               doTransition();
          }

          private void doTransition()
          {
               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    m_GameOverMessage.Scales = new Vector2(this.TransitionPosition);
               }
          }
     }
}

using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimators;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{
     public class LevelTransitionScreen : GameScreen
     {
          private const string k_FontName          = @"Fonts/HeadlineArialFont";
          private const float k_ActivationLength   = 0.5f;
          private const float k_DeactivationLength = 0.5f;
          private const float k_BlackTintAlpha     = 0.65f;
          private const float k_YOffset            = 60;
          private const string k_Level             = "Level {0}";
          private const string k_SecondsLeft       = "{0}";
          private const float k_TimeUntilExit      = 3f;
          private float m_CurrentTime              = 0;
          private float m_OneSecondTimer           = 1f;
          private int m_ShownLevel                 = 0;
          private bool m_IsInitialized;
          private StrokeSpriteFont m_LevelText;
          private StrokeSpriteFont m_SecondsLeftText;

          public LevelTransitionScreen(Game i_Game)
               : base(i_Game)
          {
               this.IsOverlayed = true;
               this.UseGradientBackground = true;
               this.BlackTintAlpha = k_BlackTintAlpha;
               this.UseFadeTransition = true;
               this.ActivationLength = TimeSpan.FromSeconds(k_ActivationLength);
               this.DeactivationLength = TimeSpan.FromSeconds(k_DeactivationLength);
               this.BlendState = BlendState.NonPremultiplied;
          }

          public int CurrentLevel { get; set; }

          private float SecondsLeft { get; set; } = 3;

          public override void Initialize()
          {
               if (!m_IsInitialized)
               {
                    base.Initialize();

                    initTexts(out m_SecondsLeftText, k_SecondsLeft, CenterOfViewPort + new Vector2(0, k_YOffset));
                    initTexts(out m_LevelText, k_Level, CenterOfViewPort);
                    this.Add(m_LevelText);
                    this.Add(m_SecondsLeftText);

                    m_IsInitialized = true;
               }
          }

          private void initTexts(out StrokeSpriteFont o_SpriteText, string i_Text, Vector2 i_Position)
          {
               o_SpriteText                = new StrokeSpriteFont(k_FontName, string.Format(i_Text, SecondsLeft), this);
               o_SpriteText.Initialize();
               o_SpriteText.PositionOrigin = o_SpriteText.SourceRectangleCenter;
               o_SpriteText.RotationOrigin = o_SpriteText.SourceRectangleCenter;
               o_SpriteText.Position       = i_Position;
          }

          public override void Update(GameTime i_GameTime)
          {
               if (m_ShownLevel != CurrentLevel)
               {
                    m_ShownLevel = CurrentLevel;
                    m_LevelText.Text = string.Format(k_Level, CurrentLevel);
               }

               m_CurrentTime += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
               m_OneSecondTimer -= (float)i_GameTime.ElapsedGameTime.TotalSeconds;

               if (m_OneSecondTimer <= 0)
               {
                    m_OneSecondTimer = 1f;
                    SecondsLeft--;
                    m_SecondsLeftText.Text = string.Format(k_SecondsLeft, SecondsLeft);
               }

               if (m_CurrentTime >= k_TimeUntilExit)
               {
                    Reset();
                    ExitScreen();
               }

               doTransition();
               base.Update(i_GameTime);
          }

          private void Reset()
          {
               m_CurrentTime = 0;
               m_OneSecondTimer = 1f;
               SecondsLeft = 3;
          }

          private void doTransition()
          {
               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    m_LevelText.Scales = new Vector2(this.TransitionPosition);
                    m_SecondsLeftText.Scales = new Vector2(this.TransitionPosition);
               }
          }
     }
}

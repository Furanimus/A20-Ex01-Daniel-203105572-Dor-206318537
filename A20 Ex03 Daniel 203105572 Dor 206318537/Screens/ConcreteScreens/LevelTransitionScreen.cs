﻿using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimators;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens
{
     public class LevelTransitionScreen : GameScreen
     {
          private const float k_PulseTargetScale   = 2f;
          private const float k_PulsePerSec        = 0.5f;
          private const float k_ActivationLength   = 0.5f;
          private const float k_DeactivationLength = 0.5f;
          private const float k_BlackTintAlpha     = 0.65f;
          private const float k_YOffset            = 40;
          private const string k_Level             = "Level {0}";
          private const string k_SecondsLeft       = "{0}";
          private const float k_TimeUntilExit      = 3f;
          private float m_CurrentTime              = 0;
          private float m_OneSecondTimer           = 1f;
          private int m_ShownLevel                 = 0;
          private bool m_IsInitialized;
          private StrokeSpriteFont m_LevelText;
          private StrokeSpriteFont m_SecondsLeft;

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

               m_LevelText = new StrokeSpriteFont("", this);
          }

          public int CurrentLevel { get; set; }

          private float SecondsLeft { get; set; } = 3;

          public override void Initialize()
          {
               if (!m_IsInitialized)
               {
                    m_IsInitialized = true;
                    base.Initialize();

                    m_ShownLevel = CurrentLevel;
                    m_LevelText = new StrokeSpriteFont(string.Format(k_Level, CurrentLevel), this);
                    m_SecondsLeft = new StrokeSpriteFont(string.Format(k_SecondsLeft, SecondsLeft), this);
                    m_LevelText.Initialize();
                    m_SecondsLeft.Initialize();
                    m_LevelText.Animations.Add(new PulseAnimator(TimeSpan.Zero, k_PulseTargetScale, k_PulsePerSec));
                    m_SecondsLeft.Animations.Add(new PulseAnimator(TimeSpan.Zero, k_PulseTargetScale, k_PulsePerSec));
                    m_LevelText.Animations.Enabled = true;
                    m_SecondsLeft.Animations.Enabled = true;

                    m_LevelText.PositionOrigin = m_LevelText.SourceRectangleCenter;
                    m_LevelText.RotationOrigin = m_LevelText.SourceRectangleCenter;
                    m_SecondsLeft.PositionOrigin = m_SecondsLeft.SourceRectangleCenter;
                    m_SecondsLeft.RotationOrigin = m_SecondsLeft.SourceRectangleCenter;
                    m_LevelText.Position = CenterOfViewPort;
                    m_SecondsLeft.Position = CenterOfViewPort + new Vector2 (0, k_YOffset);

                    this.Add(m_LevelText);
                    this.Add(m_SecondsLeft);
               }
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
                    m_SecondsLeft.Text = string.Format(k_SecondsLeft, SecondsLeft);
               }

               if (m_CurrentTime >= k_TimeUntilExit)
               {
                    m_CurrentTime = 0;
                    m_OneSecondTimer = 1f;
                    SecondsLeft = 3;
                    m_LevelText.Animations.Reset();
                    m_LevelText.Scales = Vector2.One;
                    m_SecondsLeft.Animations.Reset();
                    m_SecondsLeft.Scales = Vector2.One;
                    ExitScreen();
               }

               doTransition();
               base.Update(i_GameTime);
          }

          private void doTransition()
          {
               if (this.TransitionPosition != 1 && this.TransitionPosition != 0)
               {
                    m_LevelText.Scales = new Vector2(this.TransitionPosition);
                    m_SecondsLeft.Scales = new Vector2(this.TransitionPosition);
               }
          }
     }
}

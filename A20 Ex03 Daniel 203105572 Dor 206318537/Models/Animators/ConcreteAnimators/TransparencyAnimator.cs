﻿using System;
using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimators
{
     public class TransparencyAnimator : SpriteAnimator
     {
          private const int k_MaxOpacity = 1;
          private const int k_MinOpacity = 0;
          private const string k_Name = "Transparency";
          private float m_TimePassed;

          public TransparencyAnimator(TimeSpan i_AnimationLength)
               : this(k_Name, i_AnimationLength)
          {
          }

          public TransparencyAnimator(string i_Name, TimeSpan i_AnimationLength)
               : base(i_Name, i_AnimationLength)
          {
          }

          protected override void DoFrame(GameTime i_GameTime)
          {
               m_TimePassed += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
               float opacity = k_MaxOpacity - (m_TimePassed / (float)AnimationLength.TotalSeconds);
               this.BoundSprite.Opacity = MathHelper.Clamp(opacity, k_MinOpacity, k_MaxOpacity);
          }

          protected override void RevertToOriginal()
          {
               m_TimePassed = 0;
               this.BoundSprite.TintColor = m_OriginalSpriteInfo.TintColor;
          }
     }
}

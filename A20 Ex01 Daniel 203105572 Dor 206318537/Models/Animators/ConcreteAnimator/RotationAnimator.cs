﻿using Microsoft.Xna.Framework;
using Models.Animators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator
{
     public class RotationAnimator : SpriteAnimator
     {
          private readonly int r_CircledPerSecond;

          public RotationAnimator(int i_CircledPerSecond, TimeSpan i_AnimationLength) 
               : this("Rotation", i_CircledPerSecond, i_AnimationLength)
          {
          }

          public RotationAnimator(string i_Name, int i_CircledPerSecond, TimeSpan i_AnimationLength) 
               : base(i_Name, i_AnimationLength)
          {
               this.ResetAfterFinish = false;
               r_CircledPerSecond = i_CircledPerSecond;
          }

          protected override void DoFrame(GameTime i_GameTime)
          {
               this.BoundSprite.Rotation += r_CircledPerSecond * MathHelper.TwoPi * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
          }

          protected override void RevertToOriginal()
          {
               this.BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
          }
     }
}

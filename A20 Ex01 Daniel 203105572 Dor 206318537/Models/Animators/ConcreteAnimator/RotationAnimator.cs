using Microsoft.Xna.Framework;
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
          private readonly int r_Circles;
          private readonly TimeSpan m_CirclesDuration;

          public RotationAnimator(int i_Circles, TimeSpan i_CirclesDurations, TimeSpan i_AnimationLength) : this("RotationAnimator", i_Circles, i_CirclesDurations, i_AnimationLength)
          {
          }

          public RotationAnimator(string i_Name, int i_Circles, TimeSpan i_CirclesDurations, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
          {
               r_Circles = i_Circles;
               m_CirclesDuration = i_CirclesDurations;
          }

          protected override void DoFrame(GameTime i_GameTime)
          {
               this.BoundSprite.Rotation += r_Circles * 360 * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
          }

          protected override void RevertToOriginal()
          {
               this.BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
          }
     }
}

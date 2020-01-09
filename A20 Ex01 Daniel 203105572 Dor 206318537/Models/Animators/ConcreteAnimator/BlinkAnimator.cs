using System;
using Microsoft.Xna.Framework;

namespace Models.Animators.ConcreteAnimators
{
     public class BlinkAnimator : SpriteAnimator
     {
          private TimeSpan m_TimeLeftForNextBlink;

          public BlinkAnimator(string i_Name, TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
               : base(i_Name, i_AnimationLength)
          {
               this.BlinkLength = i_BlinkLength;
               this.m_TimeLeftForNextBlink = i_BlinkLength;
          }

          public BlinkAnimator(TimeSpan i_BlinkLength, TimeSpan i_AnimationLength)
               : this("Blink", i_BlinkLength, i_AnimationLength)
          {
               this.BlinkLength = i_BlinkLength;
               this.m_TimeLeftForNextBlink = i_BlinkLength;
          }
          public TimeSpan BlinkLength { get; set; }

          protected override void DoFrame(GameTime i_GameTime)
          {
               m_TimeLeftForNextBlink -= i_GameTime.ElapsedGameTime;
          
               if (m_TimeLeftForNextBlink.TotalSeconds < 0)
               {
                    this.BoundSprite.Visible = !this.BoundSprite.Visible;
                    m_TimeLeftForNextBlink = BlinkLength;
               }
          }

          protected override void RevertToOriginal()
          {
               this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
          }
     }
}

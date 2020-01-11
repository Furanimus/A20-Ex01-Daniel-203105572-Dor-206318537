using System;
using Microsoft.Xna.Framework;
using Models.Animators;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator
{
     public class JumpMovementAnimator : SpriteAnimator
     {
          private const string k_Name = "JumpMovement";
          private TimeSpan m_JumpIntervals;
          private TimeSpan m_TimeUntilNextJump;

          public JumpMovementAnimator(string i_Name, TimeSpan i_AnimationLength) 
               : base(i_Name, i_AnimationLength)
          {
          }

          public JumpMovementAnimator(TimeSpan i_JumpsIntervals, TimeSpan i_AnimationLength)
               : this(k_Name, i_AnimationLength)
          {
               m_TimeUntilNextJump = m_JumpIntervals = i_JumpsIntervals;
          }

          protected override void RevertToOriginal()
          {
               this.BoundSprite.Position = m_OriginalSpriteInfo.Position;
          }

          protected override void DoFrame(GameTime i_GameTime)
          {
               if (m_TimeUntilNextJump != TimeSpan.Zero)
               {
                    m_TimeUntilNextJump -= i_GameTime.ElapsedGameTime;

                    if (m_TimeUntilNextJump.TotalSeconds <= 0)
                    {
                         m_TimeUntilNextJump = m_JumpIntervals;
                         jump();
                    }
               }
          }

          private void jump()
          {
               this.BoundSprite.Position += (this.BoundSprite.Velocity * this.BoundSprite.MoveDirection) / 2;
          }
     }
}

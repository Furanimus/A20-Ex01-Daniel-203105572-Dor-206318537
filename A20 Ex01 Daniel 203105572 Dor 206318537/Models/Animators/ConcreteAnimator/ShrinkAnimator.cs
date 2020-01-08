using Microsoft.Xna.Framework;
using Models.Animators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator
{
     class ShrinkAnimator : SpriteAnimator
     {
          private const int k_InitialScale = 1;

          public ShrinkAnimator(TimeSpan i_AnimationLength)
               : this("Shrink" , i_AnimationLength)
          {
          }

          public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength)
              : base(i_Name, i_AnimationLength)
          {
          }
          
          protected override void DoFrame(GameTime i_GameTime)
          {
               float totalSeconds = (float)i_GameTime.ElapsedGameTime.TotalSeconds;
               float removeFromScale = (k_InitialScale / (float)this.AnimationLength.TotalSeconds) * totalSeconds;
               float newScaleX = MathHelper.Clamp(this.BoundSprite.Scales.X - removeFromScale, 0, 1);
               float newScaleY = MathHelper.Clamp(this.BoundSprite.Scales.Y - removeFromScale, 0, 1);

               this.BoundSprite.Scales = new Vector2(newScaleX, newScaleY);
          }

          protected override void RevertToOriginal()
          {
               this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
          }

          protected override void OnFinished()
          {
               base.OnFinished();
               IsFinished = false;
          }
     }
}

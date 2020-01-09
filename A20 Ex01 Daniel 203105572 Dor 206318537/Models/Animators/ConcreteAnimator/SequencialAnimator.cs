using System;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;

namespace Models.Animators.ConcreteAnimators
{
     public class SequencialAnimator : CompositeAnimator
     {
          public SequencialAnimator(string i_Name, TimeSpan i_AnimationLength, Sprite i_BoundSprite, params SpriteAnimator[] i_Animations)
               : base(i_Name, i_AnimationLength, i_BoundSprite, i_Animations)
          {
          }

          protected override void DoFrame(GameTime i_GameTime)
          {
               bool allFinished = true;

               foreach (SpriteAnimator animation in m_AnimationsList)
               {
                    if (!animation.IsFinished)
                    {
                         animation.Update(i_GameTime);
                         allFinished = false;
                         break;
                    }
               }

               if (allFinished)
               {
                    this.IsFinished = true;
               }
          }
     }
}

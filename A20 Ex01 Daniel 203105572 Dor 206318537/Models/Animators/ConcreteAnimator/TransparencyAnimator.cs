using Microsoft.Xna.Framework;
using Models.Animators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator
{
     public class TransparencyAnimator : SpriteAnimator
     {
          private float m_Alpha;
          private readonly Color r_TintColor;
          private const float k_AlphaMaxValue = 255;

          public TransparencyAnimator(Color i_BoundSpriteTintColor, TimeSpan i_AnimationLength) 
               : this("Transparency", i_BoundSpriteTintColor, i_AnimationLength)
          {
          }

          public TransparencyAnimator(string i_Name, Color i_BoundSpriteTintColor, TimeSpan i_AnimationLength) 
               : base(i_Name, i_AnimationLength)
          {
               r_TintColor = i_BoundSpriteTintColor;
               m_Alpha = (float)r_TintColor.A;
          }

          protected override void DoFrame(GameTime i_GameTime)
          {
               float totalSeconds = (float)i_GameTime.ElapsedGameTime.TotalSeconds;
               m_Alpha -= k_AlphaMaxValue * (totalSeconds / (float)AnimationLength.TotalSeconds);
               m_Alpha = MathHelper.Clamp(m_Alpha, 0, k_AlphaMaxValue);
               this.BoundSprite.TintColor = new Color(r_TintColor, (byte)m_Alpha);
          }

          protected override void RevertToOriginal()
          {
               this.BoundSprite.TintColor = m_OriginalSpriteInfo.TintColor;
               m_Alpha = (float)m_OriginalSpriteInfo.TintColor.A;
          }

          protected override void OnFinished()
          {
               base.OnFinished();
               IsFinished = false;
          }
     }
}

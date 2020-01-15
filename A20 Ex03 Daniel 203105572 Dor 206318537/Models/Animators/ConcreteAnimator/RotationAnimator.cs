using System;
using Microsoft.Xna.Framework;
using Models.Animators;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator
{
     public class RotationAnimator : SpriteAnimator
     {
          private const string k_Name = "Rotation";
          private readonly int r_CircledPerSecond;

          public RotationAnimator(int i_CircledPerSecond, TimeSpan i_AnimationLength) 
               : this(k_Name, i_CircledPerSecond, i_AnimationLength)
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

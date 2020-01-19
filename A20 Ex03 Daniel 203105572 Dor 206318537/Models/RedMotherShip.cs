using System;
using Microsoft.Xna.Framework;
using Models.Animators;
using Models.Animators.ConcreteAnimators;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.Animators.ConcreteAnimator;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class RedMotherShip : MotherShip
     {
          private const string k_AssetName = @"Sprites\MotherShip_32x120";
          private const string k_DeadAnimatorName = "Dead";
          private const float k_BlinksPerSecond = 10f;
          private const float k_BlinkLength = 1 / k_BlinksPerSecond;
          private const float k_DeadAnimationLength = 2.2f;

          public RedMotherShip(GameScreen i_GameScreen) 
               : base(k_AssetName, i_GameScreen)
          {
               this.TintColor                    = Color.Red;
               this.Score                        = 800;
               this.Velocity                     = new Vector2(100, 0);
               this.Width                        = 120;
               this.Height                       = 32;
               this.StartingPosition             = new Vector2(-Width, 32);
               this.RotationOrigin               = new Vector2(this.Width / 2, this.Height / 2);
               this.PausePositionDuringAnimation = true;
          }

          public override void Initialize()
          {
               base.Initialize();
               CompositeAnimator deadAnimator = new CompositeAnimator(
                    k_DeadAnimatorName,
                    TimeSpan.FromSeconds(k_DeadAnimationLength),
                    this,
                    new BlinkAnimator(TimeSpan.FromSeconds(k_BlinkLength), TimeSpan.FromSeconds(k_DeadAnimationLength)),
                    new ShrinkAnimator(TimeSpan.FromSeconds(k_DeadAnimationLength)),
                    new TransparencyAnimator(this.TintColor, TimeSpan.FromSeconds(k_DeadAnimationLength)));
               deadAnimator.Finished += deadAnimator_Finished;
               this.Animations.Add(deadAnimator);
               this.Animations.Pause();
               this.Animations.Enabled = true;
          }

          private void deadAnimator_Finished(object i_Sender, EventArgs i_Args)
          {
               CompositeAnimator spriteAnimator = i_Sender as CompositeAnimator;
               spriteAnimator.Pause();
               this.Visible = false;
               this.IsDuringAnimation = false;
          }
     }
}

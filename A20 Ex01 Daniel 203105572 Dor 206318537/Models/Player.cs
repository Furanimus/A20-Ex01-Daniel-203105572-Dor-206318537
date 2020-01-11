using System;
using Microsoft.Xna.Framework;
using Models.Animators;
using Models.Animators.ConcreteAnimators;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Player : ShooterPlayer, ICollidable2D
     {
          private const int k_ScoreLostOnDestroyed = 1200;
          private const int k_RotationPerSeconed = 4;
          private const float k_BlinksPerSecond = 6f;
          private const float k_BlinkLength = 1 / k_BlinksPerSecond;
          private const float k_BlinksAnimationLength = 2.5f;
          private const float k_DeadAnimationLength = 2.5f;
          private const string k_DeadAnimatorName = "Dead";

          public Player(string i_AssetName, Game i_Game) 
               : base(i_AssetName, i_Game) 
          {
               this.Lives               = 3;
               this.Score               = 0;
               this.Width               = 32;
               this.Height              = 32;
               this.ViewDirection       = Sprite.Up;
               this.GroupRepresentative = this;
               this.RotationOrigin      = new Vector2(this.Width / 2, this.Height / 2);
          }

          public override void Initialize()
          {
               base.Initialize();

               BlinkAnimator lostLife = new BlinkAnimator(TimeSpan.FromSeconds(k_BlinkLength), TimeSpan.FromSeconds(k_BlinksAnimationLength));
               CompositeAnimator dead = new CompositeAnimator(
                    k_DeadAnimatorName,
                    TimeSpan.FromSeconds(k_DeadAnimationLength),
                    this,
                    new RotationAnimator(k_RotationPerSeconed, TimeSpan.FromSeconds(k_DeadAnimationLength)),
                    new TransparencyAnimator(this.TintColor, TimeSpan.FromSeconds(k_DeadAnimationLength)));

               dead.Finished += dead_Finished;
               lostLife.Finished += lostLife_Finished;
               
               this.Animations.Add(dead);
               this.Animations.Add(lostLife);
               this.Animations.Pause();
               this.Animations.Enabled = true;
          }

          private void lostLife_Finished(object i_Sender, EventArgs i_Args)
          {
               SpriteAnimator animator = i_Sender as SpriteAnimator;
               animator.Pause();
          }

          private void dead_Finished(object i_Sender, EventArgs i_Args)
          {
               CompositeAnimator animator = i_Sender as CompositeAnimator;
               animator.Pause();
               this.Visible = false;
               this.Enabled = false;
          }

          protected override void OnCollidedWithBullet(BaseBullet i_Bullet)
          {
               if (Lives > 0)
               {
                    Lives--;

                    if (Lives == 0)
                    {
                         executeDeadAnimation();
                    }
                    else if (Lives > 0)
                    {
                         executeLostLifeAnimation();
                    }

                    updateScore();
               }
          }

          private void updateScore()
          {
               if (Score >= k_ScoreLostOnDestroyed)
               {
                    Score -= k_ScoreLostOnDestroyed;
               }
               else
               {
                    Score = 0;
               }
          }

          private void executeDeadAnimation()
          {
               this.Animations["Blink"].Pause();
               this.Visible = true;
               this.Animations["Dead"].Resume();
          }

          private void executeLostLifeAnimation()
          {
               Position = StartingPosition;
               this.Animations["Blink"].Restart();
          }

          public object GroupRepresentative { get; set; }
     }
}

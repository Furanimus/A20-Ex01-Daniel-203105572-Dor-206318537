using System;
using Microsoft.Xna.Framework;
using Models.Animators.ConcreteAnimators;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class AlienMatrixEnemy : ShooterEnemy
     {
          private const int k_MaxShotInMidAir = 1;
          private const int k_DefaultWidth = 32;
          private const int k_DefaultHeight = 32;
          private const int k_DefaultScoreWorth = 0;
          private const string k_AssetName = @"Sprites\EnemySpriteSheet_192x32";
          private const bool k_IsStartCellAnimationFromEnd = false;

          public AlienMatrixEnemy(Rectangle i_SourceRectangle, Game i_Game)
               : this(i_SourceRectangle, k_DefaultScoreWorth, Color.White, i_Game)
          {
               CellAnimation = new CellAnimator(k_IsStartCellAnimationFromEnd, TimeSpan.FromSeconds(0.5), 2, TimeSpan.Zero);
          }

          public CellAnimator CellAnimation { get; set; }

          public AlienMatrixEnemy(Rectangle i_SourceRectangle, int i_ScoreWorth, Color i_TintColor, Game i_Game)
               : base(k_AssetName, i_SourceRectangle, i_Game)
          {
               this.Gun = new Gun(k_MaxShotInMidAir, this);
               this.Score = i_ScoreWorth;
               this.TintColor = i_TintColor;
               this.Width = k_DefaultWidth;
               this.Height = k_DefaultHeight;
               this.RotationOrigin = new Vector2(Width / 2, Height / 2);
          }

          public override void Initialize()
          {
               base.Initialize();

               RotationAnimator rotation = new RotationAnimator(6, TimeSpan.FromSeconds(1.2));
               ShrinkAnimator shrink = new ShrinkAnimator(TimeSpan.FromSeconds(1.2));
               JumpMovementAnimator jump = new JumpMovementAnimator(TimeSpan.FromSeconds(0.5), TimeSpan.Zero);
               this.Animations.Add(CellAnimation);
               this.Animations.Add(rotation);
               this.Animations.Add(shrink);
               this.Animations.Add(jump);
               this.Animations.Pause();

               CellAnimation.Resume();
               jump.Resume();
               rotation.Finished += rotationAnimator_Finished;

               this.Animations.Enabled = true;
          }

          private void rotationAnimator_Finished(object i_Sender, EventArgs i_Args)
          {
               this.Enabled = false;
               this.Visible = false;
               this.Animations.Pause();
               this.Lives--;
          }

          protected override void OnUpdate(float i_TotalSeconds)
          {
               this.Rotation += this.AngularVelocity * i_TotalSeconds;
          }

          public override bool CheckCollision(ICollidable i_Source)
          {
               bool isCollide = false;

               if(IsAlive)
               {
                    isCollide = base.CheckCollision(i_Source);
               }

               return isCollide;
          }
     }
}

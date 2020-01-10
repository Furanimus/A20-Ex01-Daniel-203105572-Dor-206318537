using System;
using Microsoft.Xna.Framework;
using Models.Animators;
using Models.Animators.ConcreteAnimators;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class RedMotherShip : MotherShip
     {
          private const string k_AssetName = @"Sprites\MotherShip_32x120";

          public RedMotherShip(Game i_Game) : base(k_AssetName, i_Game)
          {
               this.TintColor = Color.Red;
               this.Score = 800;
               this.Velocity = new Vector2(100, 0);
               this.Width = 120;
               this.Height = 32;
               this.StartingPosition = new Vector2(-Width, 32);
               this.RotationOrigin = new Vector2(this.Width / 2, this.Height / 2);
               this.PausePositionDuringAnimation = true;
          }

          public override void Initialize()
          {
               base.Initialize();
               CompositeAnimator deadAnimator = new CompositeAnimator(
                    "Dead",
                    TimeSpan.FromSeconds(2.2),
                    this,
                    new BlinkAnimator(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(2.2)),
                    new ShrinkAnimator(TimeSpan.FromSeconds(2.2)),
                    new TransparencyAnimator(this.TintColor, TimeSpan.FromSeconds(2.2)));
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

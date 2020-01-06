using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using Models.Animators;
using Models.Animators.ConcreteAnimators;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class ShooterEnemy : Enemy
     {
          private const int k_MaxShotInMidAir = 1;
          private const string k_AssetName = @"Sprites\EnemySpriteSheet_192x32";

          protected ShooterEnemy(Game i_Game) : base(k_AssetName, i_Game)
          {
               Gun = new Gun(k_MaxShotInMidAir, this);
          }

          public IGun Gun { get; set; }

          public void Shoot()
          {
               Gun.Shoot();
          }

          public override void Initialize()
          {
               base.Initialize();

               this.Animations.Add(new CellAnimator(TimeSpan.FromSeconds(0.5), 2, TimeSpan.Zero));
               this.Animations.Enabled = true;
          }
     }
}

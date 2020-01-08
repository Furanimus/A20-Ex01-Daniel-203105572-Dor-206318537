using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator;
using Microsoft.Xna.Framework;
using Models.Animators;
using Models.Animators.ConcreteAnimators;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class ShooterEnemy : Enemy
     {
          private readonly Rectangle r_SourceRectangle;

          protected ShooterEnemy(string i_AssetName, Rectangle i_SourceRectangle, Game i_Game) 
               : base(i_AssetName, i_Game)
          {
               r_SourceRectangle = i_SourceRectangle;
          }

          public IGun Gun { get; set; }

          public void Shoot()
          {
               Gun.Shoot();
          }

          protected override void InitSourceRectangle()
          {
               this.SourceRectangle = r_SourceRectangle;
          }
     }
}

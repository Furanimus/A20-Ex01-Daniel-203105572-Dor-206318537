using Microsoft.Xna.Framework;
using Models.Animators.ConcreteAnimators;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyYellow : ShooterEnemy
     {
          public EnemyYellow(Game i_Game) : base(i_Game)
          {
               TintColor = Color.LightYellow;
               Score = 100;
               Width = 32;
               Height = 32;
          }

          protected override void InitSourceRectangle()
          {
               base.InitSourceRectangle();

               SourceRectangle = new Rectangle(128, 0, (int)this.WidthBeforeScale, (int)this.HeightBeforeScale);
          }

     }
}

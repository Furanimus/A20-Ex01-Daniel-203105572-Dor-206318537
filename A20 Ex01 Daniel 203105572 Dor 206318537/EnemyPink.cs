using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyPink : Enemy, IShooter
     {
          private EnemyPink()
          {
               GraphicsPath = @"Sprites\Enemy0101_32x32";
               Score = 250;
          }

          public void Shoot()
          {
               throw new NotImplementedException();
          }
     }
}

using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyYellow : Enemy, IShooter
     {
          private EnemyYellow()
          {
               GraphicsPath = @"Sprites\Enemy0301_32x32";
               Score = 100;
          }

          public void Shoot()
          {
               throw new NotImplementedException();
          }
     }
}

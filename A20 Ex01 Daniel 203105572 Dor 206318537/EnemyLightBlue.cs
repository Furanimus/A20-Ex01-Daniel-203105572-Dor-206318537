using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyLightBlue : Enemy, IShooter
     {
          private EnemyLightBlue()
          {
               GraphicsPath = @"Sprites\Enemy0201_32x32";
               Score = 150;
          }

          public IGun Gun { get; set; }
     }
}

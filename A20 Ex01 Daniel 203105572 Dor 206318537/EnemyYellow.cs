using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyYellow : ShooterEnemy
     {
          private EnemyYellow()
          {
               GraphicsPath = @"Sprites\Enemy0301_32x32";
               //m_RandomBehavior = new RandomBehavior(150, 0, 2000);
               Score = 100;
               
          }
     }
}

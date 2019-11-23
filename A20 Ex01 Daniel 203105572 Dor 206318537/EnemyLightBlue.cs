using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyLightBlue : Enemy
     {
          private EnemyLightBlue()
          {
               GraphicsPath = @"Sprites\Enemy0201_32x32";
               Score = 150;
          }

          public override void Attack()
          {
               throw new NotImplementedException();
          }
     }
}

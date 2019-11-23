using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyYellow : Enemy
     {
          private EnemyYellow()
          {
               GraphicsPath = @"Sprites\Enemy0301_32x32";
               Score = 100;
          }

          public override void Attack()
          {
          }

          public override void Move()
          {
          }
     }
}

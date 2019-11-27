using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Gun : IGun
     {
          public int Capacity { get; set; }

          public Type BulletType { get; set; }

          public ISprite Shoot(Vector2 i_Direction)
          {
               if(BulletType == null)
               {
                    BulletType = typeof(Bullet);
               }

               return Singelton<SpriteFactory>.Instance.Create(BulletType);
          }
     }
}
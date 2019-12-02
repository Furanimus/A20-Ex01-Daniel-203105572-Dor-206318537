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
          public int Capacity { get; set; } = 2;

          public int BulletShot { get; set; }

          public Type BulletType { get; set; } = typeof(Bullet);

          public Gun()
          {

          }

          public Gun(int i_Capacity)
          {
               Capacity = i_Capacity;
          }

          public void Reload()
          {
               if(BulletShot >= 0)
               {
                    BulletShot--;
               }
          }

          public ISprite Shoot()
          {
               ISprite bullet = null;

               if(BulletType == null)
               {
                    BulletType = typeof(Bullet);
               }

               if(BulletShot < Capacity)
               {
                    bullet = Singelton<SpriteFactory>.Instance.Create(BulletType);
                    BulletShot++;
               }

               return bullet;
          }
     }
}
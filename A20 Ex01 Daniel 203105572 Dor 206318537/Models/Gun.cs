using System;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Gun : BaseGun
     {
          public int Capacity { get; set; } = 2;

          public int BulletShot { get; set; }

          public Gun()
          {
               BulletType = typeof(Bullet);
          }

          public Gun(int i_Capacity)
          {
               Capacity = i_Capacity;
          }

          public override void ReloadBullet()
          {
               if(BulletShot >= 0)
               {
                    BulletShot--;
               }
          }

          public override BaseBullet Shoot()
          {
               BaseBullet bullet = null;

               if(BulletType == null)
               {
                    BulletType = typeof(Bullet);
               }

               if(BulletShot < Capacity)
               {
                    bullet = Singelton<SpriteFactory>.Instance.Create(BulletType) as Bullet;
                    BulletShot++;
               }

               return bullet;
          }
     }
}
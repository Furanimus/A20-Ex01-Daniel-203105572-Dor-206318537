using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Gun : BaseGun
     {
          public int Capacity { get; }

          public List<BaseBullet> Bullets;

          public int BulletShot { get; set; }

          public Gun(int i_Capacity, Sprite i_Shooter) : base(i_Shooter)
          {
               Capacity = i_Capacity;
               Bullets = new List<BaseBullet>(i_Capacity);
               BulletType = typeof(Bullet);
               initBullets();
          }

          private void initBullets()
          {
               ConstructorInfo[] constructors = BulletType.GetConstructors();

               for (int bullet = 0; bullet < Capacity; bullet++)
               {
                    Bullets.Add(constructors[0].Invoke(new object[] { r_Shooter.Game }) as BaseBullet);
               }

               BulletsVelocity = Bullets[0].Velocity;
          }

          public override void ReloadBullet()
          {
               if(BulletShot >= 0)
               {
                    BulletShot--;
               }
          }

          public override void Shoot()
          {
               this.GunDirection = r_Shooter.ViewDirection;

               if(BulletShot < Capacity)
               {
                    for(int i = 0; i < Capacity; i++)
                    {
                         BaseBullet bullet = Bullets[i];

                         if (!bullet.Enabled)
                         {
                              if(r_Shooter is ICollidable)
                              {
                                   Bullets[i].GroupRepresentative = (r_Shooter as ICollidable).GroupRepresentative;
                              }

                              bullet.Position          = getShootOrigin(bullet);
                              bullet.LeftWindowBounds += onLeftWindowBounds;
                              bullet.VisibleChanged   += Bullet_VisibleChanged;
                              bullet.MoveDirection     = this.GunDirection;
                              bullet.Enabled           = true;
                              bullet.Visible           = true;
                              BulletShot++;
                              break;
                         }
                    }
               }
          }

          private void Bullet_VisibleChanged(object sender, EventArgs e)
          {
               ReloadBullet();
          }

          private void onLeftWindowBounds(object i_Sender, EventArgs i_Args)
          {
               if (i_Sender != null && i_Sender is BaseBullet)
               {
                    BaseBullet bullet = i_Sender as BaseBullet;
                    bullet.Visible = false;
                    bullet.Enabled = false;
                    ReloadBullet();
               }
          }

          private Vector2 getShootOrigin(BaseBullet i_Bullet)
          {
               Vector2 shooterCenterWidth = new Vector2(r_Shooter.Width / 2, 0);
               Vector2 bulletCenterWidth = new Vector2(i_Bullet.Width / 2, 0);

               return r_Shooter.Position + shooterCenterWidth - bulletCenterWidth;
          }
     }
}
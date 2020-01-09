﻿using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Gun : BaseGun
     {
          public Gun(int i_Capacity, Sprite i_Shooter, Action<ICollidable> i_ExecuteOnBulletCollided = null) : base(i_Shooter, i_ExecuteOnBulletCollided)
          {
               Capacity = i_Capacity;
               BulletType = typeof(Bullet);
          }

          protected override void InitializeBullets()
          {
               ConstructorInfo[] constructors = BulletType.GetConstructors();

               for (int i = 0; i < Capacity; i++)
               {
                    BaseBullet bullet = constructors[0].Invoke(new object[] { r_Shooter.Game }) as BaseBullet;
                    this.AddBullet(bullet);

                    if (r_Shooter is Player)
                    {
                         bullet.TintColor = Color.Red;
                    }
                    else if(r_Shooter is Enemy)
                    {
                         bullet.TintColor = Color.Blue;
                    }

                    if(BulletsVelocity == Vector2.Zero)
                    {
                         BulletsVelocity = bullet.Velocity;
                    }
               }
          }
     }
}
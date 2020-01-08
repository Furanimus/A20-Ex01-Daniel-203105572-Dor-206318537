using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class BaseGun
     {

          public List<BaseBullet> Bullets;
          protected readonly Sprite r_Shooter;

          public int BulletShot { get; set; }

          public int Capacity { get; protected set; } = 2;

          protected BaseGun(Sprite i_Shooter)
          {
               r_Shooter = i_Shooter;
               Bullets = new List<BaseBullet>(Capacity);
          }

          public Type BulletType { get; set; } = typeof(Bullet);

          public abstract void ReloadBullet();

          public abstract void Shoot();

          public Vector2 BulletsVelocity { get; set; } = Sprite.Down;

          public Vector2 GunDirection { get; set; } = Sprite.Down;
     }
}

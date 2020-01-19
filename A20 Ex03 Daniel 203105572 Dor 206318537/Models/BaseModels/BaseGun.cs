using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Components;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers.BaseModels
{
     public abstract class BaseGun : CompositeDrawableComponent<BaseBullet>
     {
          protected readonly Sprite r_Shooter;
          private readonly LinkedList<BaseBullet> r_Bullets;
          private readonly Action<ICollidable> r_ExecuteWhenBulletCollided;
          private int m_BulletsAdded;

          protected BaseGun(Sprite i_Shooter, Action<ICollidable> i_ExecuteWhenBulletCollided) 
               : base(i_Shooter.Game)
          {
               r_Shooter = i_Shooter;
               r_Bullets = new LinkedList<BaseBullet>();
               r_ExecuteWhenBulletCollided = i_ExecuteWhenBulletCollided;
               i_Shooter.GameScreen.Add(this);
               this.BlendState = BlendState.NonPremultiplied;
          }

          protected void AddBullet(BaseBullet i_Bullet)
          {
               if (m_BulletsAdded < Capacity)
               {
                    i_Bullet.CollidedWithSprite += r_ExecuteWhenBulletCollided;
                    m_BulletsAdded++;
                    r_Bullets.AddLast(i_Bullet);
                    this.Add(i_Bullet);
               }
          }

          protected void RemoveBullet()
          {
               if(r_Bullets.Count > 0)
               {
                    r_Bullets.RemoveFirst();
               }
          }

          protected BaseBullet GetFirstBullet()
          {
               BaseBullet first = null;

               if(r_Bullets.Count > 0)
               {
                    first = r_Bullets.First.Value;
               }

               return first;
          }

          protected virtual void ReloadBullet()
          {
               if (BulletShot > 0)
               {
                    BulletShot--;
               }
          }

          public virtual void Shoot()
          {
               if (BulletShot < Capacity)
               {
                    BulletShot++;
                    BaseBullet bullet = this.GetFirstBullet();

                    if (bullet != null)
                    {
                         this.RemoveBullet();

                         bullet.Enabled = true;
                         bullet.Visible = true;
                    }
               }
          }

          public override void Initialize()
          {
               ICollidable shooter = r_Shooter as ICollidable;

               InitializeBullets();

               if (shooter != null)
               {
                    foreach (BaseBullet bullet in r_Bullets)
                    {
                         bullet.GroupRepresentative = shooter.GroupRepresentative;
                         bullet.LeftWindowBounds += onLeftWindowBounds;
                         bullet.VisibleChanged += bullet_VisibleChanged;
                    }
               }

               base.Initialize();
          }

          public override void Update(GameTime i_GameTime)
          {
               this.GunDirection = r_Shooter.ViewDirection;

               foreach(BaseBullet bullet in r_Bullets)
               {
                    bullet.MoveDirection = this.GunDirection;
                    bullet.Position = getShootOrigin(bullet);
               }

               base.Update(i_GameTime);
          }

          private void onLeftWindowBounds(object i_Sender, EventArgs i_Args)
          {
               BaseBullet bullet = i_Sender as BaseBullet;
               OnBulletLeftWindowBounds(bullet);
          }

          protected virtual void OnBulletLeftWindowBounds(BaseBullet i_Bullet)
          {
               i_Bullet.Visible = false;
               i_Bullet.Enabled = false;
          }

          private void bullet_VisibleChanged(object i_Sender, EventArgs i_Args)
          {
               BaseBullet bullet = i_Sender as BaseBullet;

               if (!bullet.Visible)
               {
                    OnBulletNotVisible(bullet);
               }
          }

          protected virtual void OnBulletNotVisible(BaseBullet i_Bullet)
          {
               r_Bullets.AddLast(i_Bullet);
               ReloadBullet();
          }

          private Vector2 getShootOrigin(BaseBullet i_Bullet)
          {
               Vector2 shooterCenterWidth = new Vector2(r_Shooter.Width / 2, 0);
               Vector2 bulletCenterWidth = new Vector2(i_Bullet.Width / 2, 0);

               return r_Shooter.Position + shooterCenterWidth - bulletCenterWidth;
          }

          public Vector2 BulletsVelocity { get; set; }

          public Vector2 GunDirection { get; set; } = Sprite.Down;

          public int BulletShot { get; set; }

          public int Capacity { get; protected set; } = 2;

          public Type BulletType { get; set; } = typeof(Bullet);

          protected abstract void InitializeBullets();
     }
}

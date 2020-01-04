using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Player : BasePlayer, IShooter, ICollidable2D
     {
          private readonly Vector2 r_Velocity = new Vector2(110, 0);
          private const int k_MaxShotInMidAir = 2;
          private const int k_ScoreLostOnDestroyed = 1200;
          private const string k_AssetName = @"Sprites\Ship01_32x32";
          private Vector2 m_StartingPosition;

          public Player(Game i_Game) : base(k_AssetName, i_Game) 
          {
               ViewDirection = Sprite.Up;
               Lives = 3;
               Score = 0;
               Gun = new Gun(k_MaxShotInMidAir, this);
          }

          protected override void InitOrigins()
          {
               m_StartingPosition = new Vector2(
                    GraphicsDevice.Viewport.Width - (Width * 2),
                    GraphicsDevice.Viewport.Height - (Height * 2));
               Position = m_StartingPosition;

               base.InitOrigins();
          }

          public void Shoot()
          {
               Gun.Shoot();
          }

          public override void Update(GameTime i_GameTime)
          {
               CheckKeyboard();
               PrevKBState = CurrKBState;

               checkMouse();

               //removeBulletsCollided();
               base.Update(i_GameTime);
          }

          private void checkMouse()
          {
               checkMouseForMovement();
               checkMouseForShooting();
          }

          private void checkMouseForMovement()
          {
               Vector2 MovementDelta = GetMousePositionDelta();

               if(MovementDelta != Vector2.Zero)
               {
                    Position += MovementDelta; 
               }
          }

          private void checkMouseForShooting()
          {
               if(ShootingMouseState.LeftButton == ButtonState.Pressed && PrevShootingMouseState.LeftButton == ButtonState.Released)
               {
                    Shoot();
               }

               PrevShootingMouseState = ShootingMouseState;
          }

          //private void removeBulletsCollided()
          //{
          //     LinkedList<BaseBullet> bulletsToRemove;
          //     bulletsToRemove = findBulletsToRemove();

          //     foreach(BaseBullet bullet in bulletsToRemove)
          //     {
          //          removeBullet(bullet);
          //     }
          //}

          public override void Collided(ICollidable i_Collidable)
          {
               if (i_Collidable != null)
               {
                    if (i_Collidable is BaseBullet)
                    {
                         onCollidedWithBullet();
                    }
                    else if (i_Collidable is Enemy)
                    {
                         onCollidedWithEnemy();
                    }
               }
          }

          private void onCollidedWithBullet()
          {
               if (Lives > 0)
               {
                    Lives--;

                    if (Lives == 0)
                    {
                         this.Enabled = false;
                         this.Visible = false;
                    }

                    if (Score >= k_ScoreLostOnDestroyed)
                    {
                         Score -= k_ScoreLostOnDestroyed;
                    }
                    else
                    {
                         Score = 0;
                    }
               }
          }

          private void onCollidedWithEnemy()
          {
               Lives = 0;

          }

          //private LinkedList<BaseBullet> findBulletsToRemove()
          //{ 
          //     EnemyManager enemyManager = (Game as SpaceInvadersGame).EnemyManager;
          //     LinkedList<BaseBullet> toRemove = new LinkedList<BaseBullet>();

          //     foreach (BaseBullet bullet in Bullets)
          //     {
          //          foreach (Enemy enemy in enemyManager.EnemiesMatrix)
          //          {
          //               if (enemy.IsAlive)
          //               {
          //                    if (CollisionDetector.IsCollide(bullet, enemy))
          //                    {
          //                         toRemove.AddLast(bullet);
          //                         enemy.IsAlive = false;
          //                         Score += enemy.Score;
          //                    }
          //               }
          //          }

          //          if (CollisionDetector.IsCollide(bullet, enemyManager.MotherShip))
          //          {
          //               toRemove.AddLast(bullet);
          //               enemyManager.MotherShip.IsAlive = false;
          //               Score += enemyManager.MotherShip.Score;
          //          }

          //          if (bullet.Position.Y <= 0)
          //          {
          //               toRemove.AddLast(bullet);
          //          }
          //     }

          //     return toRemove;
          //}

          public void CheckKeyboard()
          {
               checkKBForMovements();
               checkKBForShooting();
          }

          private void checkKBForShooting()
          {
               if (CurrKBState.IsKeyDown(Keys.Enter) && PrevKBState.IsKeyUp(Keys.Enter))
               {
                    Shoot();
               }
          }

          private void checkKBForMovements()
          {
               if(CurrKBState.GetPressedKeys().Length != 0)
               {
                    if (CurrKBState.IsKeyDown(Keys.Right))
                    {
                         Velocity = r_Velocity * Sprite.Right;
                    }
                    else if (CurrKBState.IsKeyDown(Keys.Left))
                    {
                         Velocity = r_Velocity * Sprite.Left;
                    }
               }
               else
               {
                    Velocity = Vector2.Zero;
               }
          }
         
          public IGun Gun { get; set; }

          public int Score { get; set; }
     }
}

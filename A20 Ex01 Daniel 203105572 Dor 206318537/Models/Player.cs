﻿using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Player : BasePlayer, IShooter
     {
          private const int k_MaxShotInMidAir = 20;
          private const int k_ScoreLostOnDestroyed = 1200;
          private const string k_GraphicsPath = @"Sprites\Ship01_32x32";
          private readonly Vector2 r_StartingPosition;

          private Player(Game i_Game) : base(k_GraphicsPath, i_Game) 
          {
               Width = 32;
               Height = 32;
               Lives = 3;
               Score = 0;
               Velocity = 110;
               r_StartingPosition = new Vector2(GameEnvironment.WindowWidth - (Width * 2), GameEnvironment.WindowHeight - (Height * 2));
               Position = r_StartingPosition;
          }

          public void Shoot()
          {
               BaseBullet bullet = Gun.Shoot();

               if(bullet != null)
               {
                    bullet.LeftWindowBounds += onLeftWindowBounds;
                    bullet.SpriteBatch = m_SpriteBatch;
                    bullet.Direction = Sprite.Up;
                    bullet.Position += this.Position + new Vector2((Width / 2) - (bullet.Width / 2), 0);
                    Bullets.AddLast(bullet);
                    this.Game.Components.Add(bullet);
               }
          }

          public override void Update(GameTime i_GameTime)
          {
               CheckKeyboard();
               PrevKBState = CurrKBState;

               checkMouse();

               removeBulletsCollided();
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

          private void removeBulletsCollided()
          {
               LinkedList<BaseBullet> bulletsToRemove;
               bulletsToRemove = findBulletsToRemove();

               foreach(BaseBullet bullet in bulletsToRemove)
               {
                    removeBullet(bullet);
               }
          }

          public override void Destroyed()
          {
               if (Lives > 0)
               {
                    Lives--;
               }

               if (Score >= k_ScoreLostOnDestroyed)
               {
                    Score -= k_ScoreLostOnDestroyed;
               }
               else
               {
                    Score = 0;
               }

               Position = r_StartingPosition;
          }

          private LinkedList<BaseBullet> findBulletsToRemove()
          { 
               EnemyManager enemyManager = (Game as SpaceInvadersGame).EnemyManager;
               LinkedList<BaseBullet> toRemove = new LinkedList<BaseBullet>();

               foreach (BaseBullet bullet in Bullets)
               {
                    foreach (Enemy enemy in enemyManager.EnemiesMatrix)
                    {
                         if (enemy.IsAlive)
                         {
                              if (CollisionDetector.IsCollide(bullet, enemy))
                              {
                                   toRemove.AddLast(bullet);
                                   enemy.IsAlive = false;
                                   Score += enemy.Score;
                              }
                         }
                    }

                    if (CollisionDetector.IsCollide(bullet, enemyManager.MotherShip))
                    {
                         toRemove.AddLast(bullet);
                         enemyManager.MotherShip.IsAlive = false;
                         Score += enemyManager.MotherShip.Score;
                    }

                    if (bullet.Position.Y <= 0)
                    {
                         toRemove.AddLast(bullet);
                    }
               }

               return toRemove;
          }

          private void onLeftWindowBounds(BaseBullet i_Bullet)
          {
               removeBullet(i_Bullet);
          }

          private void removeBullet(BaseBullet i_Bullet)
          {
               if (i_Bullet != null)
               {
                    i_Bullet.Visible = false;
                    i_Bullet.Position = Vector2.Zero;
                    Bullets.Remove(i_Bullet);
                    Game.Components.Remove(i_Bullet);
                    Gun.ReloadBullet();
               }
          }

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
                    Direction = Vector2.Zero;

                    if (CurrKBState.IsKeyDown(Keys.Right))
                    {
                         Direction = Sprite.Right;
                    }
                    else if (CurrKBState.IsKeyDown(Keys.Left))
                    {
                         Direction = Sprite.Left;
                    }
               }
               else
               {
                    Direction = Vector2.Zero;
               }
          }
         
          public IGun Gun { get; set; } = new Gun(k_MaxShotInMidAir);

          public int Score { get; set; }

          public LinkedList<BaseBullet> Bullets { get; } = new LinkedList<BaseBullet>();
     }
}

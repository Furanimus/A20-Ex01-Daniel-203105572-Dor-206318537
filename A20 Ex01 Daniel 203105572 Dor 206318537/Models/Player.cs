﻿using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Player : BasePlayer, IShooter
     {
          private const int k_MaxShotInMidAir = 2;
          private const string k_GraphicsPath = @"Sprites\Ship01_32x32";

          private Player(Game i_Game) : base(k_GraphicsPath, i_Game) 
          {
               Width = 32;
               Height = 32;
               Lives = 3;
               Score = 0;
               Velocity = 110;
               Position = new Vector2(GameEnvironment.WindowWidth - Width * 2,
                    GameEnvironment.WindowHeight - Height * 2);
          }

          public void Shoot()
          {
               Sprite bullet = Gun.Shoot() as Sprite;

               if(bullet != null)
               {
                    bullet.SpriteBatch = m_SpriteBatch;
                    bullet.Direction = Sprite.Up;
                    bullet.Position += this.Position + new Vector2((Width / 2) - (bullet.Width / 2), 0);
                    Bullets.AddLast(bullet);
                    this.Game.Components.Add(bullet);
               }
          }

          public override void Update(GameTime i_GameTime)
          {
               checkKeyboard();
               PrevKBState = CurrKBState;
               updateBulletsPosition(i_GameTime);
               base.Update(i_GameTime);
          }

          private void updateBulletsPosition(GameTime i_GameTime)
          {
               EnemyManager enemyManager = (Game as SpaceInvadersGame).EnemyManager;
               LinkedList<Sprite> bulletsToRemove = new LinkedList<Sprite>();

               foreach (Sprite bullet in Bullets)
               {
                    foreach(Enemy enemy in enemyManager.EnemiesMatrix)
                    {
                         if(enemy.IsAlive)
                         {
                              if (CollisionDetector.IsCollide(bullet, enemy))
                              {
                                   bulletsToRemove.AddLast(bullet);
                                   enemy.IsAlive = false;
                                   Score += enemy.Score;
                              }
                         }
                    }

                    if (CollisionDetector.IsCollide(bullet, enemyManager.MotherShip))
                    {
                         bulletsToRemove.AddLast(bullet);
                         enemyManager.MotherShip.IsAlive = false;
                         Score += enemyManager.MotherShip.Score;
                    }

                    if (bullet.Position.Y <= 0)
                    {
                         bulletsToRemove.AddLast(bullet);
                    }
               }

               foreach (Sprite bullet in bulletsToRemove)
               {
                    bullet.Visible = false;
                    Bullets.Remove(bullet);
                    Game.Components.Remove(bullet);
                    Gun.Reload();
               }
          }

          public void checkKeyboard()
          {
               checkKBForMovements();
               checkKBForShooting();
               //Position = getMouseLocation();
          }

          private void checkKBForShooting()
          {
               if (CurrKBState.IsKeyDown(Keys.Enter) && PrevKBState.IsKeyUp(Keys.Enter))
               {
                    Shoot();
               }
          }

          private Vector2 getMouseLocation()
          {
               Vector2 retVal = Vector2.Zero;

               if (PrevMouseState != CurrMouseState)
               {
                    retVal.X = CurrMouseState.X;
                    retVal.Y = Position.Y;
               }
               else
               {
                    retVal = Position;
                    PrevMouseState = CurrMouseState;
               }

               return retVal;
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

          private Vector2 getMousePositionDelta()
          {
               Vector2 retVal = Vector2.Zero;
               m_Position.X = Mouse.GetState().X;

               if (PrevMouseState != null)
               {
                    retVal.X = (CurrMouseState.X - PrevMouseState.X);
               }

               PrevMouseState = CurrMouseState;

               return retVal;
          }

          public KeyboardState CurrKBState { get; set; }

          public KeyboardState PrevKBState { get; set; }

          public MouseState CurrMouseState { get; set; }

          public MouseState PrevMouseState { get; set; } = Mouse.GetState();

          public IGun Gun { get; set; } = new Gun(k_MaxShotInMidAir);

          public int Score { get; set; }

          public LinkedList<Sprite> Bullets { get; } = new LinkedList<Sprite>();
     }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Player : Entity, IShooter
     {
          private Player()
          {
               Width = 32;
               Height = 32;
               GraphicsPath = @"Sprites\Ship01_32x32";
               Lives = 3;
               Score = 0;
               Velocity = 110;
          }

          public KeyboardState CurrKBState { get; set; }

          public KeyboardState PrevKBState { get; set; }

          public MouseState CurrMouseState { get; set; }

          public MouseState PrevMouseState { get; set; } = Mouse.GetState();

          public IGun Gun { get; set; } = new Gun();

          public int Score { get; set; }

          internal int MaxShotsMidAir { get; set; } = 50;

          public LinkedList<ISprite> Bullets { get; } = new LinkedList<ISprite>();

          public override void Move(Vector2 i_Direction)
          {
               Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }

          public void Shoot(ContentManager i_ContentManager)
          {
               if (Bullets.Count < MaxShotsMidAir)
               {
                    Sprite bullet = Gun.Shoot() as Sprite;
                    bullet.Position = this.Position;
                    bullet.Position += new Vector2(Width / 2, 0);
                    bullet.Graphics = i_ContentManager.Load<Texture2D>(bullet.GraphicsPath);
                    Bullets.AddLast(bullet);
               }
          }

          public void HandlePlayerAction(EnemyManager i_EnemyManager, ContentManager i_ContentManager, GameTime i_GameTime)
          {
               updatePlayerMovement(i_GameTime);
               if (CurrKBState.IsKeyDown(Keys.Enter) && PrevKBState.IsKeyUp(Keys.Enter))
               {
                    Shoot(i_ContentManager);
               }

               PrevKBState = CurrKBState;
               updateBulletsPosition(i_EnemyManager, i_GameTime);
          }

          private void updateBulletsPosition(EnemyManager i_EnemyManager, GameTime i_GameTime)
          {
               bool isbulletOutOfWindowBounds = false;
               LinkedList<Sprite> bulletsToRemove = new LinkedList<Sprite>();
               bool isItersected;

               foreach (Sprite bullet in Bullets)
               {
                    foreach(Enemy enemy in i_EnemyManager.EnemiesMatrix)
                    {
                         if(enemy.IsAlive)
                         {
                              isItersected = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Width, bullet.Height).Intersects(
                              new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.Width, enemy.Height));

                              if (isItersected)
                              {
                                   bulletsToRemove.AddLast(bullet);
                                   enemy.IsAlive = false;
                                   Score += enemy.Score;
                              }
                         }
                    }

                    isItersected = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, bullet.Width, bullet.Height).Intersects(
                             new Rectangle((int) (i_EnemyManager.MotherShip as Sprite).Position.X, 
                             (int)(i_EnemyManager.MotherShip as Sprite).Position.Y,
                             (i_EnemyManager.MotherShip as Sprite).Width, (i_EnemyManager.MotherShip as Sprite).Height));

                    if (isItersected)
                    {
                         bulletsToRemove.AddLast(bullet);
                         (i_EnemyManager.MotherShip as Enemy).IsAlive = false;
                         Score += (i_EnemyManager.MotherShip as Enemy).Score;
                    }

                    if (bullet.Position.Y <= 0)
                    {
                         isbulletOutOfWindowBounds = true;
                         break;
                    }
                    else
                    {
                         bullet.GameTime = i_GameTime;
                         bullet.Move(Sprite.Up);
                    }
               }

               if (isbulletOutOfWindowBounds)
               {
                    Bullets.RemoveFirst();
               }

               foreach (Sprite bullet in bulletsToRemove)
               {
                    Bullets.Remove(bullet);
               }
          }

          private void updatePlayerMovement(GameTime i_GameTime)
          {
               updateKBMovement(i_GameTime);
               //Position = getMouseLocation();
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
          
          private void updateKBMovement(GameTime i_GameTime)
          {
               if(CurrKBState.GetPressedKeys().Length != 0)
               {
                    GameTime = i_GameTime;
                    Move(getDirection());
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

          private Vector2 getDirection()
          {
               Vector2 direction = new Vector2(0, 0);

               if (CurrKBState.IsKeyDown(Keys.Right))
               {
                    direction = Sprite.Right;
               }
               else if (CurrKBState.IsKeyDown(Keys.Left))
               {
                    direction = Sprite.Left;
               }

               return direction;
          }
     }
}

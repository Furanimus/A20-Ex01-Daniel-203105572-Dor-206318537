using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
          }

          //public override void Move(Vector2 i_Direction)
          //{
          //     Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          //}

          public void Shoot()
          {
               Sprite bullet = Gun.Shoot() as Sprite;

               if(bullet != null)
               {
                    bullet.Position = this.Position;
                    bullet.Position += new Vector2((Width / 2) - (bullet.Width / 2), 0);
                    bullet.Graphics = Game.Content.Load<Texture2D>(bullet.GraphicsPath);
                    Bullets.AddLast(bullet);
               }
          }

          public override void Update(GameTime i_GameTime)
          {
               updatePlayerMovement(i_GameTime);

               if (CurrKBState.IsKeyDown(Keys.Enter) && PrevKBState.IsKeyUp(Keys.Enter))
               {
                    Shoot();
               }

               PrevKBState = CurrKBState;
               updateBulletsPosition((Game as SpaceInvadersGame).EnemyManager, i_GameTime);
               base.Update(i_GameTime);
          }

          private void updateBulletsPosition(EnemyManager i_EnemyManager, GameTime i_GameTime)
          {
               bool isbulletOutOfWindowBounds = false;
               LinkedList<Sprite> bulletsToRemove = new LinkedList<Sprite>();

               foreach (Sprite bullet in Bullets)
               {
                    foreach(Enemy enemy in i_EnemyManager.EnemiesMatrix)
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

                    if (CollisionDetector.IsCollide(bullet, i_EnemyManager.MotherShip))
                    {
                         bulletsToRemove.AddLast(bullet);
                         i_EnemyManager.MotherShip.IsAlive = false;
                         Score += i_EnemyManager.MotherShip.Score;
                    }

                    if (bullet.Position.Y <= 0)
                    {
                         isbulletOutOfWindowBounds = true;
                    }
                    else
                    {
                         bullet.GameTime = i_GameTime;
                         bullet.Direction = Sprite.Up;
                         bullet.Update(i_GameTime);
                    }
               }

               if (isbulletOutOfWindowBounds)
               {
                    Bullets.RemoveFirst();
                    Gun.Reload();
               }

               foreach (Sprite bullet in bulletsToRemove)
               {
                    Bullets.Remove(bullet);
                    Gun.Reload();
               }
          }

          public void updatePlayerMovement(GameTime i_GameTime)
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
                    updateDirection();
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

          private void updateDirection()
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

          public KeyboardState CurrKBState { get; set; }

          public KeyboardState PrevKBState { get; set; }

          public MouseState CurrMouseState { get; set; }

          public MouseState PrevMouseState { get; set; } = Mouse.GetState();

          public IGun Gun { get; set; } = new Gun(k_MaxShotInMidAir);

          public int Score { get; set; }

          public LinkedList<Sprite> Bullets { get; } = new LinkedList<Sprite>();
     }
}

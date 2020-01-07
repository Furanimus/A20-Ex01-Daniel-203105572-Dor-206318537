﻿using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Enums;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Player : BasePlayer, ICollidable2D
     {
          private readonly Vector2 r_Velocity = new Vector2(145, 0);
          private const int k_MaxShotInMidAir = 2;
          private const int k_ScoreLostOnDestroyed = 1200;
          private readonly IInputManager r_InputManager;

          public Player(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game) 
          {
               ViewDirection         = Sprite.Up;
               Lives                 = 3;
               Score                 = 0;
               Height                = 32;
               Width                 = 32;
               Gun                   = new Gun(k_MaxShotInMidAir, this);
               r_InputManager        = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
               NoneCollisionGroupKey = this;
          }

          public Keys MoveLeftKey { get; set; } = Keys.H;

          public Keys MoveRightKey { get; set; } = Keys.K;

          public Keys ShootKey { get; set; } = Keys.U;

          public eInputButtons MouseShootButton { get; set; } = eInputButtons.Left;

          public bool IsMouseControllable { get; set; } = true;

          public override void Update(GameTime i_GameTime)
          {
               if(r_InputManager.KeyboardState.IsKeyDown(MoveLeftKey))
               {
                    Velocity = r_Velocity * Sprite.Left;
               }
               else if (r_InputManager.KeyboardState.IsKeyDown(MoveRightKey))
               {
                    Velocity = r_Velocity * Sprite.Right;
               }
               else
               {
                    Velocity = Vector2.Zero;
               }

               if (r_InputManager.KeyPressed(ShootKey) ||
                    (IsMouseControllable && r_InputManager.ButtonPressed(MouseShootButton)))
               {
                    Gun.Shoot();
               }

               if(IsMouseControllable)
               {
                    Vector2 mouseDelta = r_InputManager.MousePositionDelta;

                    if(mouseDelta != Vector2.Zero)
                    {
                         Position += mouseDelta;
                    }
               }

               base.Update(i_GameTime);
          }

          public override Vector2 Position 
          {
               get
               {
                    return base.Position;
               }

               set 
               {
                    m_Position.X = MathHelper.Clamp(value.X, 0, (float)this.GraphicsDevice.Viewport.Width - Width);
               } 
          }

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

          public IGun Gun { get; set; }

          public int Score { get; set; }

          public object NoneCollisionGroupKey { get; set; }
     }
}

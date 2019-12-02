using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
               Velocity = 110;
          }

          public KeyboardState CurrKBState { get; set; }
          public KeyboardState PrevKBState { get; set; } = Keyboard.GetState();
          public MouseState CurrMouseState { get; set; }
          public MouseState PrevMouseState { get; set; } = Mouse.GetState();

          public IGun Gun { get; set; } = new Gun();

          internal int HeldShots { get; set; } = 2;

          public override void Move(Vector2 i_Direction)
          {
               Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }

          public void Shoot(ContentManager i_ContentManager)
          {
          }

          public void UpdatePlayerMovement(GameTime i_GameTime)
          {
               updateKBMovement(i_GameTime);
               updateMouseState();
          }

          private void updateKBMovement(GameTime i_GameTime)
          {
               CurrKBState = Keyboard.GetState();
               if(CurrKBState.GetPressedKeys().Length != 0)
               {
                    GameTime = i_GameTime;
                    Move(getDirection());
               }
          }

          private void updateMouseState()
          {
               CurrMouseState = Mouse.GetState();
               if (CurrKBState.GetPressedKeys().Length == 0 && PrevKBState.GetPressedKeys().Length == 0)
               {
                    m_Position.X = CurrMouseState.X;
                    //m_Position.X += GetMouseDelta().X;
                    PrevMouseState = CurrMouseState;
               }
          }

          public Vector2 GetMouseDelta()
          {
               Vector2 result = new Vector2(0, 0);

               result.X = (CurrMouseState.X - PrevMouseState.X);
               result.Y = (CurrMouseState.Y - PrevMouseState.Y);

               return result;
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

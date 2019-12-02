using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class MotherShip : Enemy, IMotherShip
     {
          private MotherShip()
          {
               Score = 800;
               Velocity = 100;
               Width = 120;
               Height = 32;
               m_Position.X = -Width;
               m_Position.Y = 32;
               GraphicsPath = @"Sprites\MotherShip_32x120";
          }
          public bool IsOnScreen { get; set; } = false;

          public override void Move(Vector2 i_Direction)
          {
               m_Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }

          private void trySpawn()
          {
               if (m_RandomBehavior.Roll())
               {
                    IsOnScreen = true;
                    IsAlive = true;
               }
          }

          public void HandleMotherShip(GameTime i_GameTime)
          {
               if(IsOnScreen && !IsAlive)
               {
                    reset();
               }

               GameTime = i_GameTime;

               if (IsOnScreen)
               {
                    Move(Sprite.Right);

                    if (isCollideWithRightBound())
                    {
                         reset();
                    }
               }
               else
               {
                    trySpawn();
               }
          }

          private bool isCollideWithRightBound()
          {
               return (m_Position.X >= GameEnvironment.WindowWidth);
          }

          private void reset()
          {
               IsOnScreen = false;
               m_Position.X = -Width;            
          }
     }
}

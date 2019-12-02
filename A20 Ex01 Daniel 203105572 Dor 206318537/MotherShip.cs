using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class MotherShip : Enemy
     {
          private RandomBehavior m_RandomBehavior = new RandomBehavior();

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

          public void TrySpawn()
          {
               if (50 >= m_RandomBehavior.GetRandomNumber(0, 50000))
               {
                    IsOnScreen = true;
               }
          }

          public void HandleMothership()
          {
               if (IsOnScreen)
               {
                    Move(Sprite.Right);

                    if (IsCollideWithRightBound())
                    {
                         Reset();
                    }
               }
               else
               {
                    TrySpawn();
               }
          }

          public bool IsCollideWithRightBound()
          {
               return (m_Position.X >= GameEnvironment.WindowWidth);
          }

          public void Reset()
          {
               IsOnScreen = false;
               m_Position.X = 0 - Width;            
          }
     }
}

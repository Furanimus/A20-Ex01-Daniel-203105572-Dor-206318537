namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class MotherShip : Enemy
     {
          private const int k_RandomFactor = 10;
          private const int k_RandomMin = 0;
          private const int k_RandomMax= 5000;
          
          public bool IsOnScreen { get; set; } = false;
          
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

          public override void Move()
          {
               m_Position.X += Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }

          public void TrySpawn()
          {
               if (m_Random.Next(k_RandomMin, k_RandomMax) <= k_RandomFactor)
               {
                    IsOnScreen = true;
               }
          }

          public void HandleMothership()
          {
               if (IsOnScreen)
               {
                    Move();

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
               m_Position.X = 0;            
          }
     }
}

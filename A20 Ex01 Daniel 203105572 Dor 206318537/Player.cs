using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Player : BasicEntity, IShooter
     {
          private Player()
          {
               Width = 32;
               Height = 32;
               GraphicsPath = @"Sprites\Ship01_32x32";
               Lives = 3;
               Velocity = 110;
          }

          public KeyboardState KeyboardState { get; set; }

          public override void Move()
          {
               if(KeyboardState.IsKeyDown(Keys.Right))
               {
                    MoveRight();
               }
               else if(KeyboardState.IsKeyDown(Keys.Left))
               {
                    MoveLeft();
               }
          }

          public void MoveLeft()
          {
               if(Position.X > 0)
               {
                    m_Position.X -= Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
               }
          }

          public void MoveRight()
          {
               if (Position.X < GameEnvironment.WindowWidth - this.Width)
               {
                    m_Position.X += Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
               }
          }

          public void Shoot()
          {
          }
     }
}

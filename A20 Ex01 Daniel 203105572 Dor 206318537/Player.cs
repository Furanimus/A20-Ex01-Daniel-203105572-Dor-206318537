using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Player : BasicEntity
     {
          KeyboardState KeyboardState { get; set; }

          private Player()
          {
               Width = 32;
               Height = 32;
               GraphicsPath = @"Sprites\Ship01_32x32";
               Lives = 3;
               Velocity = 110;
          }

          public override void Attack()
          {
          }

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
     }
}

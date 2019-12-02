using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class RedMotherShip : MotherShip
     {
          private RedMotherShip()
          {
               Score = 800;
               Velocity = 100;
               Width = 120;
               Height = 32;
               m_Position.X = -Width;
               m_Position.Y = 32;
               GraphicsPath = @"Sprites\MotherShip_32x120";
          }

          public override void Move(Vector2 i_Direction)
          {
               m_Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }
     }
}

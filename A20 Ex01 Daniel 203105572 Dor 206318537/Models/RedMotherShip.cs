using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class RedMotherShip : MotherShip
     {
          private const string k_GraphicsPath = @"Sprites\MotherShip_32x120";


          private RedMotherShip(Game i_Game) : base(k_GraphicsPath, i_Game)
          {
               Score = 800;
               Velocity = 100;
               Width = 120;
               Height = 32;
               m_Position.X = -Width;
               m_Position.Y = 32;
          }
     }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Gun : IGun
     {
          private Bullet m_Bullet;

          public Gun()
          {
               m_Bullet = new Bullet();
          }

          public int Capacity { get; set; }

          public void Shoot(Vector2 i_Direction)
          {
               m_Bullet.Move(i_Direction); //i_Direction
          }
     }
}
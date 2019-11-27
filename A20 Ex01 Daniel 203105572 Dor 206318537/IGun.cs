using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IGun
     {
          Type BulletType { get; set; }

          ISprite Shoot(Vector2 i_Direction);
     }
}

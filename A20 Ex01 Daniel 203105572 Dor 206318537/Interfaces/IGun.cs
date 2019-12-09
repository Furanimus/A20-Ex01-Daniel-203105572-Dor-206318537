using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IGun
     {
          Type BulletType { get; set; }

          BaseBullet Shoot();

          void ReloadBullet();
     }
}

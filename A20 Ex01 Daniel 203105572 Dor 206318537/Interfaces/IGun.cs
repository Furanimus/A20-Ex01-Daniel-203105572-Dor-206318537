using System;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IGun
     {
          Type BulletType { get; set; }

          BaseBullet Shoot();

          void ReloadBullet();
     }
}

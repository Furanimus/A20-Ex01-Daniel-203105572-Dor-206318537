using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class BaseGun : IGun
     {
          public Type BulletType { get ; set ; } = typeof(Bullet);

          public abstract void ReloadBullet();

          public abstract BaseBullet Shoot();
     }
}

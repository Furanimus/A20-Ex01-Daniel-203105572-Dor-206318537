using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class ShooterEnemy : Enemy
     {
          private const int k_MaxShotInMidAir = 1;

          public ShooterEnemy(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
          {
               Gun = new Gun(k_MaxShotInMidAir, this);
          }

          public IGun Gun { get; set; }

          public void Shoot()
          {
               Gun.Shoot();
          }
     }
}

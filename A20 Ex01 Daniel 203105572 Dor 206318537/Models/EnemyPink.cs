using System;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyPink : ShooterEnemy
     {
          private const string k_GraphicsPath = @"Sprites\Enemy0101_32x32";

          private EnemyPink(Game i_Game) : base (k_GraphicsPath, i_Game)
          {
               Score = 250;
          }
     }
}

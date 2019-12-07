using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyYellow : ShooterEnemy
     {
          private const string k_GraphicsPath = @"Sprites\Enemy0301_32x32";

          private EnemyYellow(Game i_Game) : base (k_GraphicsPath, i_Game)
          {
               Score = 100;
          }
     }
}

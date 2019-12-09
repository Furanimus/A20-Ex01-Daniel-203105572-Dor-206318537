using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyLightBlue : ShooterEnemy
     {
          private const string k_GraphicsPath = @"Sprites\Enemy0201_32x32";

          private EnemyLightBlue(Game i_Game) : base(k_GraphicsPath, i_Game)
          {
               TintColor = Color.LightBlue;
               Score = 150;
          }
     }
}

using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyLightBlue : ShooterEnemy
     {
          private const string k_AssetName = @"Sprites\EnemiesSpriteSheet_192x32";

          private EnemyLightBlue(Game i_Game) : base(k_AssetName, i_Game)
          {
               TintColor = Color.LightBlue;
               Score = 150;
          }

          protected override void InitSourceRectangle()
          {
               TopLeftPosition = new Vector2(64, 0);
               Width = 64;
               Height = 32;

               base.InitSourceRectangle();
          }
     }
}

using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyPink : ShooterEnemy
     {
          private const string k_AssetName = @"Sprites\EnemiesSpriteSheet_192x32";

          private EnemyPink(Game i_Game) : base(k_AssetName, i_Game)
          {
               TintColor = Color.Pink;
               Score = 250;
          }

          protected override void InitSourceRectangle()
          {
               TopLeftPosition = new Vector2(0, 0);
               Width = 64;
               Height = 32;
               base.InitSourceRectangle();
          }
     }
}

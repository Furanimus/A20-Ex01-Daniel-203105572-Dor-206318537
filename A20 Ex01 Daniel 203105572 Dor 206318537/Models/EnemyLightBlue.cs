using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyLightBlue : ShooterEnemy
     {
          private const string k_AssetName = @"Sprites\EnemySpriteSheet_192x32";

          public EnemyLightBlue(Game i_Game) : base(k_AssetName, i_Game)
          {
               TintColor = Color.LightBlue;
               Score = 150;
               Width = 32;
               Height = 32;
          }

          protected override void InitSourceRectangle()
          {
               SourceRectangle = new Rectangle(64, 0, (int)this.WidthBeforeScale, (int)this.HeightBeforeScale);
          }
     }
}

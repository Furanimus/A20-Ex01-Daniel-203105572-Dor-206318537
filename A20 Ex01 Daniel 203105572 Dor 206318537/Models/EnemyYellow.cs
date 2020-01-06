using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyYellow : ShooterEnemy
     {
          private const string k_AssetName = @"Sprites\EnemySpriteSheet_192x32";

          public EnemyYellow(Game i_Game) : base(k_AssetName, i_Game)
          {
               TintColor = Color.LightYellow;
               Score = 100;
               Width = 32;
               Height = 32;
          }

          protected override void InitSourceRectangle()
          {
               SourceRectangle = new Rectangle(128, 0, (int)this.WidthBeforeScale, (int)this.HeightBeforeScale);
          }
     }
}

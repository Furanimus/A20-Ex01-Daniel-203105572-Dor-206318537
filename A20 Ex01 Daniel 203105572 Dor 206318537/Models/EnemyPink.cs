using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyPink : ShooterEnemy
     {

          public EnemyPink(Game i_Game) : base(i_Game)
          {
               TintColor = Color.Pink;
               Score = 250;
               Width = 32;
               Height = 32;
          }

          protected override void InitSourceRectangle()
          {
               base.InitSourceRectangle();

               SourceRectangle = new Rectangle(0, 0, (int)this.WidthBeforeScale, (int)this.HeightBeforeScale);
          }
     }
}

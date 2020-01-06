using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Bullet : BaseBullet
     {
          private const string k_GraphicPath = @"Sprites\Bullet";

          public Bullet(Game i_Game) 
              : base(k_GraphicPath, i_Game)
          {
               TintColor = Color.Red;
               Velocity = new Vector2(0, 160);
          }

          public override void Initialize()
          {
               base.Initialize();
               this.Enabled = false;
               this.Visible = false;
          }
     }
}
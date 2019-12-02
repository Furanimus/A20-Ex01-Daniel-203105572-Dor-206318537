using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Bullet : Sprite
     {
          private Bullet()
          {
               Velocity = 160;
               GraphicsPath = @"Sprites\Bullet";
               Height = 16;
               Width = 6;
          }

          public override void Move(Vector2 i_Direction)
          {
               Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }
     }
}

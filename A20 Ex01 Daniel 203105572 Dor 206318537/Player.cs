using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class Player : Entity, IShooter
     {
          private Player()
          {
               Width = 32;
               Height = 32;
               GraphicsPath = @"Sprites\Ship01_32x32";
               Lives = 3;
               Velocity = 110;
          }

          public IGun Gun { get ; set; }

          internal int HeldShots { get; set; } = 2;

          public override void Move(Vector2 i_Direction)
          {
               Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
          }
     }
}

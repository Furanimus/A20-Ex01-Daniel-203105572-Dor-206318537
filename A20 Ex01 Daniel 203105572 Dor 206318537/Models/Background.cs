using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Background : Sprite
     {
          private const string k_GraphicsPath = @"Sprites\BG_Space01_1024x768";

          public Background(Game i_Game) : base(k_GraphicsPath, i_Game)
          {
          }
     }
}

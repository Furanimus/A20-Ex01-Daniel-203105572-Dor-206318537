using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public sealed class GameEnvironment
     {
          private GameEnvironment()
          {
          }

          public Vector2 BackgroundPosition { get; set; } = Vector2.Zero;

          public int WindowHeight { get; set; } = 768;

          public int WindowWidth { get; set; } = 1024;

          public Background Background { get; set; }
     }
}
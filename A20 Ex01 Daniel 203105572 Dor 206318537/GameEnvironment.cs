using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class GameEnvironment: IGameEnvironment
     {
          public int WindowHeight { get; set; } = 768;

          public int WindowWidth { get; set; } = 1024;

          public string BackgroundPath { get; set; } = @"Sprites\BG_Space01_1024x768";

          public Texture2D Background { get; set; }
     }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     class Bullet : ISprite
     {
          public Bullet()
          {
               Velocity = 160;
               GraphicsPath = @"Sprites\Bullet.png";
               Height = 16;
               Width = 6;
          }

          public Texture2D Graphics { get; set; }

          public string GraphicsPath { get; set; }

          public GameEnvironment GameEnvironment { get; set; }

          public int Width { get; set; }

          public int Height { get; set; }

          public float Velocity { get; set; }

          public GameTime GameTime { get; set; }

          public void Move()
          {
              
          }
     }
}

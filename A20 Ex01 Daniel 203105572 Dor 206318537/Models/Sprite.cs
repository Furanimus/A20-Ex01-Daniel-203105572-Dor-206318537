using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Sprite : ISprite
     {
          protected Vector2 m_Position;

          public static Vector2 Right { get; } = new Vector2(1, 0);

          public static Vector2 Left { get; } = new Vector2(-1, 0);

          public static Vector2 Up { get; } = new Vector2(0, -1);

          public static Vector2 Down { get; } = new Vector2(0, 1);

          public Texture2D Graphics { get; set; }

          public string GraphicsPath { get; set; }

          public GameEnvironment GameEnvironment { get; set; } = Singelton<GameEnvironment>.Instance;

          public int Width {get; set; }

          public int Height { get; set; }

          public float Velocity { get; set; }

          public GameTime GameTime { get; set; }

          public Vector2 Position
          {
               get
               {
                    return m_Position;
               }
               set
               {
                    value.X = MathHelper.Clamp(value.X, 0, GameEnvironment.WindowWidth - this.Width);
                    m_Position = value;
               }
          }

          public abstract void Move(Vector2 i_Direction);
     }
}

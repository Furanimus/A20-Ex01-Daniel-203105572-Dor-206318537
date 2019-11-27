using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Sprite : ISprite
     {
          public static Vector2 Right { get; } = new Vector2(1, 0);

          public static Vector2 Left { get; } = new Vector2(-1, 0);

          public static Vector2 Up { get; } = new Vector2(0, -1);

          public static Vector2 Down { get; } = new Vector2(0, 1);

          protected Vector2 m_Position;

          public Texture2D Graphics { get; set; }

          public string GraphicsPath { get; set; }

          public GameEnvironment GameEnvironment { get; set; }

          public int Width { get; set; }

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
                    if (value.X >= 0 && value.X + Width <= GameEnvironment.WindowWidth + 1)
                    {
                         m_Position = value;
                    }
               }
          }

          public abstract void Move(Vector2 i_Direction);
     }
}

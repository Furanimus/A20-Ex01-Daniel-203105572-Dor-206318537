using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class BasicEntity : IEntity
     {
          protected Vector2 m_Position;

          public Texture2D Graphics { get; set; }

          public string GraphicsPath { get; set; }

          public GameEnvironment GameEnvironment { get; set; }

          public int Width { get; set; }

          public int Height { get; set; }

          public int Lives { get; set; }

          public bool IsAlive { get; set; } = true;

          public int Velocity { get; set; }

          public Vector2 Position { get { return m_Position; } set { m_Position = value; } }

          public KeyboardState KeyboardState { get; set; }

          public eDirection Direction { get; set; }

          public GameTime GameTime { get; set; }

          public abstract void Attack();

          public abstract void Move();
     }
}

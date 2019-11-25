﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class BasicEntity : IEntity, ISprite
     {
          protected Vector2 m_Position;

          public int Lives { get; set; }

          public bool IsAlive { get; set; } = true;

          public float Velocity { get; set; }

          public Vector2 Position { get { return m_Position; } set { m_Position = value; } }

          public GameTime GameTime { get; set; }

          public Texture2D Graphics { get; set; }

          public string GraphicsPath { get; set; }

          public GameEnvironment GameEnvironment { get ; set ; }
          public int Width { get ; set; }

          public int Height { get; set ; }

          public abstract void Move();
     }
}
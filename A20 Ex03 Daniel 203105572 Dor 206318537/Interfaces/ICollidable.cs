﻿using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ICollidable
     {
          event EventHandler<EventArgs> PositionChanged;

          event EventHandler<EventArgs> SizeChanged;

          event EventHandler<EventArgs> VisibleChanged;

          event EventHandler<EventArgs> Disposed;

          bool Visible { get; }

          bool CheckCollision(ICollidable i_Source);

          void Collided(ICollidable i_Collidable);

          object GroupRepresentative { get; set; }

          Texture2DPixels TexturePixels { get; }
     }
}

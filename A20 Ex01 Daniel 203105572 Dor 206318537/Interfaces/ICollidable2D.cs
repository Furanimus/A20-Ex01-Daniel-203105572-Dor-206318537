﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ICollidable2D : ICollidable
     {
          Rectangle Bounds { get; }
          Vector2 Velocity { get; }
     }
}
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Bullet : Sprite
     {
          private const string k_GraphicPath = @"Sprites\Bullet";

          private Bullet(Game i_Game) : base (k_GraphicPath, i_Game)
          {
               Velocity = 160;
               Height = 16;
               Width = 6;
          }
     }
}

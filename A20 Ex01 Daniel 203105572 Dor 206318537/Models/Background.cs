﻿using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Background : Sprite
     {
          private const string k_AssetName = @"Sprites\BG_Space01_1024x768";
          private const int k_CallsOrder = 1;

          public Background(Game i_Game) : base(k_AssetName, i_Game, k_CallsOrder)
          {
               this.Width = 1024;
               this.Height = 768;
          }
     }
}

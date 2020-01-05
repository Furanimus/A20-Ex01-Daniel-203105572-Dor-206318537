﻿using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Entity : Sprite
     {
          public Entity(string i_AssetName, Game i_Game)
               : base(i_AssetName, i_Game)
          {
          }

          public Entity(string i_AssetName, Game i_Game, int i_CallsOrder)
               : base(i_AssetName, i_Game, i_CallsOrder)
          {
          }

          public Entity(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
               : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
          {
          }

          public int Lives { get; set; }

          public bool IsAlive { get; set; } = true;
     }
}
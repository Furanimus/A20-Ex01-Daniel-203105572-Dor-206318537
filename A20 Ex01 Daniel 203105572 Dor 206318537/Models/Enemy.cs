﻿using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Enemy : Entity, ICollidable2D
     {
          public Enemy(string i_AssetName, Game i_Game) 
               : base(i_AssetName, i_Game)
          {
               this.Velocity = new Vector2(32, 0);
               this.Lives = 1;
               this.ViewDirection = Sprite.Down;
               this.GroupRepresentative = this;
          }

          public int Score { get; set; }

          public object GroupRepresentative { get; set; }
     }
}
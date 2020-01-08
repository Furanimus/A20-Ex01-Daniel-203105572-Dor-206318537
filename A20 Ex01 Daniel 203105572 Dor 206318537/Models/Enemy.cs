using System;
using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;
using System.Collections.Generic;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Enemy : Entity, ICollidable2D
     {
          public Enemy(string i_AssetName, Game i_Game) 
               : base(i_AssetName, i_Game)
          {
               Velocity = new Vector2(32, 0);
               Lives = 1;
               ViewDirection = Sprite.Down;
               GroupRepresentative = this;
          }

          public int Score { get; set; }

          public object GroupRepresentative { get; set; }
     }
}

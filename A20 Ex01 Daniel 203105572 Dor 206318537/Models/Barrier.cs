﻿using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Barrier : Sprite, ICollidable
     {
          private const string k_AssetName = @"Sprites\Barrier_44x32";
          private const int k_DefaultWidth = 44;
          private const int k_DefaultHeight = 32;

          public object GroupRepresentative { get; set; }

          public Barrier(Game i_Game)
               : base(k_AssetName, i_Game)
          {
               this.Width = k_DefaultWidth;
               this.Height = k_DefaultHeight;
               this.Velocity = new Vector2(45, 0);
          }

          public override void Update(GameTime i_GameTime)
          {

               base.Update(i_GameTime);
          }

          public override void Collided(ICollidable i_Collidable)
          {
               base.Collided(i_Collidable);
          }
     }
}

﻿using System;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models
{
     public class Barrier : Sprite, ICollidable2D, ISoundEmitter
     {
          private const string k_CollideSound = "BarrierHit";
          private const string k_AssetName    = @"Sprites\Barrier_44x32";
          private const int k_DefaultWidth    = 44;
          private const int k_DefaultHeight   = 32;

          public event Action<string> SoundActionOccurred;

          public object GroupRepresentative { get; set; }

          public Barrier(GameScreen i_GameScreen)
               : base(k_AssetName, i_GameScreen)
          {
               this.BlendState = BlendState.NonPremultiplied;
               this.Width = k_DefaultWidth;
               this.Height = k_DefaultHeight;
               this.GameScreen.Add(this);
          }

          public override void Collided(ICollidable i_Collidable)
          {
               if (SoundActionOccurred != null)
               {
                    SoundActionOccurred.Invoke(k_CollideSound);
               }
          }
     }
}

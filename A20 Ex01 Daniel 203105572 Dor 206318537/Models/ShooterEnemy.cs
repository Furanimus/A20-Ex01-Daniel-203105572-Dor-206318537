using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator;
using Microsoft.Xna.Framework;
using Models.Animators;
using Models.Animators.ConcreteAnimators;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class ShooterEnemy : Enemy
     {
          private readonly Rectangle r_SourceRectangle;

          protected ShooterEnemy(string i_AssetName, Rectangle i_SourceRectangle, Game i_Game) 
               : base(i_AssetName, i_Game)
          {
               r_SourceRectangle = i_SourceRectangle;
          }

          public BaseGun Gun { get; set; }

          public void Shoot()
          {
               Gun.Shoot();
          }

          public override void Initialize()
          {
               base.Initialize();
          }               

          public override void Collided(ICollidable i_Collidable)
          {
               if (this.IsAlive && i_Collidable.GroupRepresentative != this.GroupRepresentative && (i_Collidable is Bullet))
               {
                    SpriteAnimator rotationAnimator = this.Animations["Rotation"];
                    SpriteAnimator shrinkAnimator = this.Animations["Shrink"];

                    if (rotationAnimator != null)
                    {
                         rotationAnimator.Enabled = true;
                    }
                    if (shrinkAnimator != null)
                    {
                         shrinkAnimator.Enabled = true;
                    }

                    Lives--;
               }
               else if(i_Collidable is Player)
               {
                    this.Game.Exit();
               }
               else if(i_Collidable is Barrier)
               {
                    //TODO
               }
          }

          protected override void InitSourceRectangle()
          {
               this.SourceRectangle = r_SourceRectangle;
          }
     }
}

using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Bullet : BaseBullet
     {
          private const string k_GraphicPath = @"Sprites\Bullet";

          public Bullet(Game i_Game) : base(k_GraphicPath, i_Game)
          {
               this.Enabled = false;
               this.Visible = false;
               TintColor = Color.Red;
               Velocity = new Vector2(0, 160);
          }

          public override bool CheckCollision(ICollidable i_Source)
          {
               bool collided = false;

               if (i_Source.NoneCollisionGroupKey != this.NoneCollisionGroupKey)
               {
                    ICollidable2D source = i_Source as ICollidable2D;

                    if (source != null)
                    {
                         collided = source.Bounds.Intersects(this.Bounds);
                    }
               }

               return collided;
          }
     }
}
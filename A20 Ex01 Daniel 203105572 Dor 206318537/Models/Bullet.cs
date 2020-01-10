using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Bullet : BaseBullet
     {
          private const string k_GraphicPath = @"Sprites\Bullet";
          private const int k_RandomFactor = 1;
          private const int k_MaxRandom = 5;
          private const int k_MinRandom = 0;
          private readonly ICollisionsManager r_CollisionsManager;

          private readonly IRandomBehavior r_RandomBehavior;
          public Bullet(Game i_Game) : this(Color.Red, i_Game)
          {
               r_RandomBehavior = this.Game.Services.GetService(typeof(IRandomBehavior)) as IRandomBehavior;
               r_CollisionsManager = this.Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
          }

          public Bullet(Color i_TintColor, Game i_Game) : base(k_GraphicPath, i_Game)
          {
               this.Width = 6;
               this.Height = 16;
               this.Enabled = false;
               this.Visible = false;
               this.TintColor = i_TintColor;
               this.Velocity = new Vector2(0, 160);
          }

          public override void Collided(ICollidable i_Collidable)
          {
               Barrier barrier = i_Collidable as Barrier;

               if(barrier != null)
               {
                    handleBarrierCollision(barrier);
               }
               else if (i_Collidable is Bullet && i_Collidable.GroupRepresentative is BasePlayer && this.GroupRepresentative is EnemyManager)
               {
                    if(r_RandomBehavior.Roll(k_RandomFactor, k_MinRandom, k_MaxRandom))
                    {
                         base.Collided(i_Collidable);
                    }
               }
               else
               {
                    base.Collided(i_Collidable);
               }
          }

          private void handleBarrierCollision(Barrier i_Barrier)
          {
               this.Enabled = false;

               Texture2DPixels barrierPixels = i_Barrier.TexturePixels;
               Rectangle intersectedRect = r_CollisionsManager.getIntersectedRect(i_Barrier, this);

               int yDirection = (int)this.MoveDirection.Y;
               int ysToDestroy = (int)(0.7f * this.Height);
               bool isBulletOutOfBarrierBounds = false;

               for(int y = 0; y < ysToDestroy && !isBulletOutOfBarrierBounds; y++)
               {
                    for(int x = 0; x < this.Width && !isBulletOutOfBarrierBounds; x++)
                    {
                         int currY = intersectedRect.Y + (y * yDirection);
                         int currX = intersectedRect.X + x;

                         if(currY >= 0 && currY < barrierPixels.Rows &&
                              currX >= 0 && currX < barrierPixels.Cols)
                         {
                              barrierPixels[currY, currX] = new Color(barrierPixels[currY, currX], 0);
                         }
                         else
                         {
                              isBulletOutOfBarrierBounds = true;
                         }
                    }
               }

               i_Barrier.Texture.SetData(barrierPixels.Pixels);

               this.Visible = false;
          }
     }
}
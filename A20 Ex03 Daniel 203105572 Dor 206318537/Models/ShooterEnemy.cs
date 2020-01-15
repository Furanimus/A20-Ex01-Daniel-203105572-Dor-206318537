using Microsoft.Xna.Framework;
using Models.Animators;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models
{
     public abstract class ShooterEnemy : Enemy
     {
          private readonly Rectangle r_SourceRectangle;
          private readonly ICollisionsManager r_CollisionsManager;

          protected ShooterEnemy(string i_AssetName, Rectangle i_SourceRectangle, GameScreen i_GameScreen) 
               : base(i_AssetName, i_GameScreen)
          {
               r_SourceRectangle = i_SourceRectangle;
               r_CollisionsManager = this.Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
          }

          public BaseGun Gun { get; set; }

          public void Shoot()
          {
               Gun.Shoot();
          }

          public override void Collided(ICollidable i_Collidable)
          {
               bool amIAliveAndCollidedWithBullet = this.IsAlive && i_Collidable.GroupRepresentative != this.GroupRepresentative && i_Collidable is Bullet;

               if (amIAliveAndCollidedWithBullet)
               {
                    onCollidedWithBullet(i_Collidable);
               }
               else if (i_Collidable is Barrier)
               {
                    onCollidedWithBarrier(i_Collidable);
               }
          }

          private void onCollidedWithBullet(ICollidable I_Collidable)
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

          private void onCollidedWithBarrier(ICollidable i_Collidable)
          {
               Barrier barrier = i_Collidable as Barrier;
               Texture2DPixels barrierPixels = barrier.TexturePixels;
               Rectangle intersectedRect;

               r_CollisionsManager.GetIntersectedRect(barrier, this, out intersectedRect);

               for (int y = 0; y < intersectedRect.Height; y++)
               {
                    for (int x = 0; x < intersectedRect.Width; x++)
                    {
                         int currY = intersectedRect.Y + y;
                         int currX = intersectedRect.X + x;

                         if (currY >= 0 && currY < barrierPixels.Rows &&
                              currX >= 0 && currX < barrierPixels.Cols)
                         {
                              barrierPixels[currY, currX] = new Color(barrierPixels[currY, currX], 0);
                         }
                    }
               }

               barrier.Texture.SetData(barrierPixels.Pixels);
          }

          protected override void InitSourceRectangle()
          {
               this.SourceRectangle = r_SourceRectangle;
          }
     }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Components;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     public class CollisionsManager : GameService, ICollisionsManager
     {
          protected readonly List<ICollidable> r_Collidables = new List<ICollidable>();

          public CollisionsManager(Game i_Game) :
              base(i_Game, int.MaxValue)
          { 
          }

          protected override void RegisterAsService()
          {
               this.Game.Services.AddService(typeof(ICollisionsManager), this);
          }

          public void AddObjectToMonitor(ICollidable i_Collidable)
          {
               if (!this.r_Collidables.Contains(i_Collidable))
               {
                    this.r_Collidables.Add(i_Collidable);
                    i_Collidable.PositionChanged += collidable_Changed;
                    i_Collidable.SizeChanged += collidable_Changed;
                    i_Collidable.VisibleChanged += collidable_Changed;
                    i_Collidable.Disposed += collidable_Disposed;
               }
          }

          private void collidable_Disposed(object sender, EventArgs e)
          {
               ICollidable collidable = sender as ICollidable;

               if (collidable != null
                   &&
                   this.r_Collidables.Contains(collidable))
               {
                    collidable.PositionChanged -= collidable_Changed;
                    collidable.SizeChanged -= collidable_Changed;
                    collidable.VisibleChanged -= collidable_Changed;
                    collidable.Disposed -= collidable_Disposed;

                    r_Collidables.Remove(collidable);
               }
          }

          private void collidable_Changed(object sender, EventArgs e)
          {
               if (sender is ICollidable)
               {
                    Entity entity = sender as Entity;
                    bool isEntity = entity != null;
                    bool isEntityAlive = isEntity && entity.IsAlive;

                    if (!isEntity || isEntityAlive)
                    {
                         checkCollision(sender as ICollidable);
                    }
               }
          }

          private void checkCollision(ICollidable i_Source)
          {
               if (i_Source.Visible)
               {
                    List<ICollidable> collidedComponents = new List<ICollidable>();

                    foreach (ICollidable target in r_Collidables)
                    {
                         Entity entity = target as Entity;
                         bool isEntity = entity != null;
                         bool isEntityAlive = isEntity && entity.IsAlive;

                         if (i_Source != target && (!isEntity && target.Visible || isEntityAlive))
                         {
                              if (target.CheckCollision(i_Source))
                              {
                                   if(isPixelCollided(target, i_Source))
                                   {
                                        collidedComponents.Add(target);
                                   }
                              }
                         }
                    }

                    foreach (ICollidable target in collidedComponents)
                    {
                         target.Collided(i_Source);
                         i_Source.Collided(target);
                    }
               }
          }

          private bool isPixelCollided(ICollidable i_Target, ICollidable i_Source)
          {
               bool pixelCollided = false;

               Sprite target = i_Target as Sprite;
               Sprite source = i_Source as Sprite;

               if(target != null && source != null)
               {
                    Rectangle targetIntersectedRect = getIntersectedRect(target, source);
                    Rectangle sourceIntersectedRect = getIntersectedRect(source, target);

                    for (int row = 0; row < targetIntersectedRect.Height; row++)
                    {
                         for (int col = 0; col < targetIntersectedRect.Width; col++)
                         {
                              int targetRow = targetIntersectedRect.Y + row;
                              int targetCol = targetIntersectedRect.X + col;
                              int sourceRow = sourceIntersectedRect.Y + row;
                              int sourceCol = sourceIntersectedRect.X + col;

                              Color targetColor = target.TexturePixels[targetRow, targetCol];
                              Color sourceColor = source.TexturePixels[sourceRow, sourceCol];

                              if (targetColor.A != 0 && sourceColor.A != 0)
                              {
                                   pixelCollided = true;
                              }
                         }
                    }
               }

               return pixelCollided;
          }

          public Rectangle getIntersectedRect(Sprite i_Target, Sprite i_Source)
          {
               int x1 = MathHelper.Max((int)i_Target.Position.X, (int)i_Source.Position.X);
               int y1 = MathHelper.Max((int)i_Target.Position.Y, (int)i_Source.Position.Y);
               int x2 = MathHelper.Min((int)(i_Target.Position.X + i_Target.Width), (int)(i_Source.Position.X + i_Source.Width));
               int y2 = MathHelper.Min((int)(i_Target.Position.Y + i_Target.Height), (int)(i_Source.Position.Y + i_Source.Height));

               int xOrigin = MathHelper.Clamp((int)(x1 - i_Target.Position.X), 0, (int)i_Target.Width);
               int yOrigin = MathHelper.Clamp((int)(y1 - i_Target.Position.Y), 0, (int)i_Target.Height);

               int intersectedWidth = x2 - x1;
               int intersectedHeight = y2 - y1;

               return new Rectangle(xOrigin, yOrigin, intersectedWidth, intersectedHeight);
          }

          public bool IsCollideWithWindowEdge(Sprite i_Sprite)
          {
               return IsCollideWithWindowLeftEdge(i_Sprite) ||
                    IsCollideWithWindowRightEdge(i_Sprite) ||
                    IsCollideWithWindowTopEdge(i_Sprite) ||
                    IsCollideWithWindowBottomEdge(i_Sprite);
          }

          public bool IsCollideWithWindowBottomEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.Y + i_Sprite.Height >= this.Game.GraphicsDevice.Viewport.Height;
          }

          public bool IsCollideWithWindowTopEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.Y <= 0;
          }

          public bool IsCollideWithWindowRightEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.X + i_Sprite.Width >= this.Game.GraphicsDevice.Viewport.Width;
          }

          public bool IsCollideWithWindowLeftEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.X <= 0;
          }
     }
}

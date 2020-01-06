using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     public class CollisionsManager : GameService, ICollisionsManager
     {
          protected readonly List<ICollidable> m_Collidables = new List<ICollidable>();

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
               if (!this.m_Collidables.Contains(i_Collidable))
               {
                    this.m_Collidables.Add(i_Collidable);
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
                   this.m_Collidables.Contains(collidable))
               {
                    collidable.PositionChanged -= collidable_Changed;
                    collidable.SizeChanged -= collidable_Changed;
                    collidable.VisibleChanged -= collidable_Changed;
                    collidable.Disposed -= collidable_Disposed;

                    m_Collidables.Remove(collidable);
               }
          }

          private void collidable_Changed(object sender, EventArgs e)
          {
               if (sender is ICollidable)
               {
                    checkCollision(sender as ICollidable);
               }
          }

          private void checkCollision(ICollidable i_Source)
          {
               if (i_Source.Visible)
               {
                    List<ICollidable> collidedComponents = new List<ICollidable>();

                    foreach (ICollidable target in m_Collidables)
                    {
                         if (i_Source != target && target.Visible)
                         {
                              if (target.CheckCollision(i_Source))
                              {
                                   collidedComponents.Add(target);
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

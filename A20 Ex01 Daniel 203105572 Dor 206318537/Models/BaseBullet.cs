using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class BaseBullet : Sprite, ICollidable2D
     {
          public event EventHandler LeftWindowBounds;

          protected BaseBullet(string i_GraphicPath, Game i_Game) 
               : base(i_GraphicPath, i_Game)
          {
               this.LeftWindowBounds += onLeftBounds;
          }
          
          public object GroupRepresentative { get; set; }

          protected virtual void onLeftBounds(object i_Sender, EventArgs i_Args)
          {
          }

          public override void Update(GameTime gameTime)
          {
               base.Update(gameTime);

               if (LeftWindowBounds != null)
               {
                    if (this.Position.Y > this.Game.GraphicsDevice.Viewport.Height ||
                    this.Position.Y < 0 ||
                    this.Position.X > this.Game.GraphicsDevice.Viewport.Width ||
                    this.Position.X < 0)
                    {
                         LeftWindowBounds.Invoke(this, null);
                    }
               }
          }

          public override void Collided(ICollidable i_Collidable)
          {
               if(i_Collidable != null)
               {
                    this.Enabled = false;
                    this.Visible = false;
               }
          }
     }
}

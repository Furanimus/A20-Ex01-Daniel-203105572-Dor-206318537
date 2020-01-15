using System;
using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels
{
     public abstract class BaseBullet : Sprite, ICollidable2D
     {
          public event EventHandler LeftWindowBounds;

          public event Action<ICollidable> CollidedWithSprite;

          protected BaseBullet(string i_GraphicPath, Game i_Game) 
               : base(i_GraphicPath, i_Game)
          {
               this.LeftWindowBounds += OnLeftBounds;
          }

          public override void Initialize()
          {
               base.Initialize();
               this.Position = new Vector2(-Width, -Height);
          }

          public object GroupRepresentative { get; set; }

          protected virtual void OnLeftBounds(object i_Sender, EventArgs i_Args)
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
               this.Enabled = false;
               this.Visible = false;

               if(CollidedWithSprite != null)
               {
                    CollidedWithSprite.Invoke(i_Collidable);
               }
          }
     }
}
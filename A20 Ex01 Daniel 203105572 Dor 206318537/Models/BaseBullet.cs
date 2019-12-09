using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class BaseBullet : Sprite
     {
          public event Action<BaseBullet> LeftWindowBounds;

          public BaseBullet(string i_GraphicPath, Game i_Game) : 
               base (i_GraphicPath, i_Game)
          {
          }

          public override void Update(GameTime gameTime)
          {
               if (LeftWindowBounds != null)
               {
                    if (this.Position.Y > GameEnvironment.WindowHeight ||
                    this.Position.Y < 0 ||
                    this.Position.X > GameEnvironment.WindowWidth ||
                    this.Position.X < 0)
                    {
                         LeftWindowBounds.Invoke(this);
                    }
               }

               base.Update(gameTime);
          }
     }
}

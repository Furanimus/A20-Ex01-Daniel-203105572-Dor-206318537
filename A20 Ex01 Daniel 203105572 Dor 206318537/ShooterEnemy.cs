using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class ShooterEnemy : Enemy, IShooter
     {
          public ShooterEnemy()
          {
               m_RandomBehavior = new RandomBehavior(275, 0, 5010);
          }

          public IGun Gun { get; set; } = new Gun();

          public LinkedList<ISprite> Bullets { get; } = new LinkedList<ISprite>();

          public void Shoot(ContentManager i_ContentManager)
          {
               if (m_RandomBehavior.Roll())
               {
                    Sprite bullet = Gun.Shoot(Sprite.Down) as Sprite;
                    bullet.Position = this.Position;
                    bullet.Graphics = i_ContentManager.Load<Texture2D>(bullet.GraphicsPath);
                    Bullets.AddLast(bullet);
               }
          }

          public void UpdateBulletsLocation()
          {
               bool isRemove = false;

               foreach(Sprite bullet in Bullets)
               {
                    if(bullet.Position.Y >= GameEnvironment.WindowWidth)
                    {
                         isRemove = true;
                         break;
                    }
                    else
                    {
                         bullet.GameTime = this.GameTime;
                         bullet.Move(Sprite.Down);
                    }
               }

               if(isRemove)
               {
                    Bullets.RemoveFirst();
               }
          }
     }
}

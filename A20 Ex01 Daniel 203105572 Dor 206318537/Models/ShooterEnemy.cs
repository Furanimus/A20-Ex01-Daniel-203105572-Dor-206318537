using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class ShooterEnemy : Enemy, IShooter
     {
          private const int k_MaxShotInMidAir = 100;

          public ShooterEnemy(string i_GraphicsPath, Game i_Game) : base(i_GraphicsPath, i_Game)
          {
          }

          public IGun Gun { get; set; } = new Gun(k_MaxShotInMidAir);

          public LinkedList<Sprite> Bullets { get; } = new LinkedList<Sprite>();

          public void Shoot()
          {
               Sprite bullet = Gun.Shoot() as Sprite;

               if (bullet != null)
               {
                    bullet.Direction = Sprite.Down;
                    bullet.SpriteBatch = m_SpriteBatch;
                    bullet.Position = this.Position + new Vector2((Width / 2) - (bullet.Width / 2), Height);
                    Bullets.AddLast(bullet);
                    this.Game.Components.Add(bullet);
               }
          }

          public void UpdateBulletsLocation()
          {
               Sprite bulletToRemove = null;

               foreach (Sprite bullet in Bullets)
               { 
                    if (bullet.Position.Y >= GameEnvironment.WindowWidth)
                    {
                         bulletToRemove = bullet;
                         break;
                    }
               }

               if (bulletToRemove != null)
               {
                    this.Game.Components.Remove(bulletToRemove);
                    this.Bullets.Remove(bulletToRemove);
               }
          }
     }
}

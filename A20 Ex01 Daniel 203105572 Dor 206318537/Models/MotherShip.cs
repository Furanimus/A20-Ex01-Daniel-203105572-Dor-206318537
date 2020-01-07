using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class MotherShip : Enemy
     {
          public MotherShip(string i_AssetName, Game i_Game) 
               : base(i_AssetName, i_Game)
          {
               MoveDirection = Sprite.Right;
               Visible = false;
          }

          public override void Update(GameTime i_GameTime)
          {
               if(Visible && !IsAlive)
               {
                    reset();
               }

               if (Visible)
               {
                    m_Position += MoveDirection * Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;

                    if (isCollideWithRightBound())
                    {
                         reset();
                    }
               }
               else
               {
                    trySpawn();
               }
          }

          private void trySpawn()
          {
               if (m_RandomBehavior.Roll())
               {
                    Visible = true;
                    Lives++;
               }
          }

          private bool isCollideWithRightBound()
          {
               return m_Position.X >= Game.GraphicsDevice.Viewport.Width;
          }

          private void reset()
          {
               Visible = false;
               m_Position.X = -Width;
          }
     }
}

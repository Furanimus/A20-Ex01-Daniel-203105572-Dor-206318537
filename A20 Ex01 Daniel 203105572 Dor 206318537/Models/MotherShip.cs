using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class MotherShip : Enemy
     {
          public MotherShip (string i_GraphicsPath, Game i_Game) : base (i_GraphicsPath, i_Game)
          {
               Direction = Sprite.Right;
               Visible = false;
          }

          //public bool IsOnScreen { get; set; } = false;        

          public override void Update(GameTime i_GameTime)
          {
               if(Visible && !IsAlive)
               {
                    reset();
               }

               GameTime = BaseGame.GameTime;

               if (Visible)
               {
                    Position += Direction * Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;

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
                    IsAlive = true;
               }
          }

          private bool isCollideWithRightBound()
          {
               return (m_Position.X >= GameEnvironment.WindowWidth);
          }

          private void reset()
          {
               Visible = false;
               m_Position.X = -Width;            
          }
     }
}

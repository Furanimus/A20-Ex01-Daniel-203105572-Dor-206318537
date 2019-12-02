using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class MotherShip : Enemy
     {
          public bool IsOnScreen { get; set; } = false;        

          public virtual void HandleMotherShip(GameTime i_GameTime)
          {
               if(IsOnScreen && !IsAlive)
               {
                    reset();
               }

               GameTime = i_GameTime;

               if (IsOnScreen)
               {
                    Move(Sprite.Right);

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
                    IsOnScreen = true;
                    IsAlive = true;
               }
          }

          private bool isCollideWithRightBound()
          {
               return (m_Position.X >= GameEnvironment.WindowWidth);
          }

          private void reset()
          {
               IsOnScreen = false;
               m_Position.X = -Width;            
          }
     }
}

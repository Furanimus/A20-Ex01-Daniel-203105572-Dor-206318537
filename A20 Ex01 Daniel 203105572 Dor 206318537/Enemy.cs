using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Enemy: Entity
     {
          protected RandomBehavior m_RandomBehavior;

          public Enemy()
          {
               Velocity = 50;
               Height = 32;
               Width = 32;
               Lives = 1;
               m_RandomBehavior = new RandomBehavior();
          }

          public int Score { get; set; }

          public override void Move(Vector2 i_Direction)
          {
               if(Position.X + Width < GameEnvironment.WindowWidth && i_Direction == Sprite.Right || Position.X > 0 && i_Direction == Sprite.Left || i_Direction == Sprite.Down)
               {
                    if (i_Direction == Sprite.Down)
                    {
                         Position += i_Direction * (Height / 2);
                    }
                    else
                    {
                         Position += i_Direction * Velocity * (float)GameTime.ElapsedGameTime.TotalSeconds;
                    }
               }
          }
     }
}

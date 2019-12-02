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

          protected class RandomBehavior
          {
               protected Random m_Random;
               protected readonly int r_RandomFactor = 10;
               protected readonly int r_RandomMin = 0;
               protected readonly int r_RandomMax = 5000;

               public RandomBehavior()
               {
                    m_Random = new Random();
               }

               public RandomBehavior(int i_RandomFactor, int i_RandomMin, int i_RandomMax)
               {
                    m_Random = new Random();
                    r_RandomFactor = i_RandomFactor;
                    r_RandomMin = i_RandomMin;
                    r_RandomMax = i_RandomMax;

               }

               public bool Roll()
               {
                    return m_Random.Next(r_RandomMin, r_RandomMax) < r_RandomFactor;
               }
          }
     }
}

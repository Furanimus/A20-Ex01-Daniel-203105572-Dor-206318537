using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public class RandomBehavior : GameService, IRandomBehavior
     {
          private readonly Random r_Random;
          private readonly int r_RandomFactor = 10;
          private readonly int r_RandomMin = 0;
          private readonly int r_RandomMax = 5000;
          private double m_Delay;
          private double m_Timer;
          private readonly Game r_Game;

          public RandomBehavior(Game i_Game)
               : base(i_Game)
          {
               r_Game = i_Game;
               r_Random = new Random();
               this.Game.Services.AddService(typeof(IRandomBehavior), this);
          }

          public RandomBehavior(int i_RandomFactor, int i_RandomMin, int i_RandomMax, Game i_Game)
               : base(i_Game)
          {
               r_Random = new Random();
               r_RandomFactor = i_RandomFactor;
               r_RandomMin = i_RandomMin;
               r_RandomMax = i_RandomMax;
          }

          public bool Roll()
          {
               return r_Random.Next(r_RandomMin, r_RandomMax) < r_RandomFactor;
          }

          public bool Roll(int i_RandomFactor, int i_RandomMin, int i_RandomMax)
          {
               return r_Random.Next(i_RandomMin, i_RandomMax) < i_RandomFactor;
          }

          public Action DelayedAction { get; set; }

          public void TryInvokeDelayedAction()
          {
               if (m_Delay == 0)
               {
                    m_Delay = r_Random.Next(1, 10) / 6;
               }

               m_Timer += (this.r_Game as BaseGame).GameTime.ElapsedGameTime.TotalSeconds;

               if (m_Timer >= m_Delay)
               {
                    m_Timer -= m_Delay;
                    m_Delay = 0;
              
                    DelayedAction.Invoke();
               }
          }

          public int GetRandomNumber(int i_Min, int i_Max)
          {
               return r_Random.Next(i_Min, i_Max);
          }

          public TimeSpan GetRandomIntervalMilliseconds(int i_MillisecondsMaxVal)
          {
               return new TimeSpan(0, 0, 0, 0, new Random().Next(i_MillisecondsMaxVal));
          }

          public TimeSpan GetRandomIntervalSeconds(int i_SecondsMaxVal)
          {
               return new TimeSpan(0, 0, new Random().Next(i_SecondsMaxVal));
          }
     }
}

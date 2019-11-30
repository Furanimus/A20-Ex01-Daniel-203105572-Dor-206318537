using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class RandomBehavior
     {
          private readonly Random r_Random = new Random();

          public Action DelayedAction { get; set; }

          private double Delay { get; set; }

          private double Timer { get; set; }

          private GameTime GameTime { get; set; }

          public void TryInvokeDelayedAction(GameTime i_GameTime)
          {
               GameTime = i_GameTime;

               if (Delay == 0)
               {
                    Delay = r_Random.Next(1, 10) / 6;
               }

               Timer += GameTime.ElapsedGameTime.TotalSeconds;

               if (Timer >= Delay)
               {
                    Timer -= Delay;
                    Delay = 0;

                    DelayedAction.Invoke();
               }
          }

          public int GetRandomNumber(int i_Min, int i_Max)
          {
               return r_Random.Next(i_Min, i_Max);
          }
     }
}

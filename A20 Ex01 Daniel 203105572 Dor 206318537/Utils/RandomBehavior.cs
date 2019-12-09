using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public class RandomBehavior
     {
          protected readonly Random r_Random;
          protected readonly int r_RandomFactor = 10;
          protected readonly int r_RandomMin = 0;
          protected readonly int r_RandomMax = 5000;

          public RandomBehavior()
          {
               r_Random = new Random();
          }

          public RandomBehavior(int i_RandomFactor, int i_RandomMin, int i_RandomMax)
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


          public Action DelayedAction { get; set; }

          private double Delay { get; set; }

          private double Timer { get; set; }

          public void TryInvokeDelayedAction()
          {
               if (Delay == 0)
               {
                    Delay = r_Random.Next(1, 10) / 6;
               }

               Timer += BaseGame.GameTime.ElapsedGameTime.TotalSeconds;

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

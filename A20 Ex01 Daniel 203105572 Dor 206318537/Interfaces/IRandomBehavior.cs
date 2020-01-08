﻿using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public interface IRandomBehavior
     {
          bool Roll();

          bool Roll(int i_RandomFactor, int i_RandomMin, int i_RandomMax);

          Action DelayedAction { get; set; }

          void TryInvokeDelayedAction();

          int GetRandomNumber(int i_Min, int i_Max);
     }
}
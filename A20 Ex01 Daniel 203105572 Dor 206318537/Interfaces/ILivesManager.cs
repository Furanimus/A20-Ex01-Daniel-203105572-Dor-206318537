﻿using A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ILivesManager
     {
          event Action AllPlayersDied;
          void AddPlayer(BasePlayer i_Player);
          bool IsPlayerAlreadyAdded(BasePlayer i_Player);
     }
}

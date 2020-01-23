using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IPlayersManager
     {
          event Action AllPlayersDied;

          event Action<BasePlayer, ICollidable2D> PlayerCollided;

          void AddPlayer(string i_AssetName);

          void AddPlayer(BasePlayer i_Player);

          BasePlayer this[int i_Index] { get; }

          BasePlayer GetLastAddedPlayer();

          int PlayersCount { get; set; }

          void LevelReset();

          void ResetAll();
     }
}
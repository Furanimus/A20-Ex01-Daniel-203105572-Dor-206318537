using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.BaseModels;
using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
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
     }
}
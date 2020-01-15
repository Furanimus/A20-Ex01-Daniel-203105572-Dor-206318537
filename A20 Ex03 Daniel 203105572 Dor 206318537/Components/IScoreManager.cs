using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IScoreManager
     {
          void AddPlayer(BasePlayer i_Player, Color i_ScoreColor);

          bool IsPlayerAlreadyAdded(BasePlayer i_Player);
     }
}

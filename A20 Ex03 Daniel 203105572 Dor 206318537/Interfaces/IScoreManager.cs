using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens.ConcreteScreens;
using Microsoft.Xna.Framework;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IScoreManager
     {
          void AddPlayer(BasePlayer i_Player);

          void AddScreen(GameScreen i_GameScreen);

          bool IsPlayerAlreadyAdded(BasePlayer i_Player);

          void ShowResult(GameScreen i_GameScreen);

          void DrawScores(GameScreen i_GameScreen);

          Vector2 ResultPosition { get; set; }
     }
}

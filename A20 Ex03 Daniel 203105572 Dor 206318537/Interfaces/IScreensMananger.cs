using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IScreensMananger
     {
          GameScreen ActiveScreen { get; }

          void SetCurrentScreen(GameScreen i_NewScreen);

          bool Remove(GameScreen i_Screen);

          void Add(GameScreen i_Screen);
     }
}
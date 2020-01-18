using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IScreensMananger
     {
          GameScreen ActiveScreen { get; }

          void SetCurrentScreen(GameScreen i_NewScreen);

          bool Remove(GameScreen i_Screen);

          void Add(GameScreen i_Screen);

          void Push(GameScreen i_GameScreen);
     }
}
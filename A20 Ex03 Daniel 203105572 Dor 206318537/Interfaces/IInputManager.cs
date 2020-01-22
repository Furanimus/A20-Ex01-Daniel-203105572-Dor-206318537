using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IInputManager
     {
          GamePadState GamePadState { get; }

          KeyboardState KeyboardState { get; }

          MouseState MouseState { get; }

          bool ButtonIsDown(eInputButtons i_MouseButtons);

          bool ButtonIsUp(eInputButtons i_MouseButtons);

          bool ButtonsAreDown(eInputButtons i_MouseButtons);

          bool ButtonsAreUp(eInputButtons i_MouseButtons);

          bool ButtonPressed(eInputButtons i_Buttons);

          bool ButtonReleased(eInputButtons i_Buttons);

          bool ButtonsPressed(eInputButtons i_Buttons);

          bool ButtonsReleased(eInputButtons i_Buttons);

          bool KeyPressed(Keys i_Key);

          bool KeyReleased(Keys i_Key);

          bool KeyHeld(Keys i_Key);

          Vector2 MousePositionDelta { get; }

          int ScrollWheelDelta { get; }

          Vector2 LeftThumbDelta { get; }

          Vector2 RightThumbDelta { get; }

          float LeftTrigerDelta { get; }

          float RightTrigerDelta { get; }
     }
}

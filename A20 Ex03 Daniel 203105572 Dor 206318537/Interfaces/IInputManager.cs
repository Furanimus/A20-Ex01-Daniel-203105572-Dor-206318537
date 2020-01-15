using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using A20_Ex03_Daniel_203105572_Dor_206318537.Enums;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IInputManager
     {
          // Exposes the three input device states as well:
          GamePadState GamePadState { get; }

          KeyboardState KeyboardState { get; }

          MouseState MouseState { get; }

          // Allows querying buttons current state (Mouse and GamePad):
          bool ButtonIsDown(eInputButtons i_MouseButtons);

          bool ButtonIsUp(eInputButtons i_MouseButtons);

          bool ButtonsAreDown(eInputButtons i_MouseButtons);

          bool ButtonsAreUp(eInputButtons i_MouseButtons);

          // Allows querying buttons state CHANGES (Mouse and GamePad):
          bool ButtonPressed(eInputButtons i_Buttons);

          bool ButtonReleased(eInputButtons i_Buttons);

          bool ButtonsPressed(eInputButtons i_Buttons);

          bool ButtonsReleased(eInputButtons i_Buttons);

          // Allows querying KEYBOARD's state CHANGES:
          bool KeyPressed(Keys i_Key);

          bool KeyReleased(Keys i_Key);

          bool KeyHeld(Keys i_Key);

          // Allows querying all kind of analog input DELTAs:
          Vector2 MousePositionDelta { get; }

          int ScrollWheelDelta { get; }

          Vector2 LeftThumbDelta { get; }

          Vector2 RightThumbDelta { get; }

          float LeftTrigerDelta { get; }

          float RightTrigerDelta { get; }
     }
}

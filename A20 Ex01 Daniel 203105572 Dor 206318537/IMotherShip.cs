using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IMotherShip
     {
          void HandleMotherShip(GameTime i_GameTime);

          bool IsOnScreen { get; set; }
     }
}
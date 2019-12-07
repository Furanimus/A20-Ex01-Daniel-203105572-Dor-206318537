using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IMotherShip
     {
          void HandleMotherShip();

          bool IsOnScreen { get; set; }
     }
}
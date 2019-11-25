using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IEntity
     {
          int Lives { get; set; }

          bool IsAlive { get; set; }
          
          Vector2 Position { get; set; }
     }
}
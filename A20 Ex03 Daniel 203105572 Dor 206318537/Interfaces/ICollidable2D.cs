using Microsoft.Xna.Framework;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ICollidable2D : ICollidable
     {
          Rectangle Bounds { get; }

          Vector2 Velocity { get; }
     }
}

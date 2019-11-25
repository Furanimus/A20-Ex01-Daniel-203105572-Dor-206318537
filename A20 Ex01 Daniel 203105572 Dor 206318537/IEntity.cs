using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IEntity
     {
          int Lives { get; set; }

          bool IsAlive { get; set; }
          
          void Attack();

          Vector2 Position { get; set; }
     }
}
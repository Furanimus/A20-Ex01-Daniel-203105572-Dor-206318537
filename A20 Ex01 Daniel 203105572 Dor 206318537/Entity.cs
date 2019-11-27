using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Entity : Sprite 
     {
          public int Lives { get; set; }

          public bool IsAlive { get; set; } = true;
     }
}

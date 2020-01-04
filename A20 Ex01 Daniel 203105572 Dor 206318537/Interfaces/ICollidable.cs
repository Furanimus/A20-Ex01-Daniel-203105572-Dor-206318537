using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ICollidable
     {
          event EventHandler<EventArgs> PositionChanged;
          event EventHandler<EventArgs> SizeChanged;
          event EventHandler<EventArgs> VisibleChanged;
          event EventHandler<EventArgs> Disposed;
          bool Visible { get; }
          bool CheckCollision(ICollidable i_Source);
          void Collided(ICollidable i_Collidable);
     }
}

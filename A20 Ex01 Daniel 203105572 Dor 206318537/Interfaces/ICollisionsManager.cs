using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ICollisionsManager
     {
          void AddObjectToMonitor(ICollidable i_Collidable);

          bool IsCollideWithWindowEdge(Sprite i_Sprite);

          bool IsCollideWithWindowBottomEdge(Sprite i_Sprite);

          bool IsCollideWithWindowTopEdge(Sprite i_Sprite);

          bool IsCollideWithWindowRightEdge(Sprite i_Sprite);

          bool IsCollideWithWindowLeftEdge(Sprite i_Sprite);

          void getIntersectedRect(Sprite i_First, Sprite i_Second, out Rectangle o_IntersectedRect);
     }
}

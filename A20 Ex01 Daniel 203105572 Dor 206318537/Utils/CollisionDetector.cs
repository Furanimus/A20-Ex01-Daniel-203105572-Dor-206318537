using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public class CollisionDetector
     {
          private static readonly GameEnvironment sr_GameEnvironment = Singelton<GameEnvironment>.Instance;

          public static bool IsCollideWithWindowEdge(Sprite i_Sprite)
          {
               return IsCollideWithLeftEdge(i_Sprite) ||
                    IsCollideWithRightEdge(i_Sprite) ||
                    IsCollideWithTopEdge(i_Sprite) ||
                    IsCollideWithBottomEdge(i_Sprite);
          }

          public static bool IsCollideWithBottomEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.Y + i_Sprite.Height >= sr_GameEnvironment.WindowHeight;
          }

          public static bool IsCollideWithTopEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.Y <= 0;
          }

          public static bool IsCollideWithRightEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.X + i_Sprite.Width >= sr_GameEnvironment.WindowWidth;
          }

          public static bool IsCollideWithLeftEdge(Sprite i_Sprite)
          {
               return i_Sprite.Position.X <= 0;
          }

          public static bool IsCollideWithWindowEdge(Vector2 i_Position)
          {
               return IsCollideWithLeftEdge(i_Position) ||
                    IsCollideWithRightEdge(i_Position) ||
                    IsCollideWithTopEdge(i_Position) ||
                    IsCollideWithBottomEdge(i_Position);
          }

          public static bool IsCollideWithBottomEdge(Vector2 i_Position)
          {
               return i_Position.Y >= sr_GameEnvironment.WindowHeight;
          }

          public static bool IsCollideWithTopEdge(Vector2 i_Position)
          {
               return i_Position.Y <= 0;
          }

          public static bool IsCollideWithRightEdge(Vector2 i_Position)
          {
               return i_Position.X >= sr_GameEnvironment.WindowWidth;
          }

          public static bool IsCollideWithLeftEdge(Vector2 i_Position)
          {
               return i_Position.X <= 0;
          }

          public static bool IsCollide(Sprite i_First, Sprite i_Second)
          {
               return createRectangle(i_First).Intersects(createRectangle(i_Second));
          }

          private static Rectangle createRectangle(Sprite i_Sprite)
          {
               return new Rectangle((int)i_Sprite.Position.X, (int)i_Sprite.Position.Y, i_Sprite.Width, i_Sprite.Height);
          }
     }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IEntity
     {
          Texture2D Graphics { get; set; }

          GameEnvironment GameEnvironment { get; set; }

          int Width { get; set; }

          int Height { set; get; }

          int Lives { get; set; }

          bool IsAlive { get; set; }

          int Velocity { get; set; }

          KeyboardState KeyboardState { get; set; }
          
          GameTime GameTime { get; set; }

          void Attack();

          Vector2 Position { get; set; }
     }
}
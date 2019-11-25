using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface ISprite
     {
          Texture2D Graphics { get; set; }

          string GraphicsPath { get; set; }

          GameEnvironment GameEnvironment { get; set; }

          int Width { get; set; }

          int Height { get; set; }

          float Velocity { get; set; }

          GameTime GameTime { get; set; }


     }
}

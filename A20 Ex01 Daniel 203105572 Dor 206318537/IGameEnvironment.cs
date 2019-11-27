using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IGameEnvironment
     {
          int WindowHeight { get; set; }

          int WindowWidth { get; set; }

          string BackgroundPath { get; set; }

          Texture2D Background { get; set; }
     }
}

using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ISpriteFactory
     {
          Game Game { get; set; }

          Sprite Create(Type i_Type);
     }
}

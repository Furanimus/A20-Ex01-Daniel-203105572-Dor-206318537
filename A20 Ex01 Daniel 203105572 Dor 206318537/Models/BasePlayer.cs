using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class BasePlayer : Entity
     {
          public BasePlayer(string i_GraphicsPath, Game i_Game) : base(i_GraphicsPath, i_Game)
          {
          }

          public BasePlayer(string i_GraphicsPath, Game i_Game, int i_CallsOrder) : base(i_GraphicsPath, i_Game, i_CallsOrder)
          {
          }

          public BasePlayer(string i_GraphicsPath, Game i_Game, int i_UpdateOrder, int i_DrawOrder) : base(i_GraphicsPath, i_Game, i_UpdateOrder, i_DrawOrder)
          {
          }
     }
}

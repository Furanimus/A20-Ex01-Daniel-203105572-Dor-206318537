using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Enemy: BasicEntity
     {
          public Enemy()
          {
               Velocity = 50;
               Height = 32;
               Width = 32;
               Lives = 1;
          }

          public int Score { get; set; }
     }
}

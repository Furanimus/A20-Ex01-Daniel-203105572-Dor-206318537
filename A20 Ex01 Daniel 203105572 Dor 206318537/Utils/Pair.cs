using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public class Pair<T, U>
     {
          public Pair()
          {
          }

          public Pair(T first, U second)
          {
               this.First = first;
               this.Second = second;
          }

          public T First { get; set; }
          public U Second { get; set; }
     }
}

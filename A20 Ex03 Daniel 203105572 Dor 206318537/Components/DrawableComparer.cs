﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Components
{
     /// <summary>
     /// A comparer designed to assist with sorting IDrawable interfaces.
     /// </summary>
     public sealed class DrawableComparer<TDrawble> : IComparer<TDrawble>
         where TDrawble : class, IDrawable
     {
          /// <summary>
          /// A static copy of the comparer to avoid the GC.
          /// </summary>
          public static readonly DrawableComparer<TDrawble> Default;

          static DrawableComparer() { Default = new DrawableComparer<TDrawble>(); }
          private DrawableComparer() { }

          public int Compare(TDrawble x, TDrawble y)
          {
               const int k_XBigger = 1;
               const int k_Equal = 0;
               const int k_YBigger = -1;

               int retCompareResult = k_YBigger;

               if (x == null && y == null)
               {
                    retCompareResult = k_Equal;
               }
               else if (x != null)
               {
                    if (y == null)
                    {
                         retCompareResult = k_XBigger;
                    }
                    else if (x.Equals(y))
                    {
                         return k_Equal;
                    }
                    else if (x.DrawOrder > y.DrawOrder)
                    {
                         return k_XBigger;
                    }
               }

               return retCompareResult;
          }
     }
}
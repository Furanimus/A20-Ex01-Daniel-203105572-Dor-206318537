using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public class SpriteFactory : ISpriteFactory
     {
          private SpriteFactory()
          {
          }

          public Game Game { get; set; }

          public Sprite Create(Type i_Type)
          {
               Sprite result = null;

               if (typeof(Sprite).IsAssignableFrom(i_Type))
               {
                    ConstructorInfo[] ctors = i_Type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                    foreach(ConstructorInfo ctor in ctors)
                    {
                         if(ctor.IsPrivate && ctor.GetParameters().Length == 1)
                         {
                              if(ctor.GetParameters()[0].ParameterType == typeof(Game))
                              {
                                   result = ctor.Invoke(new object[] { Game }) as Sprite;
                                   break;
                              }
                         }
                    }
               }

               return result;
          }
     }
}

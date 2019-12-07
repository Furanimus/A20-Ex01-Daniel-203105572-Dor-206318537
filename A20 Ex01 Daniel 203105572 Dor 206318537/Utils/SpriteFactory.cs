using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public class SpriteFactory : ISpriteFactory
     {
          private readonly Game r_Game;

          private SpriteFactory(Game i_Game)
          {
               r_Game = i_Game;
          }

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
                                   result = ctor.Invoke(new object[] { r_Game }) as Sprite;
                                   break;
                              }
                         }
                    }
               }

               return result;
          }
     }
}

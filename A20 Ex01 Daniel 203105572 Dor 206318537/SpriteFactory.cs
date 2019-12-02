using System;
using System.Reflection;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class SpriteFactory : ISpriteFactory
     {
          private SpriteFactory()
          {
          }

          public ISprite Create(Type i_Type)
          {
               ISprite result = null;

               if (typeof(ISprite).IsAssignableFrom(i_Type))
               {
                    ConstructorInfo[] ctors = i_Type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                    foreach(ConstructorInfo ctor in ctors)
                    {
                         if(ctor.IsPrivate && ctor.GetParameters().Length == 0)
                         {
                              result = ctor.Invoke(null) as ISprite;
                              break;
                         }
                    }
               }

               return result;
          }
     }
}

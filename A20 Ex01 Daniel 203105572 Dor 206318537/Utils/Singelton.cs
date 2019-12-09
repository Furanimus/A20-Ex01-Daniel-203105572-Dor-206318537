using System;
using System.Reflection;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public static class Singelton<T> where T : class
     {
          private static T s_Instance = null;
          private static object s_Lock = new object();

          public static T Instance
          {
               get
               {
                    if (s_Instance == null)
                    {
                         lock (s_Lock)
                         {
                              if (s_Instance == null)
                              {
                                   Type type = typeof(T);
                                   ConstructorInfo[] ctors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                                   foreach (ConstructorInfo ctor in ctors)
                                   {
                                        if (ctor.IsPrivate && ctor.GetParameters().Length == 0)
                                        {
                                             s_Instance = (T)ctor.Invoke(null);
                                             break;
                                        }
                                   }
                              }
                         }
                    }

                    return s_Instance;
               }
          }
     }
}

using System;
using System.Reflection;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Utils
{
     public static class Singelton<T> where T : class
     {
          private static T m_Instance = null;
          private static object m_Lock = new object();

          public static T Instance
          {
               get
               {
                    if (m_Instance == null)
                    {
                         lock (m_Lock)
                         {
                              if (m_Instance == null)
                              {
                                   Type type = typeof(T);
                                   ConstructorInfo[] ctors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                                   foreach (ConstructorInfo ctor in ctors)
                                   {
                                        if (ctor.IsPrivate && ctor.GetParameters().Length == 0)
                                        {
                                             m_Instance = (T)ctor.Invoke(null);
                                             break;
                                        }
                                   }
                              }
                         }
                    }

                    return m_Instance;
               }
          }
     }
}

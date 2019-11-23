using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EntityFactory : IEntityFactory
     {
          private readonly GameEnvironment r_GameEnvironment;

          public EntityFactory(GameEnvironment i_GameEnvironment)
          {
               r_GameEnvironment = i_GameEnvironment;
          }

          public IEntity Create(Type i_Type)
          {
               IEntity result = null;

               if (typeof(IEntity).IsAssignableFrom(i_Type))
               {
                    ConstructorInfo[] ctors = i_Type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                    foreach(ConstructorInfo ctor in ctors)
                    {
                         if(ctor.IsPrivate && ctor.GetParameters().Length == 0)
                         {
                              result = ctor.Invoke(null) as IEntity;
                              break;
                         }
                    }

                    result.GameEnvironment = r_GameEnvironment;
               }

               return result;
          }
     }
}

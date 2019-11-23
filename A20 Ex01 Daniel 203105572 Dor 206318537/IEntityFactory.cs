using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public interface IEntityFactory
     {
          IEntity Create(Type i_Type);
     }
}

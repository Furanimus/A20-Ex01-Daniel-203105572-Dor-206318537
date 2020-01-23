using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ISoundEmitter
     {
          event Action<string> SoundActionOccurred;
     }
}

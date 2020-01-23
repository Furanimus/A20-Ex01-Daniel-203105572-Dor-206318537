using Microsoft.Xna.Framework.Audio;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface ISoundManager
     {
          void AddSoundEmitter(ISoundEmitter i_SoundEmitter);

          void RemoveSoundEmitter(ISoundEmitter i_SoundEmmiter);


          void PlayMusic(string i_SoundEffectName);

          void StopMusic(string i_SoundEffectName);
     }
}

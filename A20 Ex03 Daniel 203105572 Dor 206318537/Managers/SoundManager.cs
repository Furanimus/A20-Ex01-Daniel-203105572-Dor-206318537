using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Components;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework.Audio;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class SoundManager : GameService, ISoundManager
     {
          private readonly IInputManager r_InputManager;
          private readonly IGameSettings r_GameSettings;
          private readonly SoundBank r_SoundBank;
          private readonly WaveBank r_WaveBank;
          private readonly AudioEngine r_AudioEngine;

          public SoundManager(Game i_Game)
               : base(i_Game)
          {
               r_InputManager = i_Game.Services.GetService(typeof(IInputManager)) as IInputManager;
               r_GameSettings = i_Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;

               r_AudioEngine = new AudioEngine(@"Content\Win\Sound.xgs");
               r_WaveBank = new WaveBank(r_AudioEngine, @"Content\Win\Wave Bank.xwb");
               r_SoundBank = new SoundBank(r_AudioEngine, @"Content\Win\Sound Bank.xsb");
          }

          public void AddSoundEmitter(ISoundEmitter i_SoundEmmiter)
          {
               i_SoundEmmiter.ActionOccurred += soundEmitter_ActionOccurred;
          }

          public void RemoveSoundEmitter(ISoundEmitter i_SoundEmmiter)
          {
               i_SoundEmmiter.ActionOccurred -= soundEmitter_ActionOccurred;
          }

          private void soundEmitter_ActionOccurred(string i_SoundEffectPath)
          {
               PlayMusic(i_SoundEffectPath);
          }
          
          public void PlayMusic(string i_SoundEffectName)
          {
               Cue soundCue = r_SoundBank.GetCue(i_SoundEffectName);
               soundCue.Play();
          }

          public void StopMusic(string i_SoundEffectName)
          {
               Cue soundCue = r_SoundBank.GetCue(i_SoundEffectName);
               soundCue.Stop(AudioStopOptions.Immediate);
          }

          public override void Update(GameTime i_GameTime)
          {


               base.Update(i_GameTime);
          }
     }
}

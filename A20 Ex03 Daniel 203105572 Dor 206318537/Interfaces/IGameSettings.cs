using Microsoft.Xna.Framework;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IGameSettings
     {
          event EventHandler PlayersCountChanged;

          event EventHandler FullScreenChanged;

          event EventHandler MouseVisibleChanged;

          event EventHandler WindowResizeAllowChanged;

          event EventHandler BackgroundMusicVolumeChanged;

          event EventHandler SoundEffectsVolumeChanged;

          event EventHandler SoundChanged;

          GraphicsDeviceManager GraphicsDeviceManager { get; }

          int PlayersCount { get; set; }

          bool IsFullScreen { get; set; }

          bool IsMouseVisible { get; set; }

          bool IsWindowResizeAllow { get; set; }

          int BackgroundMusicVolume { get; set; }

          int SoundEffectsVolume { get; set; }

          bool IsSound { get; set; }
     }
}

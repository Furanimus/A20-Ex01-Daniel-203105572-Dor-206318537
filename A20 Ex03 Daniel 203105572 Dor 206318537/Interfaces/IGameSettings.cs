using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces
{
     public interface IGameSettings
     {
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

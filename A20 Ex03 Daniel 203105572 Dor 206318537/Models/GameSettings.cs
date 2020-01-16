using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class GameSettings : IGameSettings
     {
          public GameSettings(Game i_Game)
          {
               i_Game.Services.AddService(typeof(IGameSettings), this);
          }

          public int PlayersCount { get; set; } = 1;

          public bool IsFullScreen { get; set; }

          public bool IsMouseVisible { get; set; }

          public bool IsWindowResizeAllow { get; set; }

          public int BackgroundMusicVolume { get; set; } = 100;

          public int SoundEffectsVolume { get; set; } = 100;

          public bool IsSound { get; set; } = true;
     }
}

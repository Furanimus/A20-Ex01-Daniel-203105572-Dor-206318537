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
          private bool m_IsFullScreen;
          private readonly Game r_Game;

          public GameSettings(Game i_Game)
          {
               GraphicsDeviceManager = new GraphicsDeviceManager(i_Game);
               i_Game.Services.AddService(typeof(IGameSettings), this);
               this.r_Game = i_Game;
          }

          public int PlayersCount { get; set; } = 1;

          public bool IsFullScreen
          {
               get
               {
                    return m_IsFullScreen;
               }

               set
               {
                    if(m_IsFullScreen != value)
                    {
                         m_IsFullScreen = value;
                         GraphicsDeviceManager.ToggleFullScreen();
                    }
               }
          }

          public bool IsMouseVisible
          {
               get
               {
                    return r_Game.IsMouseVisible;
               }
               set
               {
                    if (r_Game.IsMouseVisible != value)
                    {
                         r_Game.IsMouseVisible = value;
                    }
               }
          }

          public bool IsWindowResizeAllow
          {
               get
               {
                    return r_Game.Window.AllowUserResizing;
               }
               set
               {
                    if (r_Game.Window.AllowUserResizing != value)
                    {
                         r_Game.Window.AllowUserResizing = value;
                    }
               }
          }

          public int BackgroundMusicVolume { get; set; } = 100;

          public int SoundEffectsVolume { get; set; } = 100;

          public bool IsSound { get; set; } = true;
          public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
     }
}

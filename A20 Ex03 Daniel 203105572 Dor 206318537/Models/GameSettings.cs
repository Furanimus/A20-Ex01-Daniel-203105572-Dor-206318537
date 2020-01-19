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
          public event EventHandler PlayersCountChanged;
          public event EventHandler IsFullScreenChanged;
          public event EventHandler IsMouseVisibleChanged;
          public event EventHandler IsWindowResizeAllowChanged;
          public event EventHandler BackgroundMusicVolumeChanged;
          public event EventHandler SoundEffectsVolumeChanged;
          public event EventHandler IsSoundChanged;
          
          private bool m_IsFullScreen;
          private readonly Game r_Game;
          private int m_PlayersCount = 1;
          private bool m_IsMouseVisible;
          private bool m_IsWindowResizeAllow;
          private int m_BackgroundMusicVolume = 100;
          private int m_SoundEffectsVolume = 100;
          private bool m_IsSound = true;

          public GameSettings(Game i_Game)
          {
               GraphicsDeviceManager = new GraphicsDeviceManager(i_Game);
               i_Game.Services.AddService(typeof(IGameSettings), this);
               this.r_Game = i_Game;
          }

          public int PlayersCount
          {
               get
               {
                    return m_PlayersCount;
               }

               set
               {
                    m_PlayersCount = value;

                    if (PlayersCountChanged != null)
                    {
                         PlayersCountChanged.Invoke(this, null);
                    }
               }
          }

          public bool IsFullScreen
          {
               get
               {
                    return m_IsFullScreen;
               }

               set
               {
                    if (m_IsFullScreen != value)
                    {
                         m_IsFullScreen = value;
                         GraphicsDeviceManager.ToggleFullScreen();

                         if (IsFullScreenChanged != null)
                         {
                              IsFullScreenChanged.Invoke(this, null);
                         }
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

                         if (IsMouseVisibleChanged != null)
                         {
                              IsMouseVisibleChanged.Invoke(this, null);
                         }
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

                         if (IsWindowResizeAllowChanged != null)
                         {
                              IsWindowResizeAllowChanged.Invoke(this, null);
                         }
                    }
               }
          }

          public int BackgroundMusicVolume
          {
               get
               {
                    return m_BackgroundMusicVolume;
               }

               set
               {
                    m_BackgroundMusicVolume = value;

                    if(BackgroundMusicVolumeChanged != null)
                    {
                         BackgroundMusicVolumeChanged.Invoke(this, null);
                    }
               }
          }


          public int SoundEffectsVolume
          {
               get
               {
                    return m_SoundEffectsVolume;
               }

               set
               {
                    m_SoundEffectsVolume = value;

                    if(SoundEffectsVolumeChanged != null)
                    {
                         SoundEffectsVolumeChanged.Invoke(this, null);
                    }
               }
          }

          public bool IsSound
          {
               get
               {
                    return m_IsSound;
               }

               set
               {
                    m_IsSound = value;

                    if(IsSoundChanged != null)
                    {
                         IsSoundChanged.Invoke(this, null);
                    }
               }
          }

          public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
     }
}

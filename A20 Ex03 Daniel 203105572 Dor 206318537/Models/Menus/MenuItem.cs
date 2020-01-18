using A20_Ex03_Daniel_203105572_Dor_206318537.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus
{
     public class MenuItem : Sprite
     {
          private readonly List<KeyValuePair<eInputButtons, Action<MenuItem>>> r_ButtonsToActions;
          private readonly List<KeyValuePair<Keys, Action<MenuItem>>> r_KeysToActions;
          private readonly Action<MenuItem> r_CheckMouseOrKBState;
          protected readonly IInputManager r_InputManager;
          private StrokeSpriteFont m_StrokeSpriteFont;
          private Action<MenuItem> m_WheelUpAction;
          private Action<MenuItem> m_WheelDownAction;
          private Action<MenuItem> m_WheelAction;

          public MenuItem(StrokeSpriteFont i_StrokeSpriteFont, GameScreen i_GameScreen, Menu i_LinkedMenu = null, Action<MenuItem> i_CheckMosueOrKBState = null) 
               : base("", i_GameScreen)
          {
               r_CheckMouseOrKBState = i_CheckMosueOrKBState;
               r_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
               r_KeysToActions = new List<KeyValuePair<Keys, Action<MenuItem>>>();
               r_ButtonsToActions = new List<KeyValuePair<eInputButtons, Action<MenuItem>>>();

               this.StrokeSpriteFont = i_StrokeSpriteFont;
               this.VisibleChanged += menuItem_VisibleChanged;
               this.LinkedMenu = i_LinkedMenu;
               this.BlendState = BlendState.NonPremultiplied;
          }

          public MenuItem(string i_Text, GameScreen i_GameScreen, Menu i_LinkedMenu = null, Action<MenuItem> i_CheckMosueOrKBState = null)
               : this(new StrokeSpriteFont(i_Text, i_GameScreen), i_GameScreen, i_LinkedMenu, i_CheckMosueOrKBState)
          {
          }

          public void BindActionToKeys(Action<MenuItem> i_Action, params Keys[] i_Keys)
          {
               foreach(Keys key in i_Keys)
               {
                    r_KeysToActions.Add(new KeyValuePair<Keys, Action<MenuItem>>(key, i_Action));
               }
          }

          public void BindActionToMouseButtons(Action<MenuItem> i_Action, params eInputButtons[] i_Buttons)
          {
               foreach (eInputButtons button in i_Buttons)
               {
                    r_ButtonsToActions.Add(new KeyValuePair<eInputButtons, Action<MenuItem>>(button, i_Action));
               }
          }

          public void BindActionToMouseWheelUp(Action<MenuItem> i_Action)
          {
               m_WheelUpAction = i_Action;
          }

          public void BindActionToMouseWheelDown(Action<MenuItem> i_Action)
          {
               m_WheelDownAction = i_Action;
          }

          public void BindActionToMouseWheel(Action<MenuItem> i_Action)
          {
               m_WheelAction = i_Action;
          }

          private void menuItem_VisibleChanged(object i_Sender, EventArgs i_Args)
          {
               m_StrokeSpriteFont.Enabled = this.Visible;
               m_StrokeSpriteFont.Visible = this.Visible;
               this.Enabled = this.Visible;
          }

          public bool IsInitialized { get; private set; }

          public Menu LinkedMenu { get; set; }

          public bool IsFocused { get; set; }

          public override float Opacity
          {
               get
               {
                    return (float)TintColor.A / (float)byte.MaxValue;
               }

               set
               {
                    TintColor = new Color(TintColor, (byte)(value * (float)byte.MaxValue));
                    m_StrokeSpriteFont.Opacity = value;
               }
          }

          public StrokeSpriteFont StrokeSpriteFont
          {
               get
               {
                    return m_StrokeSpriteFont;
               }

               set
               {
                    if (m_StrokeSpriteFont == null || m_StrokeSpriteFont != null && value.Text != string.Empty)
                    {
                         m_StrokeSpriteFont = value;
                         m_StrokeSpriteFont.Position = this.Position;
                         this.GameScreen.Add(m_StrokeSpriteFont);
                    }
               }
          }

          public override Vector2 Position
          {
               get
               {
                    return m_Position;
               }

               set
               {
                    m_Position = value;

                    if(StrokeSpriteFont.Position == Vector2.Zero)
                    {
                         StrokeSpriteFont.Position = value;
                    }
               }
          }

          public override void Initialize()
          {
               if (!IsInitialized)
               {
                    base.Initialize();

                    IsInitialized = true;

                    if (!StrokeSpriteFont.IsInitialized)
                    {
                         StrokeSpriteFont.Initialize();
                    }
               }
          }

          private void checkMosueOrKBStateCheckClick()
          {
               if (r_CheckMouseOrKBState != null && IsFocused)
               {
                    r_CheckMouseOrKBState.Invoke(this);
               }
          }

          public void Focus()
          {
               IsFocused = true;

               if (IsInitialized)
               {
                    this.StrokeSpriteFont.TintColor = Color.DeepSkyBlue;
                    this.StrokeSpriteFont.Animations.Restart();
               }
          }

          public void UnFocus()
          {
               IsFocused = false;

               if (IsInitialized)
               {
                    this.StrokeSpriteFont.TintColor = Color.White;
                    this.StrokeSpriteFont.Animations.Restart();
                    this.StrokeSpriteFont.Animations.Pause();
               }
          }

          public override void Update(GameTime i_GameTime)
          {
               if(IsFocused)
               {
                    checkMosueOrKBStateCheckClick();

                    foreach(KeyValuePair<Keys, Action<MenuItem>> bind in r_KeysToActions)
                    {
                         if(r_InputManager.KeyPressed(bind.Key))
                         {
                              bind.Value.Invoke(this);
                         }
                    }

                    foreach (KeyValuePair<eInputButtons, Action<MenuItem>> bind in r_ButtonsToActions)
                    {
                         if (r_InputManager.ButtonPressed(bind.Key))
                         {
                              bind.Value.Invoke(this);
                         }
                    }

                    if(m_WheelAction != null)
                    {
                         if (r_InputManager.ScrollWheelDelta != 0)
                         {
                              m_WheelAction.Invoke(this);
                         }
                    }
                    else if(m_WheelUpAction != null)
                    {
                         if (r_InputManager.ScrollWheelDelta > 0)
                         {
                              m_WheelUpAction.Invoke(this);
                         }
                    }
                    else if(m_WheelDownAction != null)
                    {
                         if (r_InputManager.ScrollWheelDelta < 0)
                         {
                              m_WheelDownAction.Invoke(this);
                         }
                    }
               }

               base.Update(i_GameTime);
          }
     }
}

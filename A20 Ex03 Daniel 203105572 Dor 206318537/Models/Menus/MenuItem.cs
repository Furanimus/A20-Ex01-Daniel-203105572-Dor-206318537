using A20_Ex03_Daniel_203105572_Dor_206318537.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens.Animators.ConcreteAnimator;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.Menus
{
     public class MenuItem : Sprite
     {
          private readonly Action<MenuItem, Keys> r_ExecuteOnClick;
          private StrokeSpriteFont m_StrokeSpriteFont;

          public MenuItem(StrokeSpriteFont i_StrokeSpriteFont, Action<MenuItem, Keys> i_ExecuteOnClick, GameScreen i_GameScreen, Menu i_LinkedMenu = null) 
               : base("", i_GameScreen)
          {
               r_ExecuteOnClick = i_ExecuteOnClick;
               this.StrokeSpriteFont = i_StrokeSpriteFont;
               this.VisibleChanged += menuItem_VisibleChanged;
               this.LinkedMenu = i_LinkedMenu;
               this.BlendState = BlendState.NonPremultiplied;
          }

          public MenuItem(string i_Text, Action<MenuItem, Keys> i_ExecuteOnClick, GameScreen i_GameScreen, Menu i_LinkedMenu = null)
               : this(new StrokeSpriteFont(i_Text, i_GameScreen), i_ExecuteOnClick, i_GameScreen, i_LinkedMenu)
          {
          }

          private void menuItem_VisibleChanged(object i_Sender, EventArgs i_Args)
          {
               m_StrokeSpriteFont.Enabled = this.Visible;
               m_StrokeSpriteFont.Visible = this.Visible;
               this.Enabled = this.Visible;
          }

          public bool IsInitialized { get; private set; }

          public Menu LinkedMenu { get; set; }

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

          public void Click(Keys i_ClickedKey)
          {
               if (r_ExecuteOnClick != null)
               {
                    r_ExecuteOnClick.Invoke(this, i_ClickedKey);
               }

               if(LinkedMenu != null)
               {
                    LinkedMenu.Visible = true;
               }
          }

          public void Focus()
          {
               if (IsInitialized)
               {
                    this.StrokeSpriteFont.TintColor = Color.DeepSkyBlue;
                    this.StrokeSpriteFont.Animations.Restart();
               }
          }

          public void UnFocus()
          {
               if (IsInitialized)
               {
                    this.StrokeSpriteFont.TintColor = Color.White;
                    this.StrokeSpriteFont.Animations.Restart();
                    this.StrokeSpriteFont.Animations.Pause();
               }
          }
     }
}

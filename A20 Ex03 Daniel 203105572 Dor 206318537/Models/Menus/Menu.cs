﻿using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using A20_ex03_Daniel_203105572_Dor_206318537.Models.Menus;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models.Menus
{
     public abstract class Menu : Sprite
     {
          private const int k_Spacing = 40;
          private const string k_FontAssetName = @"Fonts\ArialFont";
          private const float k_TitleHeight = 100;
          private readonly List<MenuItem> r_Options;
          protected readonly IInputManager r_InputManager;
          protected Menu m_PrevMenu;
          private int m_CurrentOptionIndex = -1;

          protected Menu(StrokeSpriteFont i_Title, GameScreen i_GameScreen) 
               : base("", i_GameScreen)
          {
               r_Options = new List<MenuItem>();
               r_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;

               this.VisibleChanged += menu_VisibleChanged;
               this.BlendState = BlendState.NonPremultiplied;
               this.GameSettings = this.Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;
               this.Title = i_Title;
               this.Title.Visible = false;
               i_GameScreen.Add(this);
               i_GameScreen.Add(Title);
          }

          protected Menu(string i_Title, GameScreen i_GameScreen)
               : this(new StrokeSpriteFont(i_Title, i_GameScreen), i_GameScreen)
          {
          }

          public StrokeSpriteFont Title { get; set; }

          protected Vector2 NextPosition { get; set; }

          protected IGameSettings GameSettings { get; set; }

          private void menu_VisibleChanged(object i_Sender, EventArgs i_Args)
          {
               if (this.Visible)
               {
                    this.ShowItems();
                    unFocusCurrentOption();
                    m_CurrentOptionIndex = -1;
               }
               else
               {
                    this.HideItems();
               }
               
               this.Enabled = this.Visible;
               this.Title.Visible = this.Visible;
          }

          public string FontAssetName { get; set; } = k_FontAssetName;

          public int Spacing { get; set; } = k_Spacing;

          public MenuItem this[int i_Index]
          {
               get
               {
                    return r_Options[i_Index];
               }
          }

          public override float Opacity
          {
               get
               {
                    return (float)TintColor.A / (float)byte.MaxValue;
               }

               set
               {
                    TintColor = new Color(TintColor, (byte)(value * (float)byte.MaxValue));
                    this.Title.Opacity = value;

                    foreach(MenuItem menuItem in r_Options)
                    {
                         menuItem.Opacity = value;
                    }
               }
          }

          public int CurrentOptionIndex
          {
               get
               {
                    return m_CurrentOptionIndex;
               }

               set
               {
                    unFocusCurrentOption();

                    if (value < 0)
                    {
                         m_CurrentOptionIndex = r_Options.Count - 1;
                    }
                    else
                    {
                         m_CurrentOptionIndex = value % r_Options.Count;
                    }

                    focusCurrentOption();
               }
          }

          private void focusCurrentOption()
          {
               if (m_CurrentOptionIndex >= 0 && m_CurrentOptionIndex < r_Options.Count)
               {
                    if (!CurrentOption.IsFocused)
                    {
                         CurrentOption.Focus();
                    }
               }
          }

          private void unFocusCurrentOption()
          {
               if (m_CurrentOptionIndex >= 0 && m_CurrentOptionIndex < r_Options.Count)
               {
                    if (CurrentOption.IsFocused)
                    {
                         CurrentOption.UnFocus();
                    }
               }
          }

          public void AddMenuItem(string i_Text, Action<MenuItem> i_CheckMouseOrKBState = null, Menu i_LinkedMenu = null)
          {
               MenuItem actionItem = new MenuItem(i_Text, this.GameScreen, i_LinkedMenu, i_CheckMouseOrKBState);
               AddMenuItem(actionItem);
          }

          public void AddMenuItem(StrokeSpriteFont i_Text, Action<MenuItem> i_CheckMouseOrKBState = null, Menu i_LinkedMenu = null)
          {
               MenuItem actionItem = new MenuItem(i_Text, this.GameScreen, i_LinkedMenu, i_CheckMouseOrKBState);
               AddMenuItem(actionItem);
          }

          public void AddMenuItem(MenuItem i_Item)
          {
               r_Options.Add(i_Item);
               this.GameScreen.Add(i_Item);

               setPosition(i_Item);

               if (!i_Item.IsInitialized)
               {
                    i_Item.Initialize();
               }
          }

          private void setPosition(MenuItem i_Item)
          {
               if (NextPosition == Vector2.Zero)
               {
                    NextPosition = this.Position;
               }

               i_Item.Position = NextPosition;
               NextPosition += new Vector2(0, k_Spacing);
          }

          public MenuItem CurrentOption
          {
               get
               {
                    MenuItem current = null;

                    if(CurrentOptionIndex >= 0)
                    {
                         current = r_Options[CurrentOptionIndex];
                    }

                    return current;
               }
          }

          public void Next()
          {
               CurrentOptionIndex++;
          }

          public void Back()
          {
               CurrentOptionIndex--;
          }

          protected void HideItems()
          {
               foreach(MenuItem menuItem in r_Options)
               {
                    menuItem.Visible = false;
               }
          }

          protected void ShowItems()
          {
               foreach (MenuItem menuItem in r_Options)
               {
                    menuItem.Visible = true;
               }
          }

          protected virtual void AddItems()
          {
               this.AddMenuItem("Quit", menu_Quit);
          }

          private void menu_Quit(MenuItem i_MenuItem)
          {
               if (r_InputManager.KeyPressed(Keys.Enter)
                    || r_InputManager.ButtonPressed(eInputButtons.Left))
               {
                    this.Game.Exit();
               }
          }

          public override void Initialize()
          {
               AddItems();
               base.Initialize();

               if(!this.Title.IsInitialized)
               {
                    this.Title.Initialize();
               }

               this.Title.PositionOrigin = this.Title.SourceRectangleCenter;
               this.Title.Position = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2, k_TitleHeight);
          }

          public override void Update(GameTime i_GameTime)
          {
               checkKeyboardHover();
               checkMouseHover();

               base.Update(i_GameTime);
          }

          private void checkKeyboardHover()
          {
               if (r_InputManager.KeyPressed(Keys.Down))
               {
                    Next();
               }
               else if (r_InputManager.KeyPressed(Keys.Up))
               {
                    Back();
               }
          }

          private void checkMouseHover()
          {
               Point mousePosition = r_InputManager.MouseState.Position;

               if (r_InputManager.MousePositionDelta != Vector2.Zero)
               {
                    for (int option = 0; option < r_Options.Count; option++)
                    {
                         if (r_Options[option].StrokeSpriteFont.Bounds.Contains(mousePosition))
                         {
                              if (!r_Options[option].IsFocused)
                              {
                                   CurrentOptionIndex = option;
                              }
                         }
                         else
                         {
                              if (r_Options[option].IsFocused)
                              {
                                   r_Options[option].UnFocus();
                                   m_CurrentOptionIndex = -1;
                              }
                         }
                    }
               }
          }
     }
}

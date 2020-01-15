﻿using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Components;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     public class ScreensMananger : CompositeDrawableComponent<GameScreen>, IScreensMananger
     {
          public ScreensMananger(Game i_Game) : base(i_Game)
          {
               i_Game.Components.Add(this);
          }

          private Stack<GameScreen> m_ScreensStack = new Stack<GameScreen>();

          public GameScreen ActiveScreen
          {
               get 
               { 
                    return m_ScreensStack.Count > 0 ? m_ScreensStack.Peek() : null; 
               }
          }

          public void SetCurrentScreen(GameScreen i_GameScreen)
          {
               Push(i_GameScreen);

               i_GameScreen.Activate();
          }

          public void Push(GameScreen i_GameScreen)
          {
               // hello new screen, I am your manager, nice to meet you:
               i_GameScreen.ScreensManager = this;

               if (!this.Contains(i_GameScreen))
               {
                    this.Add(i_GameScreen);

                    // let me know when you are closed, so i can pop you from the stack:
                    i_GameScreen.Closed += screen_Closed;
               }

               if (ActiveScreen != i_GameScreen)
               {
                    if (ActiveScreen != null)
                    {
                         // connect each new screen to the previous one:
                         i_GameScreen.PreviousScreen = ActiveScreen;

                         ActiveScreen.Deactivate();
                    }

                    m_ScreensStack.Push(i_GameScreen);
               }

               i_GameScreen.DrawOrder = m_ScreensStack.Count;
          }

          private void screen_Closed(object i_Sender, EventArgs i_Args)
          {
               Pop(i_Sender as GameScreen);
               Remove(i_Sender as GameScreen);
          }

          private void Pop(GameScreen i_GameScreen)
          {
               m_ScreensStack.Pop();

               if (m_ScreensStack.Count > 0)
               {
                    // when one is popped, the previous becomes the active one
                    ActiveScreen.Activate();
               }
          }

          private new bool Remove(GameScreen i_Screen)
          {
               return base.Remove(i_Screen);
          }

          private new void Add(GameScreen i_Component)
          {
               base.Add(i_Component);
          }

          protected override void OnComponentRemoved(GameComponentEventArgs<GameScreen> i_Args)
          {
               base.OnComponentRemoved(i_Args);

               i_Args.GameComponent.Closed -= screen_Closed;

               if (m_ScreensStack.Count == 0)
               {
                    Game.Exit();
               }
          }

          public override void Initialize()
          {
               Game.Services.AddService(typeof(IScreensMananger), this);

               base.Initialize();
          }
     }

}

using System;
using System.Collections.Generic;
using A20_Ex03_Daniel_203105572_Dor_206318537.Components;
using A20_Ex03_Daniel_203105572_Dor_206318537.Enums;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class ScreensMananger : CompositeDrawableComponent<GameScreen>, IScreensMananger
     {
          public event EventHandler<StateChangedEventArgs> ScreenStateChanged;
          private Stack<GameScreen> m_ScreensStack = new Stack<GameScreen>();

          public ScreensMananger(Game i_Game)
              : base(i_Game)
          {
               i_Game.Components.Add(this);
          }

          public GameScreen ActiveScreen
          {
               get { return m_ScreensStack.Count > 0 ? m_ScreensStack.Peek() : null; }
          }

          public void SetCurrentScreen(GameScreen i_GameScreen)
          {
               Push(i_GameScreen);

               i_GameScreen.Activate();
          }

          public void Push(GameScreen i_GameScreen)
          {
               i_GameScreen.ScreensManager = this;

               if (!this.Contains(i_GameScreen))
               {
                    this.Add(i_GameScreen);
                    i_GameScreen.StateChanged += screen_StateChanged;
               }

               if (ActiveScreen != i_GameScreen)
               {
                    if (ActiveScreen != null)
                    {
                         i_GameScreen.PreviousScreen = ActiveScreen;
                         ActiveScreen.Deactivate();
                    }
               }

               if (ActiveScreen != i_GameScreen)
               {
                    m_ScreensStack.Push(i_GameScreen);
               }

               i_GameScreen.DrawOrder = m_ScreensStack.Count;
          }

          private void screen_StateChanged(object i_Sender, StateChangedEventArgs i_Args)
          {
               switch (i_Args.CurrentState)
               {
                    case eScreenState.Activating:
                         break;
                    case eScreenState.Active:
                         break;
                    case eScreenState.Deactivating:
                         break;
                    case eScreenState.Closing:
                         pop(i_Sender as GameScreen);
                         break;
                    case eScreenState.Inactive:
                         break;
                    case eScreenState.Closed:
                         Remove(i_Sender as GameScreen);
                         break;
                    default:
                         break;
               }

               OnScreenStateChanged(i_Sender, i_Args);
          }

          private void pop(GameScreen i_GameScreen)
          {
               m_ScreensStack.Pop();

               if (m_ScreensStack.Count > 0)
               {
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

          protected virtual void OnScreenStateChanged(object i_Sender, StateChangedEventArgs i_Args)
          {
               if (ScreenStateChanged != null)
               {
                    ScreenStateChanged(i_Sender, i_Args);
               }
          }

          protected override void OnComponentRemoved(GameComponentEventArgs<GameScreen> i_Args)
          {
               base.OnComponentRemoved(i_Args);

               i_Args.GameComponent.StateChanged -= screen_StateChanged;

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

using System;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Components
{
     /// <summary>
     /// Arguments used with events from the GameComponentCollection.
     /// </summary>
     /// <typeparam name="ComponentType"></typeparam>
     public class GameComponentEventArgs<ComponentType> : EventArgs
         where ComponentType : IGameComponent
     {
          private ComponentType m_Component;

          public GameComponentEventArgs(ComponentType gameComponent)
          {
               m_Component = gameComponent;
          }

          public ComponentType GameComponent
          {
               get { return m_Component; }
          }
     }
}

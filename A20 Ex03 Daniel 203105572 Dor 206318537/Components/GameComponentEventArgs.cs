using System;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Components
{
     public class GameComponentEventArgs<ComponentType> : EventArgs
         where ComponentType : IGameComponent
     {

          public GameComponentEventArgs(ComponentType i_GameComponent)
          {
               GameComponent = i_GameComponent;
          }

          public ComponentType GameComponent { get; private set; }
     }
}

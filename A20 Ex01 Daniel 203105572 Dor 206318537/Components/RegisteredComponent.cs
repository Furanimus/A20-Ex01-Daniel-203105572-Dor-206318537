using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Components
{
     public class RegisteredComponent : GameComponent
     {
          public RegisteredComponent(Game i_Game, int i_UpdateOrder)
               : base(i_Game)
          {
               this.UpdateOrder = i_UpdateOrder;
               Game.Components.Add(this); // self-register as a coponent
          }

          public RegisteredComponent(Game i_Game)
               : this(i_Game, int.MaxValue)
          { 
          }
     }
}

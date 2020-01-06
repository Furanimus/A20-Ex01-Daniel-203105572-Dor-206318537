using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class BaseGame : Game
     {
          protected IInputManager m_InputManager;
          protected ICollisionsManager m_CollisionManager;

          protected BaseGame()
          {
               m_InputManager = new InputManager(this);
               m_CollisionManager = new CollisionsManager(this);

          }

          public GameTime GameTime { get; set; }

          protected override void Update(GameTime i_GameTime)
          {
               GameTime = i_GameTime;

               base.Update(i_GameTime);
          }
     }
}

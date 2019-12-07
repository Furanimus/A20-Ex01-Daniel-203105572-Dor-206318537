using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{

     public class BaseGame : Game
     {
          public static GameTime GameTime { get; set; }

          protected override void Update(GameTime i_GameTime)
          {
               GameTime = BaseGame.GameTime;
               base.Update(i_GameTime);
          }
     }

}

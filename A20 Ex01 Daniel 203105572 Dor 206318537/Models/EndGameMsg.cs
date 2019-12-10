using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EndGameMsg
     {
          private const string k_Message = "Your score is: ";
          private const string k_Message2 = "Press enter to continue";
          private readonly int r_FinalScore;
          private readonly GameEnvironment r_GameEnvironment;

          public Vector2 Position { get; set; }

          public EndGameMsg(int i_Score, GameEnvironment i_GameEnvironment)
          {
               r_FinalScore = i_Score;
               r_GameEnvironment = i_GameEnvironment;

               Position = new Vector2(i_GameEnvironment.WindowHeight / 2, i_GameEnvironment.WindowWidth / 2);
          }
     }
}

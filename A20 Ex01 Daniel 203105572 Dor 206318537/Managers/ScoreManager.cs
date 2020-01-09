using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     public class ScoreManager : DrawableGameComponent
     {
          private readonly LinkedList<BasePlayer> r_Players = new LinkedList<BasePlayer>();

          public ScoreManager(BasePlayer i_Player, Game i_Game) : base(i_Game)
          {
          }

          public void AddPlayer(BasePlayer i_Player)
          {
               r_Players.AddLast(i_Player);
          }

          public void RemovePlayer(BasePlayer i_Player)
          {
               r_Players.Remove(i_Player);
          }

          public override void Initialize()
          {
               base.Initialize();
          }

          public override void Draw(GameTime i_GameTime)
          {

               base.Draw(i_GameTime);
          }
     }
}

using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Barrier : Sprite, ICollidable
     {
          private const string k_AssetName = @"Sprites\Barrier_44x32";

          public object GroupRepresentative { get; set; }

          public Barrier(Game i_Game)
               : base(k_AssetName, i_Game)
          {
               this.Velocity = new Vector2(45, 0);
          }

          public override void Update(GameTime i_GameTime)
          {
               base.Update(i_GameTime);


          }

          public override void Draw(GameTime i_GameTime)
          {
               base.Draw(i_GameTime);

          }
     }
}

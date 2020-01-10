using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Barrier : Sprite, ICollidable2D
     {
          private const string k_AssetName = @"Sprites\Barrier_44x32";
          private const int k_DefaultWidth = 44;
          private const int k_DefaultHeight = 32;

          public object GroupRepresentative { get; set; }

          public Barrier(Game i_Game)
               : base(k_AssetName, i_Game)
          {
               this.Width = k_DefaultWidth;
               this.Height = k_DefaultHeight;
               this.Velocity = new Vector2(0, 0);
          }

          public override void Update(GameTime i_GameTime)
          {

               base.Update(i_GameTime);
          }

          public override void Collided(ICollidable i_Collidable)
          {
          }
     }
}

using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Barrier : Sprite, ICollidable2D
     {
          private const string k_AssetName = @"Sprites\Barrier_44x32";
          private const int k_DefaultWidth = 44;
          private const int k_DefaultHeight = 32;
          private const int k_CallOrder = 6;
          private const float k_XVelocity = 0;
          private const float k_YVelocity = 0;

          public object GroupRepresentative { get; set; }

          public Barrier(Game i_Game)
               : base(k_AssetName, i_Game, k_CallOrder)
          {
               this.Width = k_DefaultWidth;
               this.Height = k_DefaultHeight;
               this.Velocity = new Vector2(k_XVelocity, k_YVelocity);
          }

          public override void Collided(ICollidable i_Collidable)
          {
          }
     }
}

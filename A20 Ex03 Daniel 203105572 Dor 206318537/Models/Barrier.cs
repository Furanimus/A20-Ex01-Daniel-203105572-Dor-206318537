using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models
{
     public class Barrier : Sprite, ICollidable2D
     {
          private const string k_AssetName = @"Sprites\Barrier_44x32";
          private const int k_DefaultWidth = 44;
          private const int k_DefaultHeight = 32;
          private const float k_XVelocity = 45;
          private const float k_YVelocity = 0;

          public object GroupRepresentative { get; set; }

          public Barrier(GameScreen i_GameScreen)
               : base(k_AssetName, i_GameScreen)
          {
               this.Width = k_DefaultWidth;
               this.Height = k_DefaultHeight;
               this.Velocity = new Vector2(k_XVelocity, k_YVelocity);
               this.GameScreen.Add(this);
          }

          public override void Collided(ICollidable i_Collidable)
          {
          }
     }
}

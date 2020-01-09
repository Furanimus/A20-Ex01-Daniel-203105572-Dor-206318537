using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Bullet : BaseBullet
     {
          private const string k_GraphicPath = @"Sprites\Bullet";
          private const int k_RandomFactor = 1;
          private const int k_MaxRandom = 5;
          private const int k_MinRandom = 0;

          private readonly IRandomBehavior r_RandomBehavior;
          public Bullet(Game i_Game) : this(Color.Red, i_Game)
          {
               r_RandomBehavior = this.Game.Services.GetService(typeof(IRandomBehavior)) as IRandomBehavior;
          }

          public Bullet(Color i_TintColor, Game i_Game) : base(k_GraphicPath, i_Game)
          {
               this.Width = 6;
               this.Height = 16;
               this.Enabled = false;
               this.Visible = false;
               this.TintColor = i_TintColor;
               this.Velocity = new Vector2(0, 160);
          }

          public override void Collided(ICollidable i_Collidable)
          {

               if (i_Collidable is Bullet && i_Collidable.GroupRepresentative is BasePlayer && this.GroupRepresentative is EnemyManager)
               {
                    if(r_RandomBehavior.Roll(k_RandomFactor, k_MinRandom, k_MaxRandom))
                    {
                         base.Collided(i_Collidable);
                    }
               }
               else
               {
                    base.Collided(i_Collidable);
               }
          }
     }
}
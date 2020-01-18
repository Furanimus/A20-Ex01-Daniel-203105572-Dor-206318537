using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public abstract class Enemy : Entity, ICollidable2D
     {
          private const int k_CallOrder = 5;

          public Enemy(string i_AssetName, GameScreen i_GameScreen) 
               : base(i_AssetName, i_GameScreen, k_CallOrder)
          {
               this.Velocity = new Vector2(32, 0);
               this.Lives = 1;
               this.ViewDirection = Sprite.Down;
               this.GroupRepresentative = this;
          }

          public int Score { get; set; }

          public object GroupRepresentative { get; set; }
     }
}
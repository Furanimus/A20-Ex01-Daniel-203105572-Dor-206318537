using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.BaseModels;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public abstract class ShooterPlayer : BasePlayer
     {
          private const int k_MaxShotInMidAir = 2;

          public ShooterPlayer(string i_AssetName, GameScreen i_GameScreen) 
               : this(i_AssetName, i_GameScreen, int.MaxValue)
          {
          }

          public ShooterPlayer(string i_AssetName, GameScreen i_GameScreen, int i_CallsOrder) 
               : this(i_AssetName, i_GameScreen, int.MaxValue, int.MaxValue)
          {
          }

          public ShooterPlayer(string i_AssetName, GameScreen i_GameScreen, int i_UpdateOrder, int i_DrawOrder) 
               : base(i_AssetName, i_GameScreen, i_UpdateOrder, i_DrawOrder)
          {
               this.Gun = new Gun(k_MaxShotInMidAir, this, Bullet_Collided);
          }

          public BaseGun Gun { get; set; }

          public Keys ShootKey { get; set; } = Keys.U;

          public eInputButtons MouseShootButton { get; set; } = eInputButtons.Left;

          public override void Update(GameTime i_GameTime)
          {
               if (IsAlive)
               {
                    if (r_InputManager.KeyPressed(ShootKey) ||
                              (IsMouseControllable && r_InputManager.ButtonPressed(MouseShootButton)))
                    {
                         Gun.Shoot();
                    }
               }

               base.Update(i_GameTime);
          }

          protected virtual void Bullet_Collided(ICollidable i_Collidable)
          {
               Enemy enemy = i_Collidable as Enemy;

               if(enemy != null)
               {
                    this.Score += enemy.Score;
               }
          }
     }
}

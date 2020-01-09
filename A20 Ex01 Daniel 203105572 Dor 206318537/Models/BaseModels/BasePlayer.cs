using A20_Ex01_Daniel_203105572_Dor_206318537.Enums;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels
{
     public abstract class BasePlayer : Entity
     {
          protected readonly IInputManager r_InputManager;
          private readonly Vector2 r_Velocity;

          public BasePlayer(string i_AssetName, Game i_Game) : this(i_AssetName, i_Game, int.MaxValue)
          {
          }

          public BasePlayer(string i_AssetName, Game i_Game, int i_CallsOrder) : this(i_AssetName, i_Game, int.MaxValue, int.MaxValue)
          {
          }

          public BasePlayer(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder) : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
          {
               r_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
               r_Velocity = new Vector2(145, 0);
          }

          public int Score { get; set; }

          public bool IsMouseControllable { get; set; }

          public Keys MoveLeftKey { get; set; } = Keys.H;

          public Keys MoveRightKey { get; set; } = Keys.K;

          public override Vector2 Position
          {
               get
               {
                    return base.Position;
               }

               set
               {
                    m_Position.X = MathHelper.Clamp(value.X, 0, (float)this.GraphicsDevice.Viewport.Width - Width);
               }
          }

          public override void Update(GameTime i_GameTime)
          {
               if (IsAlive)
               {
                    if (r_InputManager.KeyboardState.IsKeyDown(MoveLeftKey))
                    {
                         Velocity = r_Velocity * Sprite.Left;
                    }
                    else if (r_InputManager.KeyboardState.IsKeyDown(MoveRightKey))
                    {
                         Velocity = r_Velocity * Sprite.Right;
                    }
                    else
                    {
                         Velocity = Vector2.Zero;
                    }

                    if (IsMouseControllable)
                    {
                         Vector2 mouseDelta = r_InputManager.MousePositionDelta;

                         if (mouseDelta != Vector2.Zero)
                         {
                              Position += mouseDelta;
                         }
                    }
               }
               else
               {
                    Velocity = Vector2.Zero;
               }

               base.Update(i_GameTime);
          }

          public override void Collided(ICollidable i_Collidable)
          {
               if (i_Collidable != null)
               {
                    BaseBullet bullet  = i_Collidable as BaseBullet;
                    Enemy enemy = i_Collidable as Enemy;

                    if (bullet != null && bullet.Visible)
                    {
                         OnCollidedWithBullet();
                    }
                    else if (enemy != null && enemy.Visible)
                    {
                         OnCollidedWithEnemy();
                    }
               }
          }

          protected virtual void OnCollidedWithBullet()
          {
          }

          protected virtual void OnCollidedWithEnemy()
          {
          }
     }
}
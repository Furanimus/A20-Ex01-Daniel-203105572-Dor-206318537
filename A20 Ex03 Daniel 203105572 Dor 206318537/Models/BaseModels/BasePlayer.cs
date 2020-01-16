using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex01_Daniel_203105572_Dor_206318537.Screens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens.BaseModels
{
     public abstract class BasePlayer : Entity
     {
          protected readonly IInputManager r_InputManager;
          private readonly Vector2 r_Velocity;

          public event Action CollidedWithEnemy;

          public BasePlayer(string i_AssetName, GameScreen i_GameScreen) 
               : this(i_AssetName, i_GameScreen, int.MaxValue)
          {
          }

          public BasePlayer(string i_AssetName, GameScreen i_GameScreen, int i_CallsOrder) 
               : this(i_AssetName, i_GameScreen, int.MaxValue, int.MaxValue)
          {
          }

          public BasePlayer(string i_AssetName, GameScreen i_GameScreen, int i_UpdateOrder, int i_DrawOrder) 
               : base(i_AssetName, i_GameScreen, i_UpdateOrder, i_DrawOrder)
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
                    if (m_Position != value)
                    {
                         m_Position.X = MathHelper.Clamp(value.X, 0, (float)this.GraphicsDevice.Viewport.Width - Width);
                         OnPositionChanged();
                    }
               }
          }

          public override void Initialize()
          {
               IScoreManager scoreManager = this.Game.Services.GetService(typeof(IScoreManager)) as IScoreManager;
               ILivesManager livesManager = this.Game.Services.GetService(typeof(ILivesManager)) as ILivesManager;

               if(scoreManager == null)
               {
                    scoreManager = new ScoreManager(this.GameScreen);
                    scoreManager.AddPlayer(this, Color.White);
               }
               else
               {
                    if (!scoreManager.IsPlayerAlreadyAdded(this))
                    {
                         scoreManager.AddPlayer(this, Color.White);
                    }
               }

               if (livesManager == null)
               {
                    livesManager = new LivesManager(this.GameScreen);
                    livesManager.AddPlayer(this);
               }
               else
               {
                    if (!livesManager.IsPlayerAlreadyAdded(this))
                    {
                         livesManager.AddPlayer(this);
                    }
               }

               base.Initialize();
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
               BaseBullet bullet  = i_Collidable as BaseBullet;
               Enemy enemy = i_Collidable as Enemy;

               if (bullet != null)
               {
                    OnCollidedWithBullet(bullet);
               }
               else if (enemy != null)
               {
                    OnCollidedWithEnemy(enemy);
               }
          }

          protected virtual void OnCollidedWithBullet(BaseBullet i_Bullet)
          {
          }

          protected virtual void OnCollidedWithEnemy(Enemy i_Enemy)
          {
               if (CollidedWithEnemy != null)
               {
                    CollidedWithEnemy.Invoke();
               }
          }
     }
}
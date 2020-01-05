using System;
using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Enemy : Entity, ICollidable2D
     {
          public event Action<Enemy> Destroyed;

          private bool m_IsAlive;
          protected RandomBehavior m_RandomBehavior;

          public Enemy(string i_AssetName, Game i_Game) 
               : base(i_AssetName, i_Game)
          {
               m_IsAlive = true;
               Velocity = new Vector2(50,0);
               Height = 32;
               Width = 32;
               Lives = 1;
               m_RandomBehavior = new RandomBehavior(i_Game);
          }

          public new bool IsAlive
          {
               get
               {
                    return m_IsAlive;
               }

               set
               {
                    m_IsAlive = value;

                    if (value == false && Destroyed != null)
                    {
                         Destroyed.Invoke(this);
                    }
               }
          }

          public int Score { get; set; }

          public override void Update(GameTime i_GameTime)
          {
               CollisionsManager collisionManager = this.Game.Services.GetService(typeof(CollisionsManager)) as CollisionsManager;

               if ((!collisionManager.IsCollideWithWindowRightEdge(this) && MoveDirection == Sprite.Right) ||
                    (!collisionManager.IsCollideWithWindowLeftEdge(this) && MoveDirection == Sprite.Left) ||
                    (!collisionManager.IsCollideWithWindowBottomEdge(this) && MoveDirection == Sprite.Down))
               {
                    if (MoveDirection == Sprite.Down)
                    {
                         Position += MoveDirection * (Height / 2);
                    }
                    else
                    {
                         Position += MoveDirection * Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                    }
               }
          }
     }
}

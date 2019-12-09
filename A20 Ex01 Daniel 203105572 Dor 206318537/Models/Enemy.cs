using System;
using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Enemy : Entity
     {
          public event Action<Enemy> Destroyed;

          private bool m_IsAlive;
          protected RandomBehavior m_RandomBehavior;

          public Enemy(string i_GraphicsPath, Game i_Game) 
               : base(i_GraphicsPath, i_Game)
          {
               m_IsAlive = true;
               Velocity = 50;
               Height = 32;
               Width = 32;
               Lives = 1;
               m_RandomBehavior = new RandomBehavior();
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
               if((!CollisionDetector.IsCollideWithRightEdge(this) && Direction == Sprite.Right) ||
                    (!CollisionDetector.IsCollideWithLeftEdge(this) && Direction == Sprite.Left) ||
                    (!CollisionDetector.IsCollideWithBottomEdge(this) && Direction == Sprite.Down))
               {
                    if (Direction == Sprite.Down)
                    {
                         Position += Direction * (Height / 2);
                    }
                    else
                    {
                         Position += Direction * Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                    }
               }
          }
     }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Enemy: BasicEntity
     {
          public static eDirection Direction { get; set; } = eDirection.Right;

          private Timer r_Timer;

          public Enemy()
          {
               r_Timer = new Timer();
               r_Timer.Interval = 1000;
               r_Timer.Elapsed += Move;
               r_Timer.Start();

               Velocity = 50;
               Height = 32;
               Width = 32;
               Lives = 1;
          }

          public int Score { get; set; }

          public virtual void Move(object sender, ElapsedEventArgs e)
          {
               if (Direction == eDirection.Right)
               {
                    m_Position.X += Velocity;
               }
               else if (Direction == eDirection.Left)
               {
                    m_Position.X -= Velocity;
               }
          }

          public static void HandleCollision(Enemy[,] i_Enemies, int i_Rows, int i_Cols)
          {
               if (Direction == eDirection.Right)
               {
                    Direction = eDirection.Left;
               }
               else if (Direction == eDirection.Left)
               {
                    Direction = eDirection.Right;
               }
               //ChangeDirection
               //Update Matrix
               //SpeedUp
               for (int row = 0; row < i_Rows; row++)
               {
                    for (int col = 0; col < i_Cols; col++)
                    {
                         float x = i_Enemies[row, col].Position.X;
                         float y = i_Enemies[row, col].Position.Y;

                         i_Enemies[row, col].Position = new Vector2(x, y + i_Enemies[row, col].Height / 2);
                    }
               }
          }
     }
}

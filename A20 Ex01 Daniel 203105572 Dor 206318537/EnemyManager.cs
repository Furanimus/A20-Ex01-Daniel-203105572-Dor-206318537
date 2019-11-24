using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyManager
     {
          private static GameEnvironment s_GameEnvironment = new GameEnvironment();
          private static EntityFactory s_EntityFactory = new EntityFactory(s_GameEnvironment);
          private readonly ContentManager r_ContentManager;
          private readonly Timer r_Timer;

          public EnemyManager(ContentManager i_ContentManager, int i_EnemyMatrixRows, int i_EnemyMatrixCols)
          {
               r_ContentManager = i_ContentManager;
               EnemyMatrixRows = i_EnemyMatrixRows;
               EnemyMatrixCols = i_EnemyMatrixCols;
               EnemiesMatrix = new Enemy[i_EnemyMatrixRows, i_EnemyMatrixCols];

               loadContent();

               r_Timer = new Timer();
               r_Timer.Interval = 1000;
               r_Timer.Elapsed += MoveAll;
               r_Timer.Start();
          }

          public Enemy[,] EnemiesMatrix { set; get; }

          public int EnemyMatrixRows { get; set; }

          public int EnemyMatrixCols { get; set; }

          public Enemy MotherShip { set; get; } = s_EntityFactory.Create(typeof(MotherShip)) as Enemy;

          public int NumOfPinkEnemiesRows { get; set; } = 1;

          public int NumOfLightBlueEnemiesRows { get; set; } = 2;

          public int NumOfYellowEnemiesRows { get; set; } = 2;

          public float EnemiesOffset { get; set; } = 0.6f;

          private void MoveAll(object sender, ElapsedEventArgs e)
          {
               MoveMatrix();
               MoveMotherShip();
          }

          private void MoveMatrix()
          {
               handleCollision();

               for (int row = 0; row < EnemyMatrixRows; row++)
               {
                    for (int col = 0; col < EnemyMatrixCols; col++)
                    {
                         EnemiesMatrix[row, col].Move();
                    }
               }
          }

          private void handleCollision()
          {
               Enemy mostRightEnemy = findMostCornerEnemy((currentCol, mostRightCol) => currentCol > mostRightCol,
                         mostRightCol => mostRightCol == EnemyMatrixCols - 1);
               Enemy mostLeftEnemy = findMostCornerEnemy((currentCol, mostLeftCol) => currentCol <= mostLeftCol,
                    mostLeftCol => mostLeftCol == 0);

               if (Enemy.Direction == eDirection.Right || Enemy.Direction == eDirection.Left)
               {
                    if (isCollideWithLeftOrRightSide(mostLeftEnemy, mostRightEnemy))
                    {
                         Enemy.Direction = eDirection.Down;
                    }
               }
               else
               {
                    fixDirection(mostLeftEnemy, mostRightEnemy);
               }
          }

          private void fixDirection(Enemy i_MostLeftEnemy, Enemy i_MostRightEnemy)
          {
               if (i_MostRightEnemy.Position.X >= s_GameEnvironment.WindowWidth - i_MostRightEnemy.Width)
               {
                    Enemy.Direction = eDirection.Left;
               }
               else if (i_MostLeftEnemy.Position.X <= 0)
               {
                    Enemy.Direction = eDirection.Right;
               }
          }

          private bool isCollideWithLeftOrRightSide(Enemy i_MostLeftEnemy, Enemy i_MostRightEnemy)
          {
               return i_MostRightEnemy.Position.X >= s_GameEnvironment.WindowWidth - i_MostRightEnemy.Width && Enemy.Direction == eDirection.Right
                         || i_MostLeftEnemy.Position.X <= 0 && Enemy.Direction == eDirection.Left;
          }

          private Enemy findMostCornerEnemy(Func<int, int, bool> i_IsMostCornerFunc, Func<int, bool> i_WhenToStopSearchFunc)
          {
               Enemy mostCornerEnemy = null;
               int mostCornerCol = 0;
               bool isFound = false;

               for (int row = 0; row < EnemyMatrixRows && !isFound; row++)
               {
                    for (int col = 0; col < EnemyMatrixCols && !isFound; col++)
                    {
                         Enemy current = EnemiesMatrix[row, col];

                         if(current.IsAlive && i_IsMostCornerFunc.Invoke(col, mostCornerCol))
                         {
                              mostCornerEnemy = current;
                              mostCornerCol = col;

                              if(i_WhenToStopSearchFunc.Invoke(mostCornerCol))
                              {
                                   isFound = true;
                              }
                         }
                    }
               }

               return mostCornerEnemy;
          }

          private void MoveMotherShip()
          {

          }

          private void loadContent()
          {
               float enemyY = 96;
               float enemyX = 0;

               for (int row = 0; row < EnemyMatrixRows; row++)
               {
                    for (int col = 0; col < EnemyMatrixCols; col++)
                    {
                         if (row < NumOfPinkEnemiesRows)
                         {
                              EnemiesMatrix[row, col] = s_EntityFactory.Create(typeof(EnemyPink)) as Enemy;
                         }
                         else if (row < NumOfPinkEnemiesRows + NumOfLightBlueEnemiesRows)
                         {
                              EnemiesMatrix[row, col] = s_EntityFactory.Create(typeof(EnemyLightBlue)) as Enemy;
                         }
                         else
                         {
                              EnemiesMatrix[row, col] = s_EntityFactory.Create(typeof(EnemyYellow)) as Enemy;
                         }

                         EnemiesMatrix[row, col].Graphics = r_ContentManager.Load<Texture2D>(EnemiesMatrix[row, col].GraphicsPath);
                         EnemiesMatrix[row, col].Position = new Vector2(enemyX, enemyY);
                         enemyX += EnemiesMatrix[row, col].Width + EnemiesMatrix[row, col].Width * EnemiesOffset;
                    }

                    enemyY += EnemiesMatrix[row, 0].Height + EnemiesMatrix[row, 0].Height * EnemiesOffset;
                    enemyX = 0;
               }
          }

          public void Draw(SpriteBatch i_SpriteBatch)
          {
               for (int row = 0; row < EnemyMatrixRows; row++)
               {
                    for (int col = 0; col < EnemyMatrixCols; col++)
                    {
                         i_SpriteBatch.Begin();

                         if (row < NumOfPinkEnemiesRows)
                         {
                              i_SpriteBatch.Draw(EnemiesMatrix[row, col].Graphics, EnemiesMatrix[row, col].Position, Color.Pink);
                         }
                         else if (row < NumOfPinkEnemiesRows + NumOfLightBlueEnemiesRows)
                         {
                              i_SpriteBatch.Draw(EnemiesMatrix[row, col].Graphics, EnemiesMatrix[row, col].Position, Color.LightBlue);
                         }
                         else
                         {
                              i_SpriteBatch.Draw(EnemiesMatrix[row, col].Graphics, EnemiesMatrix[row, col].Position, Color.LightYellow);
                         }

                         i_SpriteBatch.End();
                    }
               }
          }
     }
}

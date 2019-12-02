﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyManager
     {
          private static Vector2 s_MatrixDirection = Sprite.Right;
          private static GameEnvironment s_GameEnvironment = new GameEnvironment();
          private static SpriteFactory s_EntityFactory = Singelton<SpriteFactory>.Instance;
          private readonly ContentManager r_ContentManager;
          private readonly int r_EnemyCount;
          private const int k_NumOfDeadEnemiesToIncreaseVelocity = 5;
          private const float k_PercentageToIncreaseVelocityOnRowDescend = 0.05f;
          private const float k_PercentageToIncreaseVelocityOnNumOfDeadEnemies = 0.03f;

          private RandomBehavior m_RandomBehavior = new RandomBehavior();

          public EnemyManager(ContentManager i_ContentManager, int i_EnemyMatrixRows, int i_EnemyMatrixCols)
          {
               r_ContentManager = i_ContentManager;
               EnemyMatrixRows = i_EnemyMatrixRows;
               EnemyMatrixCols = i_EnemyMatrixCols;
               r_EnemyCount = i_EnemyMatrixCols * i_EnemyMatrixRows;
               EnemiesMatrix = new Enemy[i_EnemyMatrixRows, i_EnemyMatrixCols];

               loadContent();
          }

          public Enemy[,] EnemiesMatrix { get; set; }

          public int EnemyMatrixRows { get; set; }

          public int EnemyMatrixCols { get; set; }

          public int EnemiesDied { get; set; }

          public IMotherShip MotherShip { get; set; } = s_EntityFactory.Create(typeof(MotherShip)) as IMotherShip;

          public int NumOfPinkEnemiesRows { get; set; } = 1;

          public int NumOfLightBlueEnemiesRows { get; set; } = 2;

          public int NumOfYellowEnemiesRows { get; set; } = 2;

          public float EnemiesOffset { get; set; } = 0.6f;

          public void MoveMatrix(GameTime i_GameTime)
          {
               handleCollision();

               foreach (Enemy enemy in EnemiesMatrix)
               {
                    if(enemy.IsAlive)
                    {
                         enemy.GameTime = i_GameTime;
                         enemy.Move(s_MatrixDirection);
                    }
               }
          }

          public void HandleMotherShip(GameTime i_GameTime)
          {
               MotherShip.HandleMotherShip(i_GameTime);
          }

          private void handleCollision()
          {
               Enemy mostRightEnemy = findMostCornerEnemy((currentCol, mostRightCol) => currentCol > mostRightCol,
               mostRightCol => mostRightCol == EnemyMatrixCols - 1);
               Enemy mostLeftEnemy = findMostCornerEnemy((currentCol, mostLeftCol) => currentCol <= mostLeftCol,
               mostLeftCol => mostLeftCol == 0);

               if (s_MatrixDirection == Sprite.Right || s_MatrixDirection == Sprite.Left)
               {
                    if (isCollideWithLeftOrRightSide(mostLeftEnemy, mostRightEnemy))
                    {
                         s_MatrixDirection = Sprite.Down;
                         RaiseAllEnemiesSpeed(k_PercentageToIncreaseVelocityOnRowDescend);
                    }
               }
               else
               {
                    fixDirection(mostLeftEnemy, mostRightEnemy);
               }
          }

          public void RaiseAllEnemiesSpeed(float i_Precentage)
          {
               for (int row = 0; row < EnemyMatrixRows; row++)
               {
                    for (int col = 0; col < EnemyMatrixCols; col++)
                    {
                         EnemiesMatrix[row, col].Velocity += EnemiesMatrix[row, col].Velocity * i_Precentage;
                    }
               }
          }

          private void fixDirection(Enemy i_MostLeftEnemy, Enemy i_MostRightEnemy)
          {
               if (i_MostRightEnemy.Position.X + i_MostRightEnemy.Width >= s_GameEnvironment.WindowWidth)
               {
                    s_MatrixDirection = Sprite.Left;
               }
               else if (i_MostLeftEnemy.Position.X <= 0)
               {
                    s_MatrixDirection = Sprite.Right;
               }
          }

          private bool isCollideWithLeftOrRightSide(Enemy i_MostLeftEnemy, Enemy i_MostRightEnemy)
          {
               return (i_MostRightEnemy.Position.X + i_MostRightEnemy.Width >= s_GameEnvironment.WindowWidth && s_MatrixDirection == Sprite.Right)
                    || (i_MostLeftEnemy.Position.X <= 0 && s_MatrixDirection == Sprite.Left);
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

                         if (current.IsAlive && i_IsMostCornerFunc.Invoke(col, mostCornerCol))
                         {
                              mostCornerEnemy = current;
                              mostCornerCol = col;

                              if (i_WhenToStopSearchFunc.Invoke(mostCornerCol))
                              {
                                   isFound = true;
                              }
                         }
                    }
               }

               return mostCornerEnemy;
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

                         Enemy enemy = EnemiesMatrix[row, col];

                         enemy.Graphics = r_ContentManager.Load<Texture2D>(enemy.GraphicsPath);
                         enemy.Position = new Vector2(enemyX, enemyY);
                         enemyX += enemy.Width + enemy.Width * EnemiesOffset;
                         enemy.Destroyed += OnDestroyed;
                    }

                    enemyY += EnemiesMatrix[row, 0].Height + EnemiesMatrix[row, 0].Height * EnemiesOffset;
                    enemyX = 0;
               }

               (MotherShip as Enemy).Graphics = r_ContentManager.Load<Texture2D>((MotherShip as Sprite).GraphicsPath);
               (MotherShip as Enemy).Destroyed += OnDestroyed;
          }

          private void OnDestroyed(Enemy i_Enemy)
          {
               EnemiesDied++;
               if (EnemiesDied % k_NumOfDeadEnemiesToIncreaseVelocity == 0)
               {
                    RaiseAllEnemiesSpeed(k_PercentageToIncreaseVelocityOnNumOfDeadEnemies);
               }
          }

          public void EnemiesTryAttack(GameTime i_GameTime)
          {
               m_RandomBehavior.DelayedAction = chooseRandomEnemyToShoot;
               m_RandomBehavior.TryInvokeDelayedAction(i_GameTime);

               foreach (ShooterEnemy shooterEnemy in EnemiesMatrix)
               {
                    shooterEnemy.GameTime = i_GameTime;
                    shooterEnemy.UpdateBulletsLocation();
               }
          }

          private void chooseRandomEnemyToShoot()
          {
               int row = m_RandomBehavior.GetRandomNumber(0, EnemyMatrixRows);
               int col = m_RandomBehavior.GetRandomNumber(0, EnemyMatrixCols);

               (EnemiesMatrix[row, col] as ShooterEnemy).Shoot(r_ContentManager);
          }

          public void Draw(SpriteBatch i_SpriteBatch)
          {

               if (MotherShip.IsOnScreen && (MotherShip as Enemy).IsAlive)
               {
                    i_SpriteBatch.Begin();
                    i_SpriteBatch.Draw((MotherShip as Sprite).Graphics, (MotherShip as Sprite).Position, Color.Red);
                    i_SpriteBatch.End();
               }

               for (int row = 0; row < EnemyMatrixRows; row++)
               {
                    for (int col = 0; col < EnemyMatrixCols; col++)
                    {
                         Enemy enemy = EnemiesMatrix[row, col];
                         if (enemy.IsAlive)
                         {
                              i_SpriteBatch.Begin();

                              if (row < NumOfPinkEnemiesRows)
                              {
                                   i_SpriteBatch.Draw(enemy.Graphics, enemy.Position, Color.Pink);
                              }
                              else if (row < NumOfPinkEnemiesRows + NumOfLightBlueEnemiesRows)
                              {
                                   i_SpriteBatch.Draw(enemy.Graphics, enemy.Position, Color.LightBlue);
                              }
                              else
                              {
                                   i_SpriteBatch.Draw(enemy.Graphics, enemy.Position, Color.LightYellow);
                              }

                              i_SpriteBatch.End();

                              i_SpriteBatch.Begin();
                              foreach (Bullet bullet in (enemy as ShooterEnemy).Bullets)
                              {
                                   i_SpriteBatch.Draw(bullet.Graphics, bullet.Position, Color.Red);
                              }
                              i_SpriteBatch.End();
                         }
                    }
               }
          }
     }
}

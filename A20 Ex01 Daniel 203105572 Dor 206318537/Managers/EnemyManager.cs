﻿//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;

//namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
//{
//     public class EnemyManager
//     {



//          public EnemyManager(Game i_Game, int i_EnemyMatrixRows, int i_EnemyMatrixCols)
//          {
//               r_Game = i_Game;
//               EnemyMatrixRows = i_EnemyMatrixRows;
//               EnemyMatrixCols = i_EnemyMatrixCols;
//               r_StartingEnemyCount = i_EnemyMatrixCols * i_EnemyMatrixRows;
//               EnemiesMatrix = new Enemy[i_EnemyMatrixRows, i_EnemyMatrixCols];

//               loadContent();
//          }

//          public bool IsEnemyCollidedWithPlayer { get; private set; }

//          public Enemy[,] EnemiesMatrix { get; set; }

//          public int StartingEnemyCount
//          {
//               get
//               {
//                    return r_StartingEnemyCount;
//               }
//          }

//          public int EnemyMatrixRows { get; set; }

//          public int EnemyMatrixCols { get; set; }

//          public int EnemiesDestroyed { get; set; }

//          public MotherShip MotherShip { get; set; } = s_EntityFactory.Create(typeof(RedMotherShip)) as MotherShip;

//          public int NumOfPinkEnemiesRows { get; set; } = 1;

//          public int NumOfLightBlueEnemiesRows { get; set; } = 2;

//          public int NumOfYellowEnemiesRows { get; set; } = 2;

//          public float EnemiesOffset { get; set; } = 0.6f;

//          public void UpdateMatrixDirection()
//          {
//               handleCollision();

//               foreach (Enemy enemy in EnemiesMatrix)
//               {
//                    if(enemy.IsAlive)
//                    {
//                         enemy.Direction = s_MatrixDirection;
//                    }
//               }
//          }

//          private void handleCollision()
//          {
//               Enemy mostRightEnemy = findMostCornerEnemy(
//                    (currentCol, mostRightCol) => currentCol > mostRightCol,
//                    mostRightCol => mostRightCol == EnemyMatrixCols - 1);
//               Enemy mostLeftEnemy = findMostCornerEnemy(
//                    (currentCol, mostLeftCol) => currentCol < mostLeftCol,
//                    mostLeftCol => mostLeftCol == 0);

//               if (s_MatrixDirection == Sprite.Right || s_MatrixDirection == Sprite.Left)
//               {
//                    if (isCollideWithLeftOrRightSide(mostLeftEnemy, mostRightEnemy))
//                    {
//                         s_MatrixDirection = Sprite.Down;
//                         IncreaseAllEnemiesVelocity(k_PercentageToIncreaseVelocityOnRowDescend);
//                    }
//               }
//               else
//               {
//                    fixDirection(mostLeftEnemy, mostRightEnemy);
//               }
//          }

//          public void IncreaseAllEnemiesVelocity(float i_Precentage)
//          {
//               for (int row = 0; row < EnemyMatrixRows; row++)
//               {
//                    for (int col = 0; col < EnemyMatrixCols; col++)
//                    {
//                         EnemiesMatrix[row, col].Velocity += EnemiesMatrix[row, col].Velocity * i_Precentage;
//                    }
//               }
//          }

//          public void DestroyPlayerIfBulletsOrEnemiesCollidedWithPlayer(BasePlayer i_Player)
//          {
//               foreach (ShooterEnemy enemy in EnemiesMatrix)
//               {
//                    foreach (Bullet bullet in enemy.Bullets)
//                    {
//                         if (CollisionDetector.IsCollide(i_Player, bullet))
//                         {
//                              i_Player.Destroyed();
//                         }
//                    }

//                    IsEnemyCollidedWithPlayer = CollisionDetector.IsCollide(enemy, i_Player);
//               }
//          }

//          private void fixDirection(Enemy i_MostLeftEnemy, Enemy i_MostRightEnemy)
//          {
//               if (CollisionDetector.IsCollideWithRightEdge(i_MostRightEnemy))
//               {
//                    s_MatrixDirection = Sprite.Left;
//               }
//               else if (CollisionDetector.IsCollideWithLeftEdge(i_MostLeftEnemy))
//               {
//                    s_MatrixDirection = Sprite.Right;
//               }
//          }

//          private bool isCollideWithLeftOrRightSide(Enemy i_MostLeftEnemy, Enemy i_MostRightEnemy)
//          {
//               bool isCollide = false;

//               if(i_MostLeftEnemy != null && i_MostRightEnemy != null)
//               {
//                    isCollide = (CollisionDetector.IsCollideWithRightEdge(i_MostRightEnemy) && s_MatrixDirection == Sprite.Right)
//                    || (CollisionDetector.IsCollideWithLeftEdge(i_MostLeftEnemy) && s_MatrixDirection == Sprite.Left);
//               }

//               return isCollide;
//          }

//          private Enemy findMostCornerEnemy(Func<int, int, bool> i_IsMostCornerFunc, Func<int, bool> i_WhenToStopSearchFunc)
//          {
//               Enemy mostCornerEnemy = null;
//               int mostCornerCol = 0;
//               bool isFound = false;

//               for (int row = 0; row < EnemyMatrixRows && !isFound; row++)
//               {
//                    for (int col = 0; col < EnemyMatrixCols && !isFound; col++)
//                    {
//                         Enemy current = EnemiesMatrix[row, col];

//                         if (current.IsAlive && (mostCornerEnemy == null || 
//                              i_IsMostCornerFunc.Invoke(col, mostCornerCol)))
//                         {
//                              mostCornerEnemy = current;
//                              mostCornerCol = col;

//                              if (i_WhenToStopSearchFunc.Invoke(mostCornerCol))
//                              {
//                                   isFound = true;
//                              }
//                         }
//                    }
//               }

//               return mostCornerEnemy;
//          }

//          private void loadContent()
//          {
//               float enemyY = k_EnemiesStartingY;
//               float enemyX = k_EnemiesStartingX;

//               for (int row = 0; row < EnemyMatrixRows; row++)
//               {
//                    for (int col = 0; col < EnemyMatrixCols; col++)
//                    {
//                         if (row < NumOfPinkEnemiesRows)
//                         {
//                              EnemiesMatrix[row, col] = s_EntityFactory.Create(typeof(EnemyPink)) as Enemy;
//                         }
//                         else if (row < NumOfPinkEnemiesRows + NumOfLightBlueEnemiesRows)
//                         {
//                              EnemiesMatrix[row, col] = s_EntityFactory.Create(typeof(EnemyLightBlue)) as Enemy;
//                         }
//                         else
//                         {
//                              EnemiesMatrix[row, col] = s_EntityFactory.Create(typeof(EnemyYellow)) as Enemy;
//                         }

//                         Enemy enemy = EnemiesMatrix[row, col];

//                         enemy.Position = new Vector2(enemyX, enemyY);
//                         enemyX += enemy.Width + (enemy.Width * EnemiesOffset);
//                         enemy.Destroyed += onDestroyed;
//                    }

//                    enemyY += EnemiesMatrix[row, 0].Height + (EnemiesMatrix[row, 0].Height * EnemiesOffset);
//                    enemyX = k_EnemiesStartingX;
//               }

//               MotherShip.Destroyed += onDestroyed;
//          }

//          private void onDestroyed(Enemy i_Enemy)
//          {
//               if (i_Enemy is ShooterEnemy)
//               {
//                    EnemiesDestroyed++;
//               }

//               if (EnemiesDestroyed % k_NumOfDeadEnemiesToIncreaseVelocity == 0)
//               {
//                    IncreaseAllEnemiesVelocity(k_PercentageToIncreaseVelocityOnNumOfDeadEnemies);
//               }

//               if(!(i_Enemy is MotherShip))
//               {
//                    r_Game.Components.Remove(i_Enemy);
//               }
//          }

//          public void EnemiesTryAttack()
//          {
//               r_RandomBehavior.DelayedAction = chooseRandomEnemyToShoot;
//               r_RandomBehavior.TryInvokeDelayedAction();
//          }

//          private void chooseRandomEnemyToShoot()
//          {
//               List<List<int>> populatedCoords = getEnemiesMatrixPopulatedCoord();

//               int populatedCoordsCount = populatedCoords.Count;

//               if(populatedCoordsCount > 0)
//               {
//                    int populatedCoordIndex = r_RandomBehavior.GetRandomNumber(0, populatedCoordsCount);

//                    (EnemiesMatrix[populatedCoords[populatedCoordIndex][0], populatedCoords[populatedCoordIndex][1]] as ShooterEnemy).Shoot();
//               }
//          }

//          private List<List<int>> getEnemiesMatrixPopulatedCoord()
//          {
//               List<List<int>> rows = new List<List<int>>();

//               for(int row = 0; row < EnemyMatrixRows; row++)
//               {
//                    for (int col = 0; col < EnemyMatrixCols; col++)
//                    {
//                         if(EnemiesMatrix[row, col].IsAlive)
//                         {
//                              rows.Add(new List<int>() { row, col });
//                         }
//                    }
//               }

//               return rows;
//          }

//          private List<int> getEnemiesMatrixPopulatedCols()
//          {
//               List<int> cols = new List<int>();

//               for (int col = 0; col < EnemyMatrixCols; col++)
//               {
//                    for (int row = 0; row < EnemyMatrixRows; row++)
//                    {
//                         if (EnemiesMatrix[row, col].IsAlive)
//                         {
//                              cols.Add(col);
//                              break;
//                         }
//                    }
//               }

//               return cols;
//          }

//          public void Draw(SpriteBatch i_SpriteBatch)
//          {
//               if (MotherShip.Visible && MotherShip.IsAlive)
//               {
//                    i_SpriteBatch.Draw(MotherShip.Graphics, MotherShip.Position, Color.Red);
//               }

//               for (int row = 0; row < EnemyMatrixRows; row++)
//               {
//                    for (int col = 0; col < EnemyMatrixCols; col++)
//                    {
//                         Enemy enemy = EnemiesMatrix[row, col];

//                         if (enemy.IsAlive)
//                         {
//                              if (row < NumOfPinkEnemiesRows)
//                              {
//                                   i_SpriteBatch.Draw(enemy.Graphics, enemy.Position, Color.Pink);
//                              }
//                              else if (row < NumOfPinkEnemiesRows + NumOfLightBlueEnemiesRows)
//                              {
//                                   i_SpriteBatch.Draw(enemy.Graphics, enemy.Position, Color.LightBlue);
//                              }
//                              else
//                              {
//                                   i_SpriteBatch.Draw(enemy.Graphics, enemy.Position, Color.LightYellow);
//                              }

//                              foreach (Bullet bullet in (enemy as ShooterEnemy).Bullets)
//                              {
//                                   i_SpriteBatch.Draw(bullet.Graphics, bullet.Position, Color.Red);
//                              }
//                         }
//                    }
//               }
//          }
//     }
//}


using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyManager : GameComponent
     {
          private const int k_NumOfDeadEnemiesToIncreaseVelocity = 5;
          private const int k_MatrixRows = 5;
          private const int k_MatrixCols = 9;
          private const float k_PercentageToIncreaseVelocityOnRowDescend = 0.05f;
          private const float k_PercentageToIncreaseVelocityOnNumOfDeadEnemies = 0.03f;
          private const float k_EnemiesStartingY = 96;
          private const float k_EnemiesStartingX = 0;
          private const float k_SpaceBetweenEnemies = 32f * 0.6f;
          private Enemy m_LeftMostRepresentetive;
          private Enemy m_RightMostRepresentetive;
          private readonly ICollisionsManager r_CollisionsManager;
          private readonly List<List<Enemy>> r_EnemyMatrix;
          private int m_DeadEnemiesCounter = 0;

          public EnemyManager(Game i_Game) : base(i_Game)
          {
               this.Game.Components.Add(this);
               r_EnemyMatrix = new List<List<Enemy>>(k_MatrixRows);
               r_CollisionsManager = this.Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
          }

          public override void Initialize()
          {
               initMatrix();
               populateMatrix();
               setRepresentetives();

               base.Initialize();
          }

          private void setRepresentetives()
          {
               setLeftRepresentetive();
               setRightRepresentetive();
          }

          private void setRightRepresentetive()
          {
               for (int col = 0; col < k_MatrixCols; col++)
               {
                    for (int row = 0; row < k_MatrixRows; row++)
                    {
                         if (r_EnemyMatrix[row][col].IsAlive)
                         {
                              m_LeftMostRepresentetive = r_EnemyMatrix[row][col];
                              break;
                         }
                    }
               }
          }

          private void setLeftRepresentetive()
          {
               for (int col = k_MatrixCols - 1; col >= 0; col--)
               {
                    for (int row = 0; row < k_MatrixRows; row++)
                    {
                         if (r_EnemyMatrix[row][col].IsAlive)
                         {
                              m_RightMostRepresentetive = r_EnemyMatrix[row][col];
                              break;
                         }
                    }
               }
          }

          private void initMatrix()
          {
               for (int i = 0; i < r_EnemyMatrix.Capacity; i++)
               {
                    r_EnemyMatrix.Add(new List<Enemy>(k_MatrixCols));
               }
          }

          public override void Update(GameTime gameTime)
          {
               checkWindowCollision(m_RightMostRepresentetive);
               checkWindowCollision(m_LeftMostRepresentetive);

               base.Update(gameTime);
          }

          private void checkWindowCollision(Sprite i_Enemy)
          {
               bool isCollidedWithRightEdge = r_CollisionsManager.IsCollideWithWindowRightEdge(i_Enemy);
               bool isCollidedWithLeftEdge = r_CollisionsManager.IsCollideWithWindowLeftEdge(i_Enemy);

               if (isCollidedWithRightEdge && i_Enemy.MoveDirection == Sprite.Right ||
                    isCollidedWithLeftEdge && i_Enemy.MoveDirection == Sprite.Left)
               {
                    bounceDown();

                    if (isCollidedWithRightEdge)
                    {
                         changeSpriteDirection(Sprite.Left);
                    }
                    else
                    {
                         changeSpriteDirection(Sprite.Right);
                    }
               }
          }

          private void populateMatrix()
          {
               float top = k_EnemiesStartingY;

               for (int row = 0; row < k_MatrixRows; row++)
               {
                    float left = 0;

                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         if (row < 1)
                         {
                              r_EnemyMatrix[row].Add(new ShooterEnemy(new Rectangle(0, 0, 32, 32), 250, Color.Pink, this.Game));
                         }
                         else if (row < 3)
                         {
                              r_EnemyMatrix[row].Add(new ShooterEnemy(new Rectangle(64, 0, 32, 32), 150, Color.LightBlue, this.Game));
                         }
                         else
                         {
                              r_EnemyMatrix[row].Add(new ShooterEnemy(new Rectangle(96, 0, 32, 32), 100, Color.LightYellow, this.Game));
                         }

                         Enemy enemy = r_EnemyMatrix[row][col];
                         enemy.StartingPosition = new Vector2(left, top);
                         enemy.Destroyed += enemy_Destroyed;
                         enemy.NoneCollisionGroupKey = this;
                         left += enemy.Width + k_SpaceBetweenEnemies;
                    }

                    top += r_EnemyMatrix[row][0].Height + k_SpaceBetweenEnemies;
               }
          }

          private void enemy_Destroyed(Entity i_Enemy)
          {
               if(i_Enemy == m_LeftMostRepresentetive)
               {
                    setLeftRepresentetive();
               }
               else if(i_Enemy == m_RightMostRepresentetive)
               {
                    setRightRepresentetive();
               }

               m_DeadEnemiesCounter++;

               if (isIncreaseEnemiesSpeed())
               {
                    increaseAllEnemiesVelocity();
               }
          }

          private bool isIncreaseEnemiesSpeed()
          {
               return m_DeadEnemiesCounter % k_NumOfDeadEnemiesToIncreaseVelocity == 0;
          }

          private void increaseAllEnemiesVelocity()
          {
               for (int row = 0; row < k_MatrixRows; row++)
               {
                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         Enemy enemy = r_EnemyMatrix[row][col];
                         enemy.Velocity += enemy.Velocity * k_PercentageToIncreaseVelocityOnNumOfDeadEnemies;
                    }
               }
          }

          private void changeSpriteDirection(Vector2 i_Direction)
          {
               for(int row = 0; row < k_MatrixRows; row++)
               {
                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         r_EnemyMatrix[row][col].MoveDirection = i_Direction;
                    }
               }
          }

          private void bounceDown()
          {
               for (int row = 0; row < k_MatrixRows; row++)
               {
                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         Enemy enemy = r_EnemyMatrix[row][col];
                         enemy.Velocity += enemy.Velocity * k_PercentageToIncreaseVelocityOnRowDescend;
                         enemy.Position += Sprite.Down * (enemy.Height / 2);
                    }
               }
          }
     }
}

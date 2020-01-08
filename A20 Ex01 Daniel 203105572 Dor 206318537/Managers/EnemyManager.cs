using System;
using System.Collections.Generic;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
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
          private readonly IRandomBehavior r_RandomBehavior;

          private int m_DeadEnemiesCounter = 0;

          public EnemyManager(Game i_Game) : base(i_Game)
          {
               this.Game.Components.Add(this);
               r_EnemyMatrix = new List<List<Enemy>>(k_MatrixRows);
               r_CollisionsManager = this.Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
               r_RandomBehavior = this.Game.Services.GetService(typeof(IRandomBehavior)) as IRandomBehavior;
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

          private void setLeftRepresentetive()
          {
               bool isFound = false;

               for (int col = 0; col < k_MatrixCols && !isFound; col++)
               {
                    for (int row = 0; row < k_MatrixRows && !isFound; row++)
                    {
                         if (r_EnemyMatrix[row][col].IsAlive)
                         {
                              isFound = true;
                              m_LeftMostRepresentetive = r_EnemyMatrix[row][col];
                         }
                    }
               }
          }

          private void setRightRepresentetive()
          {
               bool isFound = false;

               for (int col = k_MatrixCols - 1; col >= 0 && !isFound; col--)
               {
                    for (int row = 0; row < k_MatrixRows && !isFound; row++)
                    {
                         if (r_EnemyMatrix[row][col].IsAlive)
                         {
                              isFound = true;
                              m_RightMostRepresentetive = r_EnemyMatrix[row][col];
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
               
               checkWindowCollision();
               chooseEnemyShoot();

               base.Update(gameTime);
          }

          private void chooseEnemyShoot()
          {
              throw new NotImplementedException();
          }

          private void checkWindowCollision()
          {
               Vector2 rightMostRepNextPosition = m_RightMostRepresentetive.Position + (m_RightMostRepresentetive.Velocity * m_RightMostRepresentetive.MoveDirection) / 2;
               Vector2 leftMostRepNextPosition = m_LeftMostRepresentetive.Position + (m_LeftMostRepresentetive.Velocity * m_LeftMostRepresentetive.MoveDirection) / 2;

               if (rightMostRepNextPosition.X >= this.Game.GraphicsDevice.Viewport.Width - m_RightMostRepresentetive.Width)
               {
                    handleWindowCollision(Sprite.Left);
               }
               else if (leftMostRepNextPosition.X <= 0)
               {
                    handleWindowCollision(Sprite.Right);
               }
          }

          private void handleWindowCollision(Vector2 i_DirectionChangeTo)
          {
               for (int row = 0; row < k_MatrixRows; row++)
               {
                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         Enemy enemy = r_EnemyMatrix[row][col];
                         enemy.Position += new Vector2(0, enemy.Height / 2);
                         enemy.MoveDirection = i_DirectionChangeTo;
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
                              r_EnemyMatrix[row].Add(new AlienMatrixEnemy(new Rectangle(0, 0, 32, 32), 250, Color.Pink, this.Game));
                         }
                         else if (row < 3)
                         {
                              r_EnemyMatrix[row].Add(new AlienMatrixEnemy(new Rectangle(64, 0, 32, 32), 150, Color.LightBlue, this.Game));
                         }
                         else
                         {
                              r_EnemyMatrix[row].Add(new AlienMatrixEnemy(new Rectangle(128, 0, 32, 32), 100, Color.LightYellow, this.Game));
                         }

                         Enemy enemy = r_EnemyMatrix[row][col];
                         enemy.StartingPosition = new Vector2(left, top);
                         enemy.Destroyed += enemy_Destroyed;
                         enemy.GroupRepresentative = this;
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
     }
}

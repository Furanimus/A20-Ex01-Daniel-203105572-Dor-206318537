﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Components;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimators;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class EnemyManager : CompositeDrawableComponent<Enemy>
     {
          public event Action MatrixReachedBottomWindow;

          public event Action AllEnemiesDied;

          private const int k_MaxLevel                                  = 5;
          private const int k_MatrixRows                                = 5;
          private const int k_MatrixCols                                = 14;
          private const int k_InitialVisibleRows                        = 5;
          private const int k_InitialVisibleCols                        = 9;
          private const int k_AlienEnemyVelocity                        = 32;
          private const int k_EnemyWidth                                = 32;
          private const int k_EnemyHeight                               = 32;
          private const int k_PinkEnemyTextureX                         = 0;
          private const int k_LightBlueTextureX                         = 64;
          private const int k_LightYellowTextureX                       = 128;
          private const int k_EnemyTextureY                             = 0;
          private const int k_MaxRowForBlueEnemies                      = 3;
          private const int k_MaxRowForPinkEnemies                      = 1;
          private const int k_MaxMillisecondToRoll                      = 300;
          private const int k_NumOfDeadEnemiesToIncreaseVelocity        = 5;
          private const int k_NumOfAnimationCells                       = 2;
          private const int k_StartingPinkEnemyScore                    = 250;
          private const int k_StartingLightBlueEnemyScore               = 150;
          private const int k_StartingLightYellowEnemyScore             = 100;
          private const int k_AddedScoresOnNextLevel                    = 140;
          private const float k_CellTime                                = 0.5f;
          private const float k_EnemiesStartingY                        = 96;
          private const float k_EnemiesStartingX                        = 0;
          private const float k_SpaceBetweenEnemies                     = 32f * 0.6f;
          private const float k_IncVelocityOnRowDecendPercentage        = 0.05f;
          private const float k_IncVelocityOnNumOfDeadEnemiesPercentage = 0.03f;
          private readonly IRandomBehavior r_RandomBehavior;
          private readonly List<List<Enemy>> r_EnemyMatrix;
          private readonly GameScreen r_GameScreen;
          private Enemy m_RightMostRepresentetive;
          private Enemy m_DownMostRepresentetive;
          private Enemy m_LeftMostRepresentetive;
          private TimeSpan m_IntervalToNextShoot;
          private int m_VisibleCols = k_InitialVisibleCols;
          private int m_VisibleRows = k_InitialVisibleRows;
          private int m_DeadEnemiesCounter;

          public EnemyManager(GameScreen i_GameScreen) : base(i_GameScreen.Game)
          {
               r_GameScreen = i_GameScreen;
               r_EnemyMatrix = new List<List<Enemy>>(k_MatrixRows);
               r_RandomBehavior = this.Game.Services.GetService(typeof(IRandomBehavior)) as IRandomBehavior;
               r_GameScreen.Add(this);
               this.DrawOrder = this.UpdateOrder = 5;
          }

          public override void Initialize()
          {
               populateMatrix();
               setRepresentetives();
               this.Add(new RedMotherShip(r_GameScreen));

               base.Initialize();
          }

          private void setRepresentetives()
          {
               setLeftRepresentetive();
               setRightRepresentetive();
               setDownRepresentetive();
          }

          public int MaxShotsInMidAir { get; set; } = 1;

          public int VisibleRows
          {
               get
               {
                    return m_VisibleRows;
               }

               set
               {
                    m_VisibleRows = k_InitialVisibleRows + (value - k_InitialVisibleRows) % k_MaxLevel;
               }
          }

          public int VisibleCols
          {
               get
               {
                    return m_VisibleCols;
               }

               set
               {
                    m_VisibleCols = k_InitialVisibleCols + (value - k_InitialVisibleCols) % k_MaxLevel;
               }
          }

          private void setDownRepresentetive()
          {
               bool isFound = false;

               for (int row = k_MatrixRows - 1; row >= 0 && !isFound; row--)
               {
                    for (int col = 0; col < k_MatrixCols && !isFound; col++)
                    {
                         Enemy enemy = r_EnemyMatrix[row][col];

                         if (enemy.IsAlive && enemy.Visible)
                         {
                              isFound = true;
                              m_DownMostRepresentetive = r_EnemyMatrix[row][col];
                         }
                    }
               }
          }

          public void UpdateLevelDifficulty()
          {
               this.VisibleCols++;

               foreach (List<Enemy> row in r_EnemyMatrix)
               {
                    foreach (Enemy enemy in row)
                    {
                         enemy.Score += k_AddedScoresOnNextLevel;
                         (enemy as ShooterEnemy).MaxShotsInMidAir++;
                    }
               }
          }

          private void setLeftRepresentetive()
          {
               bool isFound = false;

               for (int col = 0; col < k_MatrixCols && !isFound; col++)
               {
                    for (int row = 0; row < k_MatrixRows && !isFound; row++)
                    {
                         Enemy enemy = r_EnemyMatrix[row][col];

                         if (enemy.IsAlive && enemy.Visible)
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
                         Enemy enemy = r_EnemyMatrix[row][col];

                         if (enemy.IsAlive && enemy.Visible)
                         {
                              isFound = true;
                              m_RightMostRepresentetive = r_EnemyMatrix[row][col];
                         }
                    }
               }
          }

          public override void Update(GameTime i_GameTime)
          {
               checkWindowCollision();
               handleEnemyToShoot(i_GameTime);

               base.Update(i_GameTime);
          }

          private void handleEnemyToShoot(GameTime i_GameTime)
          {
               m_IntervalToNextShoot -= i_GameTime.ElapsedGameTime;
               bool isIntervalFinished = m_IntervalToNextShoot <= TimeSpan.Zero;

               if (isIntervalFinished)
               {
                    ShooterEnemy enemy = chooseEnemyShoot();
                    bool isShoot = tryShoot(enemy);

                    if (isShoot)
                    {
                         m_IntervalToNextShoot = r_RandomBehavior.GetRandomIntervalMilliseconds(k_MaxMillisecondToRoll);
                    }
               }
          }

          private bool tryShoot(ShooterEnemy i_Enemy)
          {
               bool isShoot = false;

               if (i_Enemy.IsAlive && i_Enemy.Visible)
               {
                    i_Enemy.Shoot();
                    isShoot = true;
               }

               return isShoot;
          }

          private ShooterEnemy chooseEnemyShoot()
          {
               int randomRow = r_RandomBehavior.GetRandomNumber(0, k_MatrixRows);
               int randomCol = r_RandomBehavior.GetRandomNumber(0, k_MatrixCols);
               Enemy enemy = r_EnemyMatrix[randomRow][randomCol];

               return enemy as ShooterEnemy;
          }

          private void checkWindowCollision()
          {
               Vector2 rightMostRepNextPosition = m_RightMostRepresentetive.Position + ((m_RightMostRepresentetive.Velocity * m_RightMostRepresentetive.MoveDirection) / 2);
               Vector2 leftMostRepNextPosition = m_LeftMostRepresentetive.Position + ((m_LeftMostRepresentetive.Velocity * m_LeftMostRepresentetive.MoveDirection) / 2);

               Vector2 downMostHeight = new Vector2(0, m_DownMostRepresentetive.Height);
               Vector2 downMostJumpDistance = new Vector2(0, m_DownMostRepresentetive.Height / 2);
               Vector2 downMostRepBottomNextPosition = m_DownMostRepresentetive.Position + downMostHeight + downMostJumpDistance;

               if (downMostRepBottomNextPosition.Y >= this.Game.GraphicsDevice.Viewport.Height)
               {
                    if (MatrixReachedBottomWindow != null)
                    {
                         MatrixReachedBottomWindow.Invoke();
                    }
               }

               if (rightMostRepNextPosition.X >= this.Game.GraphicsDevice.Viewport.Width - m_RightMostRepresentetive.Width && m_RightMostRepresentetive.MoveDirection == Sprite.Right)
               {
                    handleWindowCollision(Sprite.Left);
               }
               else if (leftMostRepNextPosition.X <= 0 && m_LeftMostRepresentetive.MoveDirection == Sprite.Left)
               {
                    handleWindowCollision(Sprite.Right);
               }
          }

          private void handleWindowCollision(Vector2 i_DirectionChangeTo)
          {
               foreach (List<Enemy> row in r_EnemyMatrix)
               {
                    foreach(Enemy enemy in row)
                    {
                         enemy.Position += new Vector2(0, enemy.Height / 2);
                         enemy.Velocity += enemy.Velocity * k_IncVelocityOnRowDecendPercentage;
                         enemy.MoveDirection = i_DirectionChangeTo;
                    }
               }
          }

          private void populateMatrix()
          {
               updateRows();
               updateCols();
          }

          private void updateRows()
          {
               int rowsToAdd = k_MatrixRows - r_EnemyMatrix.Count;

               for (int row = 0; row < rowsToAdd; row++)
               {
                    r_EnemyMatrix.Add(new List<Enemy>(k_MatrixCols));
               }
          }

          private void updateCols()
          {
               Color color;
               int scoreWorth;
               Rectangle sourceRectangle;
               float top = k_EnemiesStartingY;

               for (int row = 0; row < k_MatrixRows; row++)
               {
                    float left = k_EnemiesStartingX;
                    bool isStartAnimationFromSecondCell = row % 2 == 0;
                    initPropertiesForEnemy(out sourceRectangle, out color, out scoreWorth, row);

                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         AlienMatrixEnemy enemy = new AlienMatrixEnemy(sourceRectangle, scoreWorth, color, r_GameScreen);
                         r_EnemyMatrix[row].Add(enemy);

                         setVisibility(enemy, row, col);
                         initEnemy(enemy, left, top, 
                              new CellAnimator(isStartAnimationFromSecondCell, TimeSpan.FromSeconds(k_CellTime), k_NumOfAnimationCells, TimeSpan.Zero));

                         this.Add(enemy);
                         left += enemy.Width + k_SpaceBetweenEnemies;
                    }

                    top += k_EnemyHeight + k_SpaceBetweenEnemies;
               }
          }

          private void initEnemy(AlienMatrixEnemy i_Enemy, float i_PositionX, float i_PositionY, CellAnimator i_CellAnimator = null)
          {
               i_Enemy.MaxShotsInMidAir = MaxShotsInMidAir;
               i_Enemy.CellAnimation = i_CellAnimator;
               i_Enemy.StartPosition = new Vector2(i_PositionX, i_PositionY);
               i_Enemy.StartVelocity = new Vector2(k_AlienEnemyVelocity, 0);
               i_Enemy.VisibleChanged += enemy_VisibleChanged;
               i_Enemy.GroupRepresentative = this;
          }

          private void initPropertiesForEnemy(out Rectangle o_SourceRectangle, out Color o_Color, out int o_ScoreWorth, int i_CurrentRow)
          {
               if (i_CurrentRow < k_MaxRowForPinkEnemies)
               {
                    o_Color = Color.Pink;
                    o_ScoreWorth = k_StartingPinkEnemyScore;
                    o_SourceRectangle = new Rectangle(k_PinkEnemyTextureX, k_EnemyTextureY, k_EnemyWidth, k_EnemyHeight);
               }
               else if (i_CurrentRow < k_MaxRowForBlueEnemies)
               {
                    o_Color = Color.LightBlue;
                    o_ScoreWorth = k_StartingLightBlueEnemyScore;
                    o_SourceRectangle = new Rectangle(k_LightBlueTextureX, k_EnemyTextureY, k_EnemyWidth, k_EnemyHeight);
               }
               else
               {
                    o_Color = Color.LightYellow;
                    o_ScoreWorth = k_StartingLightYellowEnemyScore;
                    o_SourceRectangle = new Rectangle(k_LightYellowTextureX, k_EnemyTextureY, k_EnemyWidth, k_EnemyHeight);
               }
          }

          private void updateMatrix()
          { 
               for(int row = 0; row < k_MatrixRows; row++)
               {
                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         Enemy enemy = r_EnemyMatrix[row][col];
                         setVisibility(enemy, row, col);
                    }
               }
          }


          private void setVisibility(Enemy i_Enemy, int i_Row, int i_Col)
          {
               if (i_Row < VisibleRows && i_Col < VisibleCols)
               {
                    i_Enemy.Visible = true;
                    i_Enemy.Enabled = true;
               }
               else
               {
                    i_Enemy.Visible = false;
                    i_Enemy.Enabled = false;
               }
          }

          public void Reset()
          {
               foreach (List<Enemy> rows in r_EnemyMatrix)
               {
                    foreach (Enemy enemy in rows)
                    {
                         enemy.ResetProperties();
                         enemy.Animations["JumpMovement"].Resume();
                         enemy.Animations["Cell"].Resume();
                    }
               }

               m_DeadEnemiesCounter = 0;
               updateMatrix();
               setRepresentetives();
          }

          private void enemy_VisibleChanged(object i_Sender, EventArgs i_Args)
          {
               Enemy enemy = i_Sender as Enemy;

               if (!enemy.Visible && !enemy.IsAlive)
               {
                    if (enemy == m_DownMostRepresentetive)
                    {
                         setDownRepresentetive();
                    }

                    if (enemy == m_LeftMostRepresentetive)
                    {
                         setLeftRepresentetive();
                    }

                    if (enemy == m_RightMostRepresentetive)
                    {
                         setRightRepresentetive();
                    }

                    m_DeadEnemiesCounter++;

                    if (AllEnemiesDied != null && m_DeadEnemiesCounter == VisibleCols * VisibleRows)
                    {
                         AllEnemiesDied.Invoke();
                    }

                    if (isIncreaseEnemiesSpeed())
                    {
                         increaseAllEnemiesVelocity();
                    }
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
                         enemy.Velocity += enemy.Velocity * k_IncVelocityOnNumOfDeadEnemiesPercentage;
                    }
               }
          }
     }
}

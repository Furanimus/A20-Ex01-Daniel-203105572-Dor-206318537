using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Models.Animators.ConcreteAnimators;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class EnemyManager : GameComponent
     {
          public event Action MatrixReachedBottomWindow;

          private const int k_MatrixCols                                = 9;
          private const int k_MatrixRows                                = 5;
          private const float k_EnemiesStartingY                        = 96;
          private const float k_EnemiesStartingX                        = 0;
          private const int k_MaxRowForBlueEnemies                      = 3;
          private const int k_MaxRowForPinkEnemies                      = 1;
          private const int k_MaxMillisecondToRoll                      = 1000;
          private const float k_SpaceBetweenEnemies                     = 32f * 0.6f;
          private const int k_NumOfDeadEnemiesToIncreaseVelocity        = 5;
          private const float k_IncVelocityOnRowDecendPercentage        = 0.05f;
          private const float k_IncVelocityOnNumOfDeadEnemiesPercentage = 0.03f;
          private readonly IRandomBehavior r_RandomBehavior;
          private readonly List<List<Enemy>> r_EnemyMatrix;
          private readonly MotherShip r_MotherShip;
          private Enemy m_RightMostRepresentetive;
          private Enemy m_DownMostRepresentetive;
          private Enemy m_LeftMostRepresentetive;
          private TimeSpan m_IntervalToNextShoot;
          private int m_DeadEnemiesCounter;

          public EnemyManager(Game i_Game) : base(i_Game)
          {
               r_EnemyMatrix       = new List<List<Enemy>>(k_MatrixRows);
               r_RandomBehavior    = this.Game.Services.GetService(typeof(IRandomBehavior)) as IRandomBehavior;
               r_MotherShip        = new RedMotherShip(i_Game);

               this.Game.Components.Add(this);
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
               setDownRepresentetive();
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

          private void setDownRepresentetive()
          {
               bool isFound = false;

               for (int row = k_MatrixRows - 1; row >= 0 && !isFound; row--)
               {
                    for (int col = 0; col < k_MatrixCols && !isFound; col++)
                    {
                         if (r_EnemyMatrix[row][col].IsAlive)
                         {
                              isFound = true;
                              m_DownMostRepresentetive = r_EnemyMatrix[row][col];
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

                    if(isShoot)
                    {
                         m_IntervalToNextShoot = r_RandomBehavior.GetRandomIntervalMilliseconds(k_MaxMillisecondToRoll);
                    }
               }
          }

          private bool tryShoot(ShooterEnemy i_Enemy)
          {
               bool isShoot = false;

               if (i_Enemy.IsAlive && i_Enemy.Gun.BulletShot < i_Enemy.Gun.Capacity)
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
               Vector2 rightMostRepNextPosition = m_RightMostRepresentetive.Position + (m_RightMostRepresentetive.Velocity * m_RightMostRepresentetive.MoveDirection) / 2;
               Vector2 leftMostRepNextPosition = m_LeftMostRepresentetive.Position + (m_LeftMostRepresentetive.Velocity * m_LeftMostRepresentetive.MoveDirection) / 2;
               
               Vector2 downMostHeight = new Vector2(0, m_DownMostRepresentetive.Height);
               Vector2 downMostJumpDistance = new Vector2(0, m_DownMostRepresentetive.Height / 2);
               Vector2 downMostRepBottomNextPosition = m_DownMostRepresentetive.Position + downMostHeight + downMostJumpDistance;

               if(downMostRepBottomNextPosition.Y >= this.Game.GraphicsDevice.Viewport.Height)
               {
                    if(MatrixReachedBottomWindow != null)
                    {
                         MatrixReachedBottomWindow.Invoke();
                    }
               }

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

                         enemy.Velocity += enemy.Velocity * k_IncVelocityOnRowDecendPercentage;
                         enemy.MoveDirection = i_DirectionChangeTo;
                    }
               }
          }

          private void populateMatrix()
          {
               float top = k_EnemiesStartingY;

               for (int row = 0; row < k_MatrixRows; row++)
               {
                    float left          = k_EnemiesStartingX;
                    bool isStartAnimationFromSecondCell = row % 2 == 0;
                    Color color;
                    int scoreWorth;
                    Rectangle sourceRectangle;

                    if (row < k_MaxRowForPinkEnemies)
                    {
                         color = Color.Pink;
                         scoreWorth = 250;
                         sourceRectangle = new Rectangle(0, 0, 32, 32);
                    }
                    else if (row < k_MaxRowForBlueEnemies)
                    {
                         color = Color.LightBlue;
                         scoreWorth = 150;
                         sourceRectangle = new Rectangle(64, 0, 32, 32);
                    }
                    else
                    {
                         color = Color.LightYellow;
                         scoreWorth = 100;
                         sourceRectangle = new Rectangle(128, 0, 32, 32);
                    }

                    for (int col = 0; col < k_MatrixCols; col++)
                    {
                         r_EnemyMatrix[row].Add(new AlienMatrixEnemy(sourceRectangle, scoreWorth, color, this.Game));

                         AlienMatrixEnemy enemy      = r_EnemyMatrix[row][col] as AlienMatrixEnemy;
                         enemy.CellAnimation         = new CellAnimator(isStartAnimationFromSecondCell, TimeSpan.FromSeconds(0.5), 2, TimeSpan.Zero);
                         enemy.StartingPosition      = new Vector2(left, top);
                         enemy.VisibleChanged       += enemy_VisibleChanged;
                         enemy.GroupRepresentative   = this;
                         left                       += enemy.Width + k_SpaceBetweenEnemies;
                    }

                    top += r_EnemyMatrix[row][0].Height + k_SpaceBetweenEnemies;
               }
          }

          private void enemy_VisibleChanged(object sender, EventArgs e)
          {
               Enemy enemy = sender as Enemy;

               if (!enemy.Visible)
               {
                    if(enemy == m_DownMostRepresentetive)
                    {
                         setDownRepresentetive();
                    }
                    else if (enemy == m_LeftMostRepresentetive)
                    {
                         setLeftRepresentetive();
                    }
                    else if (enemy == m_RightMostRepresentetive)
                    {
                         setRightRepresentetive();
                    }

                    m_DeadEnemiesCounter++;

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

using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     class BarrierManager : GameComponent
     {
          private List<Barrier> m_Barriers;
          private readonly ICollisionsManager r_CollisionsManager;
          private const float k_BarrierWidth = 44;
          private const float k_BarrierHeight = 32;
          private const float k_OffsetBetweenObstacles = k_BarrierWidth;
          private const int k_NumOfBarriers = 4;
          private readonly float r_PlayerStartingY;
          private readonly float r_PlayerHeight;
          //private Vector2 m_InitPositionOfBarrier;
          private List<Color[]> m_Pixles;

          public BarrierManager(Game i_Game, float i_PlayerStartingY, float i_PlayerHeight)
                        : base(i_Game)
          {
               //k_ObstacleWidthOrAndOffset = i_ObstacleWidth;
               m_Barriers = new List<Barrier>(k_NumOfBarriers);
               r_CollisionsManager = this.Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
               r_PlayerStartingY = i_PlayerStartingY;
               r_PlayerHeight = i_PlayerHeight;
               this.Game.Components.Add(this);
          }

          public override void Initialize()
          {
               Vector2 startingObstaclesPoint = getStartingPosition();

               for (int i = 0; i < k_NumOfBarriers; i++)
               {
                    Barrier barrier = new Barrier(this.Game);

                    setBarrierProperties(barrier, startingObstaclesPoint);
                    m_Barriers.Add(barrier);
                    startingObstaclesPoint.X += barrier.Width * 2;
               }

               base.Initialize();
          }

          public Vector2 StartingPositionOfFirstObstacle { get; set; }

          internal void initPixelData()
          {
               for (int i = 0; i < k_NumOfBarriers; i++)
               {
                    Barrier barrier = m_Barriers[i];
                    uint totalPixels = (uint)barrier.Width * (uint)barrier.Height;
                    barrier.Texture.GetData<Color>(m_Pixles[i]);
               }
          }

          private Vector2 getStartingPosition()
          {
               Vector2 position;

               float posX = (this.Game.GraphicsDevice.Viewport.Width - getObstaclesLength()) / 2;
               float posY = r_PlayerStartingY - k_BarrierHeight - r_PlayerHeight;
               position = new Vector2(posX, posY);
               return position;
          }

          private float getObstaclesLength()
          {
               float res;
               //float firstToLastBarrierLen = getObstaclesLength();
               res = k_NumOfBarriers * k_BarrierWidth + (k_NumOfBarriers - 1 ) * k_BarrierWidth;
               return res;
          }

          private void setBarrierProperties(Barrier i_Barrier, Vector2 i_Pos)
          {
               i_Barrier.GroupRepresentative = this;
               i_Barrier.StartingPosition = i_Pos;
          }

          public override void Update(GameTime gameTime)
          {
               if(r_CollisionsManager.IsCollideWithWindowEdge(m_Barriers[k_NumOfBarriers - 1]))
               {
                    foreach(Barrier barrier in m_Barriers)
                    {
                         barrier.Velocity *= -1;
                    }
               }

               base.Update(gameTime);


          }
     }
}

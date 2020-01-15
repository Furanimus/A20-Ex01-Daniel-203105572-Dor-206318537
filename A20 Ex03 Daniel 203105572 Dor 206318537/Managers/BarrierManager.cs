using System.Collections.Generic;
using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex01_Daniel_203105572_Dor_206318537.Components;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class BarrierManager : CompositeDrawableComponent<Barrier>
     {
          private const float k_BarrierWidth = 44;
          private const float k_BarrierHeight = 32;
          private const int k_NumOfBarriers = 4;
          private const int k_CallOrder = 5;
          private const float k_OffsetToChangeDirection = k_BarrierWidth / 2;
          private const string k_BarrierAsset = @"Sprites\Barrier_44x32";
          private readonly float r_PlayerStartingY;
          private readonly float r_PlayerHeight;
          private readonly List<Barrier> r_Barriers;
          private readonly GameScreen r_GameScreen;
          private Texture2D m_BarrierTexture;

          public BarrierManager(GameScreen i_GameScreen, float i_PlayerStartingY, float i_PlayerHeight)
                        : base(i_GameScreen.Game)
          {
               r_Barriers = new List<Barrier>(k_NumOfBarriers);
               r_PlayerStartingY = i_PlayerStartingY;
               r_PlayerHeight = i_PlayerHeight;
               this.UpdateOrder = k_CallOrder;
               r_GameScreen = i_GameScreen;
               r_GameScreen.Add(this);
          }

          public override void Initialize()
          {
               createBarriers();
           
               base.Initialize();
          }

          private void createBarriers()
          {
               Vector2 startingObstaclesPoint = getStartingPosition();

               for (int i = 0; i < k_NumOfBarriers; i++)
               {
                    Barrier barrier = new Barrier(r_GameScreen);
                    setBarrierProperties(barrier, startingObstaclesPoint);
                    r_Barriers.Add(barrier);
                    startingObstaclesPoint.X += barrier.Width * 2;
               }
          }

          protected override void LoadContent()
          {
               base.LoadContent();

               m_BarrierTexture = Game.Content.Load<Texture2D>(k_BarrierAsset);

               foreach (Barrier barrier in r_Barriers)
               {
                    barrier.Texture = m_BarrierTexture.Clone(this.Game.GraphicsDevice);
               }

          }


          private Vector2 getStartingPosition()
          {
               float posX = (this.Game.GraphicsDevice.Viewport.Width - getObstaclesLength()) / 2;
               float posY = r_PlayerStartingY - k_BarrierHeight - r_PlayerHeight;

               return new Vector2(posX, posY);
          }

          private float getObstaclesLength()
          {
               return (k_NumOfBarriers * k_BarrierWidth) + ((k_NumOfBarriers - 1) * k_BarrierWidth);
          }

          private void setBarrierProperties(Barrier i_Barrier, Vector2 i_Pos)
          {
               i_Barrier.GroupRepresentative = this;
               i_Barrier.StartingPosition = i_Pos;
          }

          public override void Update(GameTime i_GameTime)
          {
               if (isBarriersChangeDirection())
               {
                    foreach (Barrier barrier in r_Barriers)
                    {
                         barrier.Velocity *= -1;
                    }
               }

               base.Update(i_GameTime);
          }

          private bool isBarriersChangeDirection()
          {
               Barrier representetive = r_Barriers[0];
               Vector2 BarriersRepresentetiveCurrPosition = representetive.Position;
               bool isPassedRightOffsetToChangeBarrierDirection = BarriersRepresentetiveCurrPosition.X >= representetive.StartingPosition.X + k_OffsetToChangeDirection;
               bool isPassedLeftOffsetToChangeBarrierDirection = BarriersRepresentetiveCurrPosition.X <= representetive.StartingPosition.X - k_OffsetToChangeDirection;

               return isPassedRightOffsetToChangeBarrierDirection || isPassedLeftOffsetToChangeBarrierDirection;
          }
     }
}

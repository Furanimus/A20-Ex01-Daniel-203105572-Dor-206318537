using System.Collections.Generic;
using Microsoft.Xna.Framework;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     public class BarrierManager : DrawableGameComponent
     {
          private const float k_BarrierWidth = 44;
          private const float k_BarrierHeight = 32;
          private const int k_NumOfBarriers = 4;
          private const int k_CallOrder = 5;
          private const string k_BarrierAsset = @"Sprites\Barrier_44x32";
          private readonly ICollisionsManager r_CollisionsManager;
          private readonly float r_PlayerStartingY;
          private readonly float r_PlayerHeight;
          private readonly List<Barrier> r_Barriers;
          private readonly float r_OffsetToChangeDirection;

          public BarrierManager(Game i_Game, float i_PlayerStartingY, float i_PlayerHeight)
                        : base(i_Game)
          {
               r_Barriers = new List<Barrier>(k_NumOfBarriers);
               r_CollisionsManager = this.Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
               r_PlayerStartingY = i_PlayerStartingY;
               r_PlayerHeight = i_PlayerHeight;
               this.Game.Components.Add(this);
               this.DrawOrder = k_CallOrder;
               this.UpdateOrder = k_CallOrder;
               r_OffsetToChangeDirection = k_BarrierWidth / 2;
          }

          public override void Initialize()
          {
               Vector2 startingObstaclesPoint = getStartingPosition();

               for (int i = 0; i < k_NumOfBarriers; i++)
               {
                    Barrier barrier = new Barrier(this.Game);

                    setBarrierProperties(barrier, startingObstaclesPoint);
                    r_Barriers.Add(barrier);
                    startingObstaclesPoint.X += barrier.Width * 2;
               }

               base.Initialize();
          }

          protected override void LoadContent()
          {
               BarrierTexture = this.Game.Content.Load<Texture2D>(k_BarrierAsset);

               foreach(Barrier barrier in r_Barriers)
               {
                    barrier.Texture = BarrierTexture.Clone(this.GraphicsDevice);
               }

               base.LoadContent();
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
               res = (k_NumOfBarriers * k_BarrierWidth) + ((k_NumOfBarriers - 1) * k_BarrierWidth);
               return res;
          }

          public Texture2D BarrierTexture { get; private set; }

          private void setBarrierProperties(Barrier i_Barrier, Vector2 i_Pos)
          {
               i_Barrier.GroupRepresentative = this;
               i_Barrier.StartingPosition = i_Pos;
          }

          public override void Update(GameTime gameTime)
          {
               if (isBarriersChangeDirection())
               {
                    foreach (Barrier barrier in r_Barriers)
                    {
                         barrier.Velocity *= -1;
                    }
               }

               base.Update(gameTime);
          }

          private bool isBarriersChangeDirection()
          {
               bool res = false;
               Barrier representetive = r_Barriers[0];
               Vector2 BarriersRepresentetiveCurrPosition = representetive.Position;

               if(BarriersRepresentetiveCurrPosition.X >= representetive.StartingPosition.X + r_OffsetToChangeDirection
                    || BarriersRepresentetiveCurrPosition.X <= representetive.StartingPosition.X - r_OffsetToChangeDirection)
               {
                    res = true;
               }

               return res;
          }
     }
}

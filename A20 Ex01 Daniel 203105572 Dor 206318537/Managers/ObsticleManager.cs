using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     class ObsticleManager : GameComponent
     {
          private List<Barrier> m_Barriers;
          private const int k_NumOfBarriers = 4;
          private Vector2 m_InitPositionOfBarrier;

          public ObsticleManager(Game i_Game)
              : base(i_Game)
          {
               m_Barriers = new List<Barrier>(k_NumOfBarriers);
          }

          public override void Initialize()
          {
               base.Initialize();

               for (int i = 0; i < k_NumOfBarriers; i++)
               {
                    Barrier barrier = new Barrier(this.Game);
                    setBarrierProperties(barrier);
                    m_Barriers.Add(barrier);
               }

          }


          //Barrier firstBarrier = m_Barriers[0];
          //float firstToLastBarrierLen = (m_Barriers[k_NumOfBarriers - 1].Position.X + firstBarrier.Width) 
          //     - firstBarrier.Position.X;
          //m_InitPositionOfBarrier = ((float)Game.GraphicsDevice.Viewport.Width - firstToLastBarrierLen) / 2;


          private void setBarrierProperties(Barrier i_Barrier)
          {
               i_Barrier.GroupRepresentative = this;
               i_Barrier.Position = new Vector2(100, 100);
               i_Barrier.Visible = true;
          }

          public override void Update(GameTime gameTime)
          {
               base.Update(gameTime);


          }

          internal void pixelizedCollision() //infra
          {
          }
     }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class LivesManager : LoadableDrawableComponent
     {
          private readonly LinkedList<BasePlayer> r_Players = new LinkedList<BasePlayer>();
          private readonly Vector2 m_LivesScale = new Vector2(0.5f, 0.5f);
          private const int k_IncreaseLivesY = 32;
          private const int k_SpaceBetweenLives = 10;
          private const int k_LivesStartingY = 10;
          private const int k_LivesStartingX = 10;

          public LivesManager(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder) 
               : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
          {
          }

          public LivesManager(string i_AssetName, Game i_Game, int i_CallsOrder) 
               : base(i_AssetName, i_Game, i_CallsOrder)
          {
          }

          public LivesManager(Game i_Game, int i_CallsOrder)
               :base("", i_Game, i_CallsOrder)
          {
          }

          public LivesManager(Game i_Game)
               : base("", i_Game, int.MaxValue)
          {
          }

          public void AddPlayer(BasePlayer i_Player)
          {
               r_Players.AddLast(i_Player);
          }

          public void RemovePlayer(BasePlayer i_Player)
          {
               r_Players.Remove(i_Player);
          }

          public SpriteBatch SpriteBatch { get; private set; }

          public bool UseSharedSpriteBatch { get; private set; } = true;

          protected override void LoadContent()
          {
               if (SpriteBatch == null)
               {
                    SpriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                    if (SpriteBatch == null)
                    {
                         SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                         UseSharedSpriteBatch = false;
                    }
               }
           
               base.LoadContent();
          }

          public override void Draw(GameTime i_GameTime)
          {
               if (!UseSharedSpriteBatch)
               {
                    SpriteBatch.Begin();
               }

               float yPos = k_LivesStartingY;

               foreach (BasePlayer player in r_Players)
               {
                    for(int life = 1; life <= player.Lives; life++)
                    {
                         float xPos = this.Game.GraphicsDevice.Viewport.Width - (life * player.Width);
                         Vector2 positionForDraw = new Vector2(xPos, yPos);

                         SpriteBatch.Draw(player.Texture, positionForDraw, player.SourceRectangle, Color.White,
                              0, Vector2.Zero, m_LivesScale, SpriteEffects.None, player.LayerDepth);

                    }

                    float nextLivesPos = (player.Height * m_LivesScale.Y) + k_SpaceBetweenLives;
                    yPos += nextLivesPos;
               }

               if (!UseSharedSpriteBatch)
               {
                    SpriteBatch.End();
               }

               base.Draw(i_GameTime);
          }

          protected override void InitBounds()
          {
          }

          protected override void DrawBoundingBox()
          {
          }
     }
}

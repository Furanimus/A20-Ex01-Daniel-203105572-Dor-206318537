using A20_Ex01_Daniel_203105572_Dor_206318537.Models.BaseModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Managers
{
     public class LivesManager : LoadableDrawableComponent
     {
          public event Action AllPlayersDied;

          private readonly LinkedList<BasePlayer> r_Players = new LinkedList<BasePlayer>();
          private readonly Vector2 r_LivesScale = new Vector2(0.5f, 0.5f);
          private readonly Color r_Color = new Color(Color.White, k_LivesAlpha);
          private const int k_SpaceBetweenLives = 10;
          private const int k_LivesStartingY = 10;
          private const float k_LivesAlpha = 0.5f;
          private bool m_IsAllPlayerAlive;

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

          public override void Update(GameTime i_GameTime)
          {
               foreach(BasePlayer player in r_Players)
               {
                    if(player.IsAlive || player.Visible)
                    {
                         m_IsAllPlayerAlive = true;
                         break;
                    }
               }

               if(!m_IsAllPlayerAlive && AllPlayersDied != null)
               {
                    AllPlayersDied.Invoke();
               }
          
               m_IsAllPlayerAlive = false;

               base.Update(i_GameTime);
          }

          public override void Draw(GameTime i_GameTime)
          {
               if (!UseSharedSpriteBatch)
               {
                    SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
               }

               float yPos = k_LivesStartingY;

               foreach (BasePlayer player in r_Players)
               {
                    for(int life = 1; life <= player.Lives; life++)
                    {
                         float xPos = this.Game.GraphicsDevice.Viewport.Width - (life * player.Width);
                         Vector2 positionForDraw = new Vector2(xPos, yPos);

                         SpriteBatch.Draw(player.Texture, positionForDraw, player.SourceRectangle, r_Color,
                              0, Vector2.Zero, r_LivesScale, SpriteEffects.None, player.LayerDepth);

                    }

                    float nextLivesPos = (player.Height * r_LivesScale.Y) + k_SpaceBetweenLives;
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

using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
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
     public class ScoreManager : LoadableDrawableComponent
     {
          private readonly LinkedList<KeyValuePair<BasePlayer, Color>> r_Players = new LinkedList<KeyValuePair<BasePlayer, Color>>();
          private List<Color> m_ScoreColors;
          private const int k_SpaceFactor = 12;
          private const int k_SpaceBetweenScores = 10;
          private const int k_LivesStartingY = 10;
          private const int k_StartingXForDraw = 10;
          private SpriteFont m_ComicSansMsFont;

          public ScoreManager(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
               : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
          {
          }

          public ScoreManager(string i_AssetName, Game i_Game, int i_CallsOrder)
               : base(i_AssetName, i_Game, i_CallsOrder)
          {
          }

          public ScoreManager(Game i_Game, int i_CallsOrder)
               : base("", i_Game, i_CallsOrder)
          {
          }

          public ScoreManager(Game i_Game)
               : base("", i_Game, int.MaxValue)
          {
          }

          public void AddPlayer(BasePlayer i_Player, Color i_ScoreColor)
          {
               r_Players.AddLast(new KeyValuePair<BasePlayer, Color>(i_Player, i_ScoreColor));
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

               m_ComicSansMsFont = this.Game.Content.Load<SpriteFont>(@"Fonts\ComicSansMsFont");

               base.LoadContent();
          }

          public override void Draw(GameTime i_GameTime)
          {
               if (!UseSharedSpriteBatch)
               {
                    SpriteBatch.Begin();
               }

               float yPos = k_LivesStartingY;
               int playerCounter = 1;

               foreach (KeyValuePair<BasePlayer, Color> pair in r_Players)
               {

                    Vector2 positionForDraw = new Vector2(k_StartingXForDraw, yPos);
                    string scoreString = string.Format("P{0} Score: {1}", playerCounter.ToString(), pair.Key.Score.ToString());

                    SpriteBatch.DrawString(m_ComicSansMsFont, scoreString, positionForDraw, pair.Value);

                    float nextScorePos = k_SpaceFactor + k_SpaceBetweenScores;
                    yPos += nextScorePos;
                    playerCounter++;
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

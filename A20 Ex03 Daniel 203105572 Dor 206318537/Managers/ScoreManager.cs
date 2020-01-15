using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Components;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class ScoreManager : LoadableDrawableComponent, IScoreManager
     {
          private const int k_SpaceFactor                                        = 12;
          private const int k_SpaceBetweenScores                                 = 10;
          private const int k_LivesStartingY                                     = 10;
          private const int k_StartingXForDraw                                   = 10;
          private const string k_FontAssetName                                   = @"Fonts\ComicSansMsFont";
          private readonly LinkedList<KeyValuePair<BasePlayer, Color>> r_Players = new LinkedList<KeyValuePair<BasePlayer, Color>>();
          private readonly HashSet<BasePlayer> r_PlayersSetForCheckExistance     = new HashSet<BasePlayer>();
          private SpriteFont m_ComicSansMsFont;
          private const string k_ScoreString = "P{0} Score: {1}";

          public ScoreManager(Game i_Game)
               : base(string.Empty, i_Game, int.MaxValue)
          {
               this.Game.Services.AddService(typeof(IScoreManager), this);
          }

          public void AddPlayer(BasePlayer i_Player, Color i_ScoreColor)
          {
               if(!IsPlayerAlreadyAdded(i_Player))
               {
                    r_Players.AddLast(new KeyValuePair<BasePlayer, Color>(i_Player, i_ScoreColor));
                    r_PlayersSetForCheckExistance.Add(i_Player);
               }
          }

          public bool IsPlayerAlreadyAdded(BasePlayer i_Player)
          {
               return r_PlayersSetForCheckExistance.Contains(i_Player);
          }

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

               m_ComicSansMsFont = this.Game.Content.Load<SpriteFont>(k_FontAssetName);

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
                    string scoreString = string.Format(k_ScoreString, playerCounter.ToString(), pair.Key.Score.ToString());

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

          public SpriteBatch SpriteBatch { get; private set; }

          public bool UseSharedSpriteBatch { get; private set; } = true;
     }
}

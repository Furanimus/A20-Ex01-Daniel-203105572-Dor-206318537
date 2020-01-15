﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models;
using A20_Ex01_Daniel_203105572_Dor_206318537.Models;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class ScoreManager : DrawableGameComponent, IScoreManager
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
          private readonly GameScreen r_GameScreen;

          public ScoreManager(GameScreen i_GameScreen)
               : base(i_GameScreen.Game)
          {
               this.Game.Services.AddService(typeof(IScoreManager), this);
               r_GameScreen = i_GameScreen;
               r_GameScreen.Add(this);
               this.DrawOrder = this.UpdateOrder = int.MaxValue;
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
               base.LoadContent();

               m_ComicSansMsFont = this.Game.Content.Load<SpriteFont>(k_FontAssetName);
          }

          public override void Draw(GameTime i_GameTime)
          {
               float yPos = k_LivesStartingY;
               int playerCounter = 1;

               r_GameScreen.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

               foreach (KeyValuePair<BasePlayer, Color> pair in r_Players)
               {
                    Vector2 positionForDraw = new Vector2(k_StartingXForDraw, yPos);
                    string scoreString = string.Format(k_ScoreString, playerCounter.ToString(), pair.Key.Score.ToString());

                    r_GameScreen.SpriteBatch.DrawString(m_ComicSansMsFont, scoreString, positionForDraw, pair.Value);

                    float nextScorePos = k_SpaceFactor + k_SpaceBetweenScores;
                    yPos += nextScorePos;
                    playerCounter++;
               }

               r_GameScreen.SpriteBatch.End();

               base.Draw(i_GameTime);
          }
     }
}
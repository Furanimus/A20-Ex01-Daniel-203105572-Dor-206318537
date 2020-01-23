using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Models.BaseModels;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using A20_Ex03_Daniel_203105572_Dor_206318537.Components;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class ScoreManager : GameService, IScoreManager
     {
          private const int k_SpaceFactor = 12;
          private const int k_SpaceBetweenScores = 10;
          private const int k_LivesStartingY = 10;
          private const int k_StartingXForDraw = 10;
          private const string k_ScoreString = "P{0} Score: {1}";
          private const string k_FontAssetName = @"Fonts\ComicSansMsFont";
          private const string k_WinnerString = @"The Winner is player {0}";
          private readonly List<BasePlayer> r_Players = new List<BasePlayer>();
          private readonly HashSet<BasePlayer> r_PlayersSetForCheckExistance = new HashSet<BasePlayer>();
          private readonly List<GameScreen> r_GameScreens = new List<GameScreen>();
          private SpriteFont m_ComicSansMsFont;
          private IGameSettings m_GameSettings;
          private StrokeSpriteFont m_Result;
          private bool m_Initialize;

          public ScoreManager(Game i_Game)
               : base(i_Game)
          {
               this.Game.Services.AddService(typeof(IScoreManager), this);
               this.UpdateOrder = int.MaxValue;
          }

          public void AddScreen(GameScreen i_GameScreen)
          {
               r_GameScreens.Add(i_GameScreen);
               i_GameScreen.Add(this);
          }

          public void AddPlayer(BasePlayer i_Player)
          {
               if (!IsPlayerAlreadyAdded(i_Player))
               {
                    r_Players.Add(i_Player);
                    r_PlayersSetForCheckExistance.Add(i_Player);
               }
          }

          public void RemovePlayer(BasePlayer i_Player)
          {
               BasePlayer toRemove = null;
               int scoreIndex = 0;

               foreach (BasePlayer player in r_Players)
               {
                    if (player == i_Player)
                    {
                         scoreIndex++;
                         toRemove = player;
                         break;
                    }
               }

               if (toRemove != null)
               {
                    r_Players.Remove(toRemove);
                    r_PlayersSetForCheckExistance.Remove(toRemove);
               }
          }

          public bool IsPlayerAlreadyAdded(BasePlayer i_Player)
          {
               return r_PlayersSetForCheckExistance.Contains(i_Player);
          }

          public override void Initialize()
          {
               if (!m_Initialize)
               {
                    m_ComicSansMsFont = this.Game.Content.Load<SpriteFont>(k_FontAssetName);
                    m_GameSettings = this.Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;
                    base.Initialize();
                    m_Initialize = true;
               }
          }

          public void DrawScores(GameScreen i_GameScreen)
          {
               float yPos = k_LivesStartingY;
               int playerCounter = 1;

               i_GameScreen.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

               for (int i = 0; i < m_GameSettings.PlayersCount; i++)
               {
                    BasePlayer player = r_Players[i];
                    string scoreString = string.Format(k_ScoreString, playerCounter.ToString(), player.Score.ToString());
                    Vector2 scoresDrawPosition = new Vector2(k_StartingXForDraw, yPos);

                    i_GameScreen.SpriteBatch.DrawString(m_ComicSansMsFont, scoreString, scoresDrawPosition, player.RepresentativeColor);

                    float nextScorePos = k_SpaceFactor + k_SpaceBetweenScores;
                    yPos += nextScorePos;
                    playerCounter++;
               }

               i_GameScreen.SpriteBatch.End();
          }

          public Vector2 ResultPosition { get; set; }

          public void ShowResult(GameScreen i_GameScreen)
          {
               bool isSomeoneAlive = false;

               for (int i = 0; i < m_GameSettings.PlayersCount; i++)
               {
                    BasePlayer player = r_Players[i];

                    if (player.Visible || player.IsAlive)
                    {
                         isSomeoneAlive = true;
                         break;
                    }
               }

               if (!isSomeoneAlive && m_Result == null)
               {
                    StringBuilder result = new StringBuilder();
                    int winnerIdx = 0;
                    HashSet<int> ties = new HashSet<int>();

                    for (int i = 0; i < m_GameSettings.PlayersCount; i++)
                    {
                         BasePlayer player = r_Players[i];

                         if (r_Players[winnerIdx].Score < player.Score)
                         {
                              winnerIdx = i;

                              if(ties.Count > 0)
                              {
                                   ties.Clear();
                              }
                         }
                         else if (r_Players[winnerIdx].Score == player.Score)
                         {
                              ties.Add(i);
                              ties.Add(winnerIdx);
                         }

                         result.Append(string.Format(k_ScoreString, i + 1, player.Score));
                         result.Append(Environment.NewLine);
                    }

                    if(ties.Count > 0)
                    {
                         result.Append("It's a tie between:");
                         result.Append(Environment.NewLine);

                         foreach (int playerIndex in ties)
                         {
                              result.Append(string.Format("Player{0}  ", playerIndex + 1));
                         }
                    }
                    else if (m_GameSettings.PlayersCount > 1)
                    {
                         result.Append(string.Format(k_WinnerString, winnerIdx + 1));
                    }

                    m_Result = new StrokeSpriteFont(result.ToString(), i_GameScreen);
                    m_Result.Initialize();
                    m_Result.PositionOrigin = m_Result.SourceRectangleCenter;
                    m_Result.RotationOrigin = m_Result.SourceRectangleCenter;
                    m_Result.Position = ResultPosition;
                    i_GameScreen.Add(m_Result);
               }
          }
     }
}

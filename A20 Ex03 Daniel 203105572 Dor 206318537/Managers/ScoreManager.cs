using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers.BaseModels;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Managers
{
     public class ScoreManager : DrawableGameComponent, IScoreManager
     {
          private const int k_SpaceFactor = 12;
          private const int k_SpaceBetweenScores = 10;
          private const int k_LivesStartingY = 10;
          private const int k_StartingXForDraw = 10;
          private const string k_ScoreString = "P{0} Score: {1}";
          private const string k_FontAssetName = @"Fonts\ComicSansMsFont";
          private readonly List<BasePlayer> r_Players = new List<BasePlayer>();
          private readonly HashSet<BasePlayer> r_PlayersSetForCheckExistance = new HashSet<BasePlayer>();
          private readonly GameScreen r_GameScreen;
          private SpriteFont m_ComicSansMsFont;
          private IGameSettings m_GameSettings;

          public ScoreManager(GameScreen i_GameScreen)
               : base(i_GameScreen.Game)
          {
               this.Game.Services.AddService(typeof(IScoreManager), this);
               r_GameScreen = i_GameScreen;
               r_GameScreen.Add(this);
               this.DrawOrder = this.UpdateOrder = int.MaxValue;
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

               foreach (BasePlayer player in r_Players)
               {
                    if (player == i_Player)
                    {
                         toRemove = player;
                         break;
                    }
               }

               if (toRemove != null)
               { 
                    r_Players.Remove(toRemove);
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

          public override void Initialize()
          {
               m_GameSettings = this.Game.Services.GetService(typeof(IGameSettings)) as IGameSettings;
               base.Initialize();
          }

          public override void Draw(GameTime i_GameTime)
          {
               float yPos = k_LivesStartingY;
               int playerCounter = 1;

               r_GameScreen.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

               for (int i = 0; i < m_GameSettings.PlayersCount; i++)
               {
                    BasePlayer player = r_Players[i];
                    Vector2 positionForDraw = new Vector2(k_StartingXForDraw, yPos);
                    string scoreString = string.Format(k_ScoreString, playerCounter.ToString(), player.Score.ToString());

                    r_GameScreen.SpriteBatch.DrawString(m_ComicSansMsFont, scoreString, positionForDraw, player.RepresentativeColor);

                    float nextScorePos = k_SpaceFactor + k_SpaceBetweenScores;
                    yPos += nextScorePos;
                    playerCounter++;
               }

               r_GameScreen.SpriteBatch.End();

               base.Draw(i_GameTime);
          }
     }
}

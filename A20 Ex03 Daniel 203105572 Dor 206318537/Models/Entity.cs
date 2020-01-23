using System;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Entity : Sprite, ISoundEmitter
     {
          public event Action<string> ActionOccurred;

          private const string k_KillSoundName = @"EnemyKill";
          private int m_LivesForReset = -1;
          protected int m_Lives;

          public Entity(string i_AssetName, GameScreen i_GameScreen)
               : this(i_AssetName, i_GameScreen, int.MaxValue)
          {
          }

          public Entity(string i_AssetName, GameScreen i_GameScreen, int i_CallsOrder)
               : this(i_AssetName, i_GameScreen, i_CallsOrder, i_CallsOrder)
          {
          }

          public Entity(string i_AssetName, GameScreen i_GameScreen, int i_UpdateOrder, int i_DrawOrder)
               : base(i_AssetName, i_GameScreen, i_UpdateOrder, i_DrawOrder)
          {
          }

          public string KilledSoundName { get; set; } = k_KillSoundName;

          public int Lives
          {
               get
               {
                    return m_Lives;
               }

               set
               {
                    if (value >= 0)
                    {
                         m_Lives = value;

                         if (m_LivesForReset < 0)
                         {
                              m_LivesForReset = value;
                         }

                         if (value != 0 && LifeLostSoundName != string.Empty && ActionOccurred != null)
                         {
                              ActionOccurred.Invoke(LifeLostSoundName);
                         }
                    }

                    if (m_Lives == 0)
                    {
                         if (ActionOccurred != null)
                         {
                              ActionOccurred.Invoke(KilledSoundName);
                         }

                         IsAlive = false;
                    }
                    else if (m_Lives > 0)
                    {
                         IsAlive = true;
                    }
               }
          }

          public string LifeLostSoundName { get; set; } = k_KillSoundName;

          public override void ResetProperties()
          {
               base.ResetProperties();
               this.Lives = m_LivesForReset;
          }

          public bool IsAlive { get; protected set; } = true;
     }
}

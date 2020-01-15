using A20_Ex01_Daniel_203105572_Dor_206318537.Models;
using Microsoft.Xna.Framework;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Entity : Sprite
     {
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
                    }

                    if(m_Lives == 0)
                    {
                         IsAlive = false;
                    }
                    else if(m_Lives > 0)
                    {
                         IsAlive = true;
                    }
               }
          }
          
          public bool IsAlive { get; protected set; } = true;
     }
}

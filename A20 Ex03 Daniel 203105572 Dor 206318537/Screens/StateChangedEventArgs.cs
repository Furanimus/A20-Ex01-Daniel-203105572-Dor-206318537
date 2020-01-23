using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Screens
{
     public class StateChangedEventArgs : EventArgs
     {
          protected eScreenState m_PrevState;

          public eScreenState PrevState
          {
               get { return m_PrevState; }
               set { m_PrevState = value; }
          }

          protected eScreenState m_CurrentState;

          public eScreenState CurrentState
          {
               get { return m_CurrentState; }
               set { m_CurrentState = value; }
          }

          public StateChangedEventArgs()
          {
          }

          public StateChangedEventArgs(eScreenState i_PrevState, eScreenState i_CurrState)
          {
               m_PrevState = i_PrevState;
               m_CurrentState = i_CurrState;
          }
     }
}

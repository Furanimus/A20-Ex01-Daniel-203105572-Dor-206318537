using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public abstract class Enemy: BasicEntity
     {
        protected Random m_Random;
          
        public static eDirection Direction { get; set; } = eDirection.Right;

          public Enemy()
          {
               Velocity = 50;
               Height = 32;
               Width = 32;
               Lives = 1;
               m_Random = new Random();
          }

          public int Score { get; set; }

          public override void Move()
          {
               if(Position.X + Width < GameEnvironment.WindowWidth && Direction == eDirection.Right || 
                    Position.X - Width > 0 && Direction == eDirection.Left || Direction == eDirection.Down)
               {
                    if (Direction == eDirection.Right)
                    {
                         m_Position.X += Velocity / 2;
                    }
                    else if (Direction == eDirection.Left)
                    {
                         m_Position.X -= Velocity / 2;
                    }
                    else if (Direction == eDirection.Down)
                    {
                         m_Position.Y += this.Height / 2;
                    }
               }
          }
     }
}

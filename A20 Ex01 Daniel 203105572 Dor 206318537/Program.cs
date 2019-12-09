using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
#if WINDOWS || LINUX
     /// <summary>
     /// The main class.
     /// </summary>
     public static class Program
     {
          public static object Game { get; internal set; }

          /// <summary>
          /// The main entry point for the application.
          /// </summary>
          [STAThread]
          public static void Main()
          {
               using (var game = new SpaceInvadersGame())
               {
                    game.Run();
               }
          }
     }
#endif
}

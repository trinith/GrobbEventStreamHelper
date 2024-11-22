using System;

namespace GrobbEventStreamHelper
{
#if  WINDOWSDX || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GrobbEventStreamHelperGame())
                game.Run();
        }
    }
#endif
}

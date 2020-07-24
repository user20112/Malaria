using System;

namespace Malaria2
{
#if WINDOWS || LINUX

    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (Malaria2 game = new Malaria2())
                game.Run();
        }
    }

#endif
}
using System;

namespace GalaxyRim
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GalaxyRim game = new GalaxyRim())
            {
                game.Run();
            }
        }
    }
#endif
}


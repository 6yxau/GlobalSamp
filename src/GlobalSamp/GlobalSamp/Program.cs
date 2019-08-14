using System;
using SampSharp.Core;

namespace GlobalSamp
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeGameMode();
        }

        private static void InitializeGameMode()
        {
            GameModeBuilder builder = new GameModeBuilder();
            builder.Use<GameMode>();
            builder.Run();
        }
    }
}

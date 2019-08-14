using System;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;

namespace GlobalSamp
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        protected override void OnInitialized(EventArgs e)
        {
            Console.WriteLine("\n----------------------------------");
            Console.WriteLine(" Blank Gamemode by your name here");
            Console.WriteLine("----------------------------------\n");

            /*
             * TODO: Do your initialisation and loading of data here.
             */
            base.OnInitialized(e);
        }

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            /*
             * TODO: Load or unload controllers here.
             */
        }

        #endregion
    }
}
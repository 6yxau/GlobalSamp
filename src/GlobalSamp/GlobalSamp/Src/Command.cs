using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace GlobalSamp
{
    [CommandGroup("admin")]
    class AnyClass
    {
        [Command("vehicle", "veh", "v")]
        private static void SpawnVehicleCommand(BasePlayer sender, VehicleModelType model) { }
    }
}
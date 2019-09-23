using SampSharp.GameMode;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace GlobalSamp.Commands
{
    [CommandGroup("a")]
    public class Commander
    {
        [Command("skin")]
        private static void SetSkin(BasePlayer sender, int skinId)
        {
            sender.Skin = skinId;
        }

        [Command("wrd")]
        private static void SetInt(BasePlayer sender, int intId)
        {
            sender.VirtualWorld = intId;
            sender.Spawn();
        }
        
        [Command("pos")]
        private static void SetPos(BasePlayer sender, float x, float y, float z)
        {
            sender.Position = new Vector3(x, y, z);
        }

        [Command("weather")]
        private static void SetWeather(BasePlayer sender, int id)
        {
            sender.SetWeather(id);
        }
    }
}
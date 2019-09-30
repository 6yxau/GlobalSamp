using System.Collections.Generic;
using GlobalSamp.Player;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace GlobalSamp.Commands
{
    [CommandGroup("a")]
    public class Commander
    {

        private static readonly Dictionary<int, Vector3> _intPos = new Dictionary<int, Vector3>()
        {
        };
        
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
        
        [Command("int")]

        private static void Interioir(BasePlayer sender, int id)
        {
            sender.Interior = id;
            if (!_intPos.TryGetValue(id, out Vector3 pos))
            {
                
            }
            
        }
        
        [Command("pos")]
        private static void SetPos(BasePlayer sender, float x, float y, float z)
        {
            sender.Position = new Vector3(x, y, z);
        }

        [Command("weather")]
        private static void SetWeather(BasePlayer sender, int targetPlayer, int id)
        {
            BasePlayer target = BasePlayer.Find(targetPlayer);
            target?.SetWeather(id);
        }

        [Command("tp")]
        private static void Teleport(BasePlayer sender, int targetId)
        {
            BasePlayer target = BasePlayer.Find(targetId);
            if (target == null)
            {
                return;
            }

            target.VirtualWorld = sender.VirtualWorld;
            target.Interior = sender.Interior;
            target.Position = sender.Position + new Vector3(1, 1, 1);

        }
        
    }
}
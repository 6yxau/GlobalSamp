using SampSharp.GameMode;
using SampSharp.GameMode.World;

namespace GlobalSamp.Mapping.Descriptor
{
    public struct MapRemover
    {
        public int ModelId { get; set; }
        public Vector3 Position { get; set; }
        public float Radius { get; set; }

        public void Remove(BasePlayer player)
        { 
            GlobalObject.Remove(player, ModelId, Position, Radius);
        }
    }
}
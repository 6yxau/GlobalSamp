using System;

namespace GlobalSamp.Player
{
    public class PlayerData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password{ get; set; }
        public PlayerGender Gender { get; set; }
        public string Email { get; set; }
        public long date { get; set; }
        public PlayerColor SkinColor { get; set; }
        public int Age { get; set; }

        public bool Authorized { get; set; } = false;
    }
}
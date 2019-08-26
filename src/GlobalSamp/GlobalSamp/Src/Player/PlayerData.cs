using System;

namespace GlobalSamp.Player
{
    public struct PlayerData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password{ get; set; }
        public PlayerGender Gender { get; set; }
        public string Email { get; set; }
        public long date { get; set; }

        public bool Equals(PlayerData rhs)
        {
            return Id == rhs.Id && UserName.Equals(rhs.UserName) && Password.Equals(rhs.Password) &&
                   (int) Gender == (int) rhs.Gender && Email.Equals(rhs.Email) && date == rhs.date;
        }
    }
}
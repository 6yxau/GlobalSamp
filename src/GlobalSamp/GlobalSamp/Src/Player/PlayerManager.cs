using System.Collections.Generic;
using GlobalSamp.Player.Dao;
using GlobalSamp.Tools.Common;

namespace GlobalSamp.Player
{
    public sealed class PlayerManager : Singleton<PlayerManager>
    {
        private readonly PlayerDao _dao;
        
        private readonly Dictionary<string, PlayerData> _players = new Dictionary<string, PlayerData>(1000);
        
        private readonly Dictionary<string, PlayerData> _cachedData = new Dictionary<string, PlayerData>(200);
        
        public PlayerManager()
        {
            _dao = new PlayerDao();
        }

        public PlayerData GetPlayerData(string name)
        {
            if (_players.ContainsKey(name))
            {
                return _players[name];
            }
            PlayerData data = _dao.GetPlayerModel(name);
            if (data == null)
            {
                return null;
            }
            _players.Add(name, data);
            return data;
        }

        public void CachePlayerData(PlayerData data)
        {
            _cachedData.Add(data.UserName, data);
        }

        public PlayerData GetCachedPlayerData(string name)
        {
            _cachedData.TryGetValue(name, out PlayerData data);
            return data;
        }

        public bool RemovePlayerDataFromCache(string name)
        {
            return _cachedData.Remove(name);
        }

        public bool RemovePlayerData(string name)
        {
            _cachedData.Remove(name);
            return _players.Remove(name);
        }

        public void AddPlayer(PlayerData player)
        {
            _dao.AddPlayer(player);
        }
        
    }
}
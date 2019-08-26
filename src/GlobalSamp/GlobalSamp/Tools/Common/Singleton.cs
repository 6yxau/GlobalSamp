namespace GlobalSamp.Tools.Common
{
    public class Singleton<T> where T : class, new()
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object _sync = new object();

        private static T _instance;

        public static T Instance
        {
            get
            {
                lock (_sync)
                {
                    return _instance ?? (_instance = new T());
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BetAppServer.Tools.Serialization.XML;
using GlobalSamp.Tools.Common;

namespace GlobalSamp.Application.Translator
{
    public sealed class Translator : Singleton<Translator>
    {
        private readonly Dictionary<string, string> _dict = new Dictionary<string, string>(1000);
        
        public void Configure(XmlNode config)
        {
            // ReSharper disable once TooWideLocalVariableScope
            XmlNode translation;
            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once TooWideLocalVariableScope
            string translationKey;
            for (int i = 0; i < config.Children.Count; i++)
            {
                translation = config.Children[i];
                if (translation.Name != "message")
                {
                    continue;
                }

                translationKey = translation.GetString("key");
                if (translationKey == null)
                {
                    continue;
                }
                _dict.Add(translationKey, translation.Value);
            }
        }

        public string GetMessage(string key)
        {
            return _dict[key];
        }
    }
}
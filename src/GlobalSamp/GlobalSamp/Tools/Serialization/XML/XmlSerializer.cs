/* Author: WilliamSXE */

using System.IO;
using BetAppServer.Tools.Serialization.XML.Exception;
using BetAppServer.Tools.Serialization.XML.Utils;

namespace BetAppServer.Tools.Serialization.XML
{
    public sealed class XmlSerializer
    {
        public XmlNode Deserialize(string path)
        {
            string source = File.ReadAllText(path);
            if (string.IsNullOrEmpty(source))
            {
                throw new XmlEmptyFileException();
            }

            int index = 0;
            while (true) {
                XmlUtils.SkipWhitespaceRef(source, ref index);
                if (source[index] != '<') {
                    throw new XmlParseException("Invalid token");
                }
                index++;
                if (source[index] == '?' || source[index] == '!') {
                    while (source[index] != '>') {
                        if (source[index] == '[') {
                            while (source[index] != ']') {
                                index++;
                            }
                        }
                        index++;
                    }
                    index++;
                    continue;
                }
                return XmlNode.Get(source, ref index);
            }
        }
    }
}
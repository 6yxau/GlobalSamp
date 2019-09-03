/* Author: WilliamSXE */

using System.Collections.Generic;
using BetAppServer.Tools.Serialization.XML.Exception;

namespace BetAppServer.Tools.Serialization.XML.Utils
{
    internal static class XmlUtils
    {
        private const char CHAR_TABULATION = '\t';
        private const char LINE_FEED = '\n';
        private const char CARRIAGE_RETURN = '\r';
        private const char SPACE = ' ';

        public static void SkipWhitespaceRef(string source, ref int index)
        {
            while (index < source.Length)
            {
                if (!(source[index] == SPACE || source[index] == CHAR_TABULATION || source[index] == LINE_FEED ||
                      source[index] == CARRIAGE_RETURN))
                {
                    if (source[index] == '<' && index + 4 < source.Length && source[index + 1] == '!' &&
                        source[index + 2] == '-' && source[index + 3] == '-')
                    {
                        index += 4;
                        while (index + 2 < source.Length && !(source[index] == '-' && source[index + 1] == '-'))
                        {
                            index++;
                        }

                        index += 2;
                    }
                    else
                    {
                        break;
                    }
                }

                index++;
            }
        }

        public static int SkipValueRef(string source, ref int index, char endChar, char nextEndChar, bool skipOnSpace)
        {
            int start = index;
            while (
                (!skipOnSpace || !(source[index] == SPACE || source[index] == CHAR_TABULATION ||
                                   source[index] == LINE_FEED || source[index] == CARRIAGE_RETURN)) &&
                source[index] != endChar && source[index] != nextEndChar)
            {
                index++;
            }

            return index - start;
        }

        public static string GetValueRef(string source, ref int index, char endChar, char nextEndChar, bool skinOnSpace)
        {
            int start = index;
            while (
                (!skinOnSpace || !(source[index] == SPACE || source[index] == CHAR_TABULATION ||
                                   source[index] == LINE_FEED || source[index] == CARRIAGE_RETURN)) &&
                source[index] != endChar && source[index] != nextEndChar)
            {
                index++;
            }

            return source.Substring(start, index - start);
        }

        public static void ParseAttributes(string source, ref int index, List<XmlAttribute> attributes, char endChar,
            char nextEndChar)
        {
            attributes.Clear();
            SkipWhitespaceRef(source, ref index);
            while (source[index] != endChar && source[index] != nextEndChar)
            {
                XmlAttribute attribute = new XmlAttribute
                {
                    Name = GetValueRef(source, ref index, '=', '\0', true)
                };
                SkipWhitespaceRef(source, ref index);
                index++;
                SkipWhitespaceRef(source, ref index);
                char quote = source[index];
                if (quote != '"' && quote != '\'')
                {
                    throw new XmlUnexpectedTokenException(attribute.Name, index);
                }

                index++;
                attribute.ValueStart = index;
                attribute.ValueLength = SkipValueRef(source, ref index, quote, '\0', false);
                index++;
                attributes.Add(attribute);
                SkipWhitespaceRef(source, ref index);
            }
        }
    }
}
/* Author: WilliamSXE */

using System;
using System.Collections.Generic;
using BetAppServer.Tools.Serialization.XML.Exception;
using BetAppServer.Tools.Serialization.XML.Utils;
using BetAppServer.Tools.Utils;

namespace BetAppServer.Tools.Serialization.XML
{
    public sealed class XmlNode
    {
        private static readonly List<XmlNode> _recyclePool = new List<XmlNode>(64);

        public string Name { get; private set; }
        public string Value => _valueLength > 0 ? _source.Substring(_valueStart, _valueLength) : null;

        public readonly List<XmlNode> Children = new List<XmlNode>();

        private readonly List<XmlAttribute> _attributes = new List<XmlAttribute>(2);
        private string _source;
        private int _valueStart;
        private int _valueLength;

        public static XmlNode Get(string source, ref int offset)
        {
            XmlNode result;
            if (_recyclePool.Count > 0)
            {
                result = _recyclePool[_recyclePool.Count - 1];
                _recyclePool.RemoveAt(_recyclePool.Count - 1);
            }
            else
            {
                result = new XmlNode();
            }

            result.Initialize(source, ref offset);
            return result;
        }

        private void Initialize(string source, ref int index)
        {
            _source = source;
            XmlUtils.SkipWhitespaceRef(source, ref index);
            Name = XmlUtils.GetValueRef(source, ref index, '>', '/', true);
            XmlUtils.ParseAttributes(source, ref index, _attributes, '>', '/');
            if (source[index] == '/')
            {
                index += 2;
                return;
            }

            index++;
            int temporaryIndex = index;
            XmlUtils.SkipWhitespaceRef(source, ref temporaryIndex);
            if (source[temporaryIndex] == '<')
            {
                index = temporaryIndex;
                while (source[index + 1] != '/')
                {
                    index++;
                    Children.Add(Get(source, ref index));
                    XmlUtils.SkipWhitespaceRef(source, ref index);
                    if (index >= source.Length)
                    {
                        return;
                    }

                    if (source[index] != '<')
                    {
                        throw new XmlUnexpectedTokenException(source[index].ToString(), index);
                    }
                }

                index++;
            }
            else
            {
                _valueStart = index;
                _valueLength = XmlUtils.SkipValueRef(source, ref index, '<', '\0', false);
                index++;
                if (source[index] != '/')
                {
                    throw new XmlParseException($"Invalid ending tag at {index.ToString()}");
                }
            }

            index++;
            XmlUtils.SkipWhitespaceRef(source, ref index);
            if (XmlUtils.GetValueRef(source, ref index, '>', '\0', true) != Name)
            {
                throw new XmlParseException($"Start or end tag name mismatch at {index.ToString()}");
            }

            XmlUtils.SkipWhitespaceRef(source, ref index);
            if (source[index] != '>')
            {
                throw new XmlParseException($"Invalid ending tag at {index.ToString()}");
            }

            index++;
        }

        public void Recycle()
        {
            List<XmlNode> stackCache = Children;
            for (int i = stackCache.Count - 1; i >= 0; i--)
            {
                stackCache[i].Recycle();
            }
            Children.Clear();
            _source = null;
            _recyclePool.Add(this);
        }

        public XmlNode GetChild(string name)
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                if (Children[i].Name == name)
                {
                    return Children[i];
                }
            }

            return null;
        }

        public short GetShort(string name, short @default = 0)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }
            if (!short.TryParse(raw, out short result))
            {
                throw new XmlParseException($"Can not parse attribute \"{name}\" as short integer");
            }

            return result;
        }

        public int GetInt(string name, int @default = 0)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }
            if (!int.TryParse(GetAttributeValue(name), out int result))
            {
                throw new XmlParseException($"Can not parse attribute \"{name}\" as integer");
            }
            return result;
        }

        public long GetLong(string name, long @default = 0)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }
            if (!long.TryParse(GetAttributeValue(name), out long result))
            {
                throw new XmlParseException($"Can not parse attribute \"{name}\" as long integer");
            }

            return result;
        }

        public T GetObject<T>(string name) where T : IConvertible
        {
            return (T) Convert.ChangeType(GetAttributeValue(name), typeof(T));
        }

        public string GetString(string name, string @default = null)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }

            return raw;
        }

        public byte GetByte(string name, byte @default = 0)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }

            if (!byte.TryParse(raw, out byte result))
            {
                throw new XmlParseException($"Can not parse attribute \"{name}\" as byte");
            }

            return result;
        }

        public byte[] GetByteArray(string name, byte[] @default = null)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }

            return StringUtils.DetermineEncoding(raw).GetBytes(raw);
        }

        public bool GetBool(string name, bool @default = false)
        {
            string raw = GetAttributeValue(name);
            if (raw == null)
            {
                return @default;
            }

            return Convert.ToBoolean(raw);
        }

        private string GetAttributeValue(string name)
        {
            for (int i = _attributes.Count - 1; i >= 0; i--)
            {
                if (string.CompareOrdinal(_attributes[i].Name, name) != 0) continue;
                XmlAttribute attribute = _attributes[i];
                return attribute.ValueLength > 0
                    ? _source.Substring(attribute.ValueStart, attribute.ValueLength)
                    : null;
            }

            return null;
        }
    }
}
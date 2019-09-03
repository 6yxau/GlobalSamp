/* Author: WilliamSXE */

namespace BetAppServer.Tools.Serialization.XML.Exception
{
    public class XmlUnexpectedTokenException : System.Exception
    {
        public XmlUnexpectedTokenException(string token, int line) : base(
            $"Unexpected token after {token} at line {line.ToString()}") { }
    }
}
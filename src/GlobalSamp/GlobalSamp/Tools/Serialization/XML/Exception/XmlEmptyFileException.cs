/* Author: WilliamSXE */

namespace BetAppServer.Tools.Serialization.XML.Exception
{
    public class XmlEmptyFileException : System.Exception
    {
        public XmlEmptyFileException() : base("XML file is empty") { }
    }
}
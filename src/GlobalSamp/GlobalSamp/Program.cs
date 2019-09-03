using System;
using System.Diagnostics;
using System.IO;
using BetAppServer.Tools.Serialization.XML;
using GlobalSamp.Application.Translator;
using SampSharp.Core;

namespace GlobalSamp
{
    class Program
    {

        private static Process _nativeProcessHandle = null;
        static void Main(string[] args)
        {
            CreateNativeProcess();
            
            ConfigureTranslator();
            
            InitializeGameMode();
        }

        private static void InitializeGameMode()
        {
            try
            {
                GameModeBuilder builder = new GameModeBuilder();
                builder.Use<GameMode>();
                builder.UseEncoding(Path.GetFullPath("../../../../../../env/codepages/cp1251.txt"));
                builder.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
         
        }

        private static void CreateNativeProcess()
        {
            _nativeProcessHandle = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Path.GetFullPath("../../../../../../env/"),
                    FileName = Path.GetFullPath("../../../../../../env/samp-server.exe"),
                    CreateNoWindow = false,
                    UseShellExecute = true,
                }
            };
            _nativeProcessHandle.Start();
            _nativeProcessHandle.StartInfo.RedirectStandardError = true;
            _nativeProcessHandle.StartInfo.RedirectStandardInput = true;
            _nativeProcessHandle.StartInfo.RedirectStandardOutput = true;
        }

        private static void ConfigureTranslator()
        {
            XmlSerializer serializer = new XmlSerializer();
            
            Translator.Instance.Configure(serializer.Deserialize("../../../Config/Translations/translation_ru.xml"));
        }
    }
}

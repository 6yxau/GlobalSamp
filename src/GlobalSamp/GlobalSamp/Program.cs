using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using BetAppServer.Tools.Serialization.XML;
using GlobalSamp.Application.Translator;
using GlobalSamp.Mapping;
using SampSharp.Core;

namespace GlobalSamp
{
    class Program
    {
        private static Process _nativeProcessHandle;
        
        public static readonly XmlSerializer Serializer = new XmlSerializer();
        
        static void Main(string[] args)
        {
            ConfigureManagement();
            CreateNativeProcess();
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

        private static void ConfigureManagement()
        {

            Translator.Instance.Configure(Serializer.Deserialize("../../../Config/Translations/translation_ru.xml"));

            XmlNode mapping = Serializer.Deserialize("../../../Config/Mapping/mapping.xml");
            XmlNode[] removablePaths = mapping.GetChild("removers").Children.ToArray();
            List<XmlNode> removableMappingConfigs = new List<XmlNode>(1000);
            foreach (XmlNode removablePath in removablePaths)
            {
                XmlNode rootRemovable =
                    Serializer.Deserialize($"../../../Config/Mapping/{removablePath.GetString("src")}");
                removableMappingConfigs.AddRange(rootRemovable.Children);
            }
            MapManager.Instance.ConfigureRemovableMapping(removableMappingConfigs);
        }
    }
}

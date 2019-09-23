using System;
using System.Collections.Generic;
using System.Linq;
using BetAppServer.Tools.Serialization.XML;
using GlobalSamp.Mapping.Descriptor;
using GlobalSamp.Tools.Common;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.Streamer.World;

namespace GlobalSamp.Mapping
{
    // ReSharper disable TooWideLocalVariableScope
    public sealed class MapManager : Singleton<MapManager>
    {
        private readonly List<MapRemover> _removableMapping = new List<MapRemover>(1000);

        public void ConfigureRemovableMapping(List<XmlNode> configs)
        {
            
            XmlNode @object;
            XmlNode position;
            for (int i = configs.Count - 1; i >= 0; i--)
            {
                @object = configs[i];
                position = @object.GetChild("position");
                if (position == null)
                {
                    throw new Exception("Wrong removable structure");
                }
                _removableMapping.Add(new MapRemover
                {
                    ModelId = @object.GetInt("id"),
                    Position = new Vector3(position.GetFloat("x"), position.GetFloat("y"), position.GetFloat("z")),
                    Radius = position.GetFloat("radius")
                });
            }
        }
        
        public void RemoveMappingForPlayer(BasePlayer player)
        {
            for (int i = _removableMapping.Count - 1; i >= 0; i--)
            {
                _removableMapping[i].Remove(player);
            }
        }

        public void ApplyAppenders()
        {
            XmlNode[] mapAppenders = Program.Serializer.Deserialize("../../../Config/Mapping/mapping.xml")
                .GetChild("appenders").Children.ToArray();
            
            List<XmlNode> appenderConfigs = new List<XmlNode>(10000); // main collection
            foreach (XmlNode mapAppender in mapAppenders)
            {
                XmlNode rootAppenderNode =
                    Program.Serializer.Deserialize($"../../../Config/Mapping/{mapAppender.GetString("src")}");
                appenderConfigs.AddRange(rootAppenderNode.Children);
            }

            XmlNode appender;
            for (int i = appenderConfigs.Count - 1; i >= 0; i--)
            {
                appender = appenderConfigs[i];
                int id = appender.GetInt("id");
                bool @static = appender.GetString("type") == "static";
                XmlNode positionConfig = appender.GetChild("position");
                if (positionConfig == null)
                {
                    throw new Exception($"Position is not specified at object index {i.ToString()}");
                }
                Vector3 position = new Vector3(positionConfig.GetFloat("x"), positionConfig.GetFloat("y"), positionConfig.GetFloat("z"));
                XmlNode rotationConfig = appender.GetChild("rotation");
                if (rotationConfig == null)
                {
                    throw new Exception($"Rotation is not specified at object index {i.ToString()}");
                }
                Vector3 rotation = new Vector3(rotationConfig.GetFloat("x"), rotationConfig.GetFloat("y"), rotationConfig.GetFloat("z"));
                XmlNode props = appender.GetChild("props");
                if (props == null)
                {
                    throw new Exception($"Properties are not specified at object index {i.ToString()}");
                }

                int worldId = props.GetInt("worldId");
                int interiorId = props.GetInt("interiorId");
                int playerId = props.GetInt("playerId");
                float streamDistance = props.GetFloat("streamDistance");
                float drawDistance = props.GetFloat("drawDistance");
                int areaId = props.GetInt("areaId");
                int priority = props.GetInt("priority");

                IGameObject obj;
                if (!@static)
                {
                    obj = new DynamicObject(id, position, rotation, worldId, interiorId, playerId == -1 ? null : BasePlayer.Find(playerId), streamDistance, drawDistance, null, priority);   
                }
                else
                {
                    obj = new GlobalObject(id, position, rotation, drawDistance);
                }
                XmlNode materialsConfig = appender.GetChild("materials");
                if (materialsConfig?.Children == null || materialsConfig.Children.Count <= 0)
                {
                    continue;
                }

                XmlNode materialConfig;
                for (int j = 0; j < materialsConfig.Children.Count; j++)
                {
                    materialConfig = materialsConfig.Children[j];

                    string type = materialConfig?.GetString("type");
                    if (type == null)
                    {
                        continue;
                    }

                    if (type == "object")
                    {
                        int index = materialConfig.GetInt("index");
                        int modelId = materialConfig.GetInt("modelId");
                        string txd = materialConfig.GetString("txd");
                        string texture = materialConfig.GetString("texture");
                        uint color = materialConfig.GetUInt("color");
                        obj.SetMaterial(index, modelId, txd, texture, Color.FromInteger(color, ColorFormat.RGBA));
                    } 
                    else if (type == "text")
                    {
                        int index = materialConfig.GetInt("index");
                        string text = materialConfig.GetString("text");
                        int size = materialConfig.GetInt("size");
                        string font = materialConfig.GetString("font");
                        int fontSize = materialConfig.GetInt("fontSize");
                        bool bold = materialConfig.GetInt("bold") == 1;
                        uint fontColor = materialConfig.GetUInt("fontColor");
                        uint backColor = materialConfig.GetUInt("backColor");
                        int textAlignment = materialConfig.GetInt("textAligment");
                        obj.SetMaterialText(index, text, (ObjectMaterialSize) size, font, fontSize, bold, Color.FromInteger(fontColor, ColorFormat.RGBA), Color.FromInteger(backColor, ColorFormat.RGBA), (ObjectMaterialTextAlign) textAlignment);
                    }
                    
                }
            }
        }
    }
}
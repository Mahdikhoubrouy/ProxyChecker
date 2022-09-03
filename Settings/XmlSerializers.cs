using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProxyChecker.Settings
{
    public class XmlSerializers
    {
        readonly string path = Path.Combine(Environment.CurrentDirectory, "Settings");
        readonly string pathFile = Path.Combine(Environment.CurrentDirectory, "Settings", "Settings.xml");
        public void SettingSerializer(XmlSerializerSettingModel SettingObject)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(pathFile))
            {
                var serializer = new XmlSerializer(typeof(XmlSerializerSettingModel));
                using (var writer = new StreamWriter(pathFile))
                {
                    serializer.Serialize(writer, SettingObject);
                }
            }
        }


        public void UpdateSetting(XmlSerializerSettingModel SettingObject)
        {
            var ObjectSet = SettingDeserializer();

            if (SettingObject.BotThread > 0)
            {
                ObjectSet.BotThread = SettingObject.BotThread;
            }
            else if (SettingObject.TimeOut > 0)
            {
                ObjectSet.TimeOut = SettingObject.TimeOut;
            }

            var serializer = new XmlSerializer(typeof(XmlSerializerSettingModel));

            using (var writer = new StreamWriter(pathFile))
            {
                serializer.Serialize(writer, ObjectSet);
            }

        }
        public XmlSerializerSettingModel SettingDeserializer()
        {
            var Deserializer = new XmlSerializer(typeof(XmlSerializerSettingModel));

            FileStream fs = new FileStream(pathFile, FileMode.Open);
            TextReader reader = new StreamReader(fs);

            var res = (XmlSerializerSettingModel)Deserializer.Deserialize(reader);

            fs.Close();
            reader.Close();

            return res;
        }

    }
}

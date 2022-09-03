using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyChecker.Settings
{
    public class SettingFileConfigure
    {
        public static void CreateSettingFile()
        {
            new XmlSerializers().SettingSerializer(new XmlSerializerSettingModel() { BotThread = 15 , TimeOut = 5 });
        }
        public static int GetBotThread()
        {
            var xmlDeser = new XmlSerializers();

            return xmlDeser.SettingDeserializer().BotThread;
        }

        public static int GetTimeOut()
        {
            return new XmlSerializers().SettingDeserializer().TimeOut;
        }
        public static void SetTimeOut(int timeout)
        {
            new XmlSerializers().UpdateSetting(new XmlSerializerSettingModel() { TimeOut = timeout });
        }
        public static void SetBotThread(int botThread)
        {
            new XmlSerializers().UpdateSetting(new XmlSerializerSettingModel() { BotThread = botThread });
        }

    }
}

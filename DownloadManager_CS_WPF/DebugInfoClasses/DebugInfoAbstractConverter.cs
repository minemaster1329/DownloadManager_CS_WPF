using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace DownloadManager_CS_WPF.DebugInfoClasses
{
    class DebugInfoAbstractConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(DebugInfoAbstract).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Debug.WriteLine(reader.TokenType.ToString());
            JObject jo = JObject.Load(reader);

            DebugInfoType? type = jo["DebugItemType"].ToObject<DebugInfoType?>();

            DebugInfoAbstract debugInfoItem;

            switch (type.GetValueOrDefault())
            {
                case DebugInfoType.DownloadCompleted:
                    debugInfoItem = new DebugDownloadCompletedSuccesfully();
                    break;
                case DebugInfoType.Error:
                    debugInfoItem = new DebugError();
                    break;
                case DebugInfoType.Info:
                    debugInfoItem = new DebugInfo();
                    break;
                case DebugInfoType.Warning:
                    debugInfoItem = new DebugWarning();
                    break;
                default:
                    return null;
            }

            serializer.Populate(jo.CreateReader(), debugInfoItem);
            return debugInfoItem;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}

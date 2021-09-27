using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace DownloadManager_CS_WPF.DebugInfoClasses
{
    
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class DebugInfoAbstract
    {
        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty]
        public string Message { get; set; }
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public DebugInfoType DebugItemType { get; set; }
        public bool Hidden { get; set; }
        [JsonProperty]
        public DateTime LogItemDate { get; set; }

        public override string ToString()
        {
            return $"[{LogItemDate.ToString()}] {Title} : {Message}";
        }
    }
}

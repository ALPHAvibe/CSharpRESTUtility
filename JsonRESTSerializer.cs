using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestTools
{
    public class JsonRESTSerializer : IRESTSerializer
    {
        /// <summary>
        /// Json serialization settings
        /// </summary>
        public JsonSerializerSettings settings { get; set; }

        public string Serialize<T>(T payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.Indented, settings);
        }

        public T Deserialize<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}

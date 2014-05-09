using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebRequestTools
{
    public class XMLRESTSerializer : IRESTSerializer
    {
        public string Serialize<T>(T payload)
        {
            XmlSerializer serializer = new XmlSerializer(payload.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, payload);

                return writer.ToString();
            }
        }


        public T Deserialize<T>(string response)
        {
            XmlSerializer serializer = new XmlSerializer(response.GetType());

            using (StringReader reader = new StringReader(response))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}

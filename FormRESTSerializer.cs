using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestTools
{
    public class FormRESTSerializer : IRESTSerializer
    {
        public string Serialize<T>(T payload)
        {
            return RESTUtility.ConvertToQueryString<T>(payload);
        }

        public T Deserialize<T>(string response)
        {
            throw new NotImplementedException();
        }
    }
}

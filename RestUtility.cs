using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WebRequestTools
{
    public static class RESTUtility
    {
        public enum HttpVerb
        {
            GET,
            POST,
            PUT,
            PATCH,
            DELETE
        }

        public static string BasicAuthHeaderValue(string userName, string userPassword)
        {
            string authInfo = String.Format("{0}:{1}", userName, userPassword);

            return String.Format("Basic {0}",  Convert.ToBase64String(Encoding.Default.GetBytes(authInfo)));
        }

        public static string ConvertToQueryString<T>(T instance)
        {
            var properties = from propertyInfo in instance.GetType().GetProperties()
                             where propertyInfo.GetValue(instance, null) != null
                             select propertyInfo.Name + "=" + HttpUtility.UrlEncode(propertyInfo.GetValue(instance, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public static  string Get(string url, Action<HttpWebRequest> setRequest = null)
        {
            return MakeRequest(HttpVerb.GET.ToString(), url, String.Empty, setRequest);
        }

        public static T Get<T>(string url, IRESTSerializer responseSerializer, Action<HttpWebRequest> setRequest = null)
        {
            return responseSerializer.Deserialize<T>(Get(url, setRequest));
        }

        public static string Post(string url, string payload, Action<HttpWebRequest> setRequest = null)
        {
            return MakeRequest(HttpVerb.POST.ToString(), url, payload, setRequest);
        }

        public static Out Post<In, Out>(string url, In payload, IRESTSerializer requestSerializer, IRESTSerializer responseSerializer, Action<HttpWebRequest> setRequest = null)
        {
            return responseSerializer.Deserialize<Out>(Post(url, requestSerializer.Serialize<In>(payload), setRequest));                                              
        }

        public static string Put(string url, string payload, Action<HttpWebRequest> setRequest = null)
        {
            return MakeRequest(HttpVerb.PUT.ToString(), url, payload, setRequest);
        }

        public static Out Put<In, Out>(string url, In payload, IRESTSerializer requestSerializer, IRESTSerializer responseSerializer, Action<HttpWebRequest> setRequest = null)
        {
            return responseSerializer.Deserialize<Out>(Put(url, requestSerializer.Serialize<In>(payload), setRequest));
        }

        public static string Patch(string url, string payload, Action<HttpWebRequest> setRequest = null)
        {
            return MakeRequest(HttpVerb.PATCH.ToString(), url, payload, setRequest);
        }

        public static Out Patch<In, Out>(string url, In payload, IRESTSerializer requestSerializer, IRESTSerializer responseSerializer, Action<HttpWebRequest> setRequest = null)
        {
            return responseSerializer.Deserialize<Out>(Patch(url, requestSerializer.Serialize<In>(payload), setRequest));
        }

        public static void Delete(string url, Action<HttpWebRequest> setRequest)
        {
            MakeRequest(HttpVerb.DELETE.ToString(), url, String.Empty, setRequest);
        }

        public static string MakeRequest(string httpVerb, string url, string payload, Action<HttpWebRequest> setRequest = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = httpVerb;

            if (setRequest != null)
            {
                setRequest(request);
            }

            if (!String.IsNullOrEmpty(payload))
            {
                WriteRequestPayload(request, payload);
            }

            return ReadResponse((HttpWebResponse)request.GetResponse());
        }

        public static string ReadResponse(HttpWebResponse response)
        {
            var responseValue = string.Empty;

            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseValue = reader.ReadToEnd();
                    }
                }
            }

            return responseValue;
        }

        public static T GetResponse<T>(HttpWebResponse response, IRESTSerializer responseSerializer, Action<HttpWebRequest> setRequest = null)
        {
            return responseSerializer.Deserialize<T>(ReadResponse(response));
        }

        private static void WriteRequestPayload(HttpWebRequest request, string payload)
        {
            if (!String.IsNullOrEmpty(payload))
            {
                var encoding = new UTF8Encoding();
                var bytes = encoding.GetBytes(payload);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}

WebRequestTools
===============

C# Project for making generic REST service requests


Basic Usage
===============
<pre><code>
JsonRESTSerializer serializer = new JsonRESTSerializer
        {
            settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
        };


Action<HttpWebRequest> requestSetup = request =>
            {
                request.ContentType = "application/json";
                request.Headers[HttpRequestHeader.Authorization.ToString()] = "UserName", "UserPassword";
            };

string urlRESTEndPoint = "https://foo.com/bar"

Bar = RESTUtility.Get<Bar>(urlRESTEndPoint, serializer, requestSetup);
</code></pre>

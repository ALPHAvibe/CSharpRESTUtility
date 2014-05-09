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


Action&lt;HttpWebRequest&gt; requestSetup = request =&gt;
            {
                request.ContentType = "application/json";
                request.Headers[HttpRequestHeader.Authorization.ToString()] = RESTUtility.BasicAuthHeaderValue("UserName", "UserPassword");
            };

string urlRESTEndPoint = "https://foo.com/bar"

Bar response = RESTUtility.Get&lt;Bar&gt;(urlRESTEndPoint, serializer, requestSetup);
</code></pre>

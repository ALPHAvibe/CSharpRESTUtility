WebRequestTools
===============

C# Project for making generic REST service requests


Basic Usage ##############################################################################################

JsonRESTSerializer serializer = new JsonRESTSerializer
        {
            settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
        };


Action<HttpWebRequest> requestSetup = request =>
            {
                request.ContentType = "application/json";
                request.Headers[HttpRequestHeader.Authorization.ToString()] = "UserName", "UserPassword";
            };

string urlRESTEndPoint = "https://foo.com/someobject"

SomeObject response = RESTUtility.Get<SomeObject>(urlRESTEndPoint, serializer, requestSetup);

###########################################################################################################

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CARGA_UDO
{
    public class SapServiceLayerClient
    {
        private readonly SapConnectionProfile profile;
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();
        private CookieContainer cookies = new CookieContainer();

        public SapServiceLayerClient(SapConnectionProfile profile)
        {
            this.profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public void Login()
        {
            var payload = new Dictionary<string, object>
            {
                ["CompanyDB"] = profile.CompanyDb,
                ["UserName"] = profile.SapUser,
                ["Password"] = profile.SapPassword
            };
            Send("Login", "POST", payload, false);
        }

        public bool Exists(string entitySet, string keyValue, bool numericKey)
        {
            try
            {
                Send(BuildKeyEndpoint(entitySet, keyValue, numericKey), "GET", null, true);
                return true;
            }
            catch (WebException ex)
            {
                if ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw;
            }
        }

        public void Add(string entitySet, Dictionary<string, object> payload)
        {
            Send(entitySet, "POST", payload, true);
        }

        public void Update(string entitySet, string keyValue, bool numericKey, Dictionary<string, object> payload)
        {
            Send(BuildKeyEndpoint(entitySet, keyValue, numericKey), "PATCH", payload, true);
        }

        private string BuildKeyEndpoint(string entitySet, string keyValue, bool numericKey)
        {
            string safe = (keyValue ?? string.Empty).Replace("'", "''");
            return numericKey ? $"{entitySet}({safe})" : $"{entitySet}('{safe}')";
        }

        private string Send(string endpoint, string method, object payload, bool authenticated)
        {
            string baseUrl = (profile.ServiceLayerUrl ?? string.Empty).TrimEnd('/');
            var request = (HttpWebRequest)WebRequest.Create($"{baseUrl}/{endpoint.TrimStart('/')}");
            request.Method = method;
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.CookieContainer = cookies;
            request.KeepAlive = true;

            if (method == "PATCH")
                request.Headers["B1S-ReplaceCollectionsOnPatch"] = "false";

            if (payload != null)
            {
                byte[] body = Encoding.UTF8.GetBytes(serializer.Serialize(payload));
                request.ContentLength = body.Length;
                using (Stream stream = request.GetRequestStream())
                    stream.Write(body, 0, body.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
                return reader.ReadToEnd();
        }
    }
}

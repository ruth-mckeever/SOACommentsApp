using RestSharp;
using System.Xml.Serialization; 


namespace SOACommentsApp.Services
{
    public class SanitizeService
    {
        static readonly string Sanitize_URL = "http://www.purgomalum.com/service/xml";

        public string SanitizeText(string rawText)
        {
            var client = new RestClient(Sanitize_URL);
            var request = new RestRequest();
            request.AddParameter("text", rawText);
            var response = client.Execute(request);
            if(!string.IsNullOrWhiteSpace(response.Content))
            {


                var serializer = new XmlSerializer(typeof(PurgoMalum));
                using (StringReader sr = new(response.Content))
                {
                    PurgoMalum? filtered = (PurgoMalum?)serializer.Deserialize(sr);
                    return filtered.result;
                }
            }

            return string.Empty;
        }
    }
}

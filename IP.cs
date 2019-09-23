using System.Net;
using System.IO;

namespace Walfrido.Service.FIP
{
    class IP
    {
        public static string GetIP()
        {
            WebRequest webRequest;
            WebResponse webResponse;
            string buffer;
            StreamReader streamReader;
            try
            {
                webRequest = HttpWebRequest.Create("http://icanhazip.com/");
                webResponse = webRequest.GetResponse();
                streamReader = new StreamReader(webResponse.GetResponseStream());
                buffer = streamReader.ReadToEnd();
                webResponse.Close();
                streamReader.Dispose();
                return buffer;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PageCheckerAPI.Services.Interfaces;

namespace PageCheckerAPI.Services
{
    public class WebsiteService : IWebsiteService
    {
        public string GetHtml(string url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    var encoding = response.CharacterSet.ToLower().Contains("utf-8") ? 
                        Encoding.UTF8 : 
                        Encoding.GetEncoding(response.CharacterSet);

                    readStream = new StreamReader(receiveStream, encoding);
                }

                string body = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return body;
            }

            return string.Empty;
        }
    }
}

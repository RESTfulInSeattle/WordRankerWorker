using System.Net;

namespace WordRankerWorker
{
    class URLFetcher
    {
        public string GetURLText(string URL)
        {
            string returnString="";

            if (URL.Length > 0)
            {
                WebClient client = new WebClient();
                returnString = client.DownloadString(URL);
            }

            return returnString;
        }
    }
}

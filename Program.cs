using Amazon.SQS.Model;
using System.Collections.Generic;
using System.Threading;

namespace WordRankerWorker
{
    class Program
    {
        public static string Rank(string url)
        {
            if (url.Length > 0)
            {
                URLFetcher uf = new URLFetcher();
                string urlText = uf.GetURLText(url);

                if (urlText.Length > 0)
                {
                    string returnString;
                    WordCollector wc = new WordCollector();
                    char[] delimeters = { ' ' };
                    Dictionary<string, long> words = wc.GetWords(urlText, delimeters);

                    if (words.Keys.Count > 0)
                    {
                        WordRanker wr = new WordRanker();
                        List<KeyValuePair<string, long>> rankedWords = wr.RankWords(words, 10);

                        returnString = "Top 10 Words:" + "\r\n";

                        foreach (KeyValuePair<string, long> word in rankedWords)
                        {
                            returnString += $"Word: {word.Key}  Count: {word.Value}" + "\r\n";
                        }

                        return returnString;
                    }
                    else
                    {
                        return "No words retrieved from URL: " + url;
                    }
                }
                else
                {
                    return "No text retrieved from URL: " + url;
                }


            }
            else
            {
                return "Invalid URL";
            }
        }

        public static void CheckForWork()
        {
            ReceiveMessageResponse messages =
                SQSActions.GetMessages("https://sqs.us-west-2.amazonaws.com/792614619693/Assignment3-Queue");

            if (messages.Messages.Count > 0)
            {
                foreach (var message in messages.Messages)
                {
                    string returnRank = Rank(message.Body);

                    bool success = DynamoDbActions.AddItem(message.Body, returnRank);

                    success = SQSActions.DeleteMessage("https://sqs.us-west-2.amazonaws.com/792614619693/Assignment3-Queue", message.ReceiptHandle);
                }
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                CheckForWork();
                Thread.Sleep(1000);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace WordRankerWorker
{
    class WordRanker
    {
        public List<KeyValuePair<string,long>> RankWords(Dictionary<string, long> words, int rank)
        {
            List<KeyValuePair<string, long>> returnList = new List<KeyValuePair<string, long>>();

            if (words.Keys.Count > 0 && rank > 0)
            {
                List<KeyValuePair<string,long>> sortedWords = words.ToList();
                sortedWords.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

                for (int i = 0; i < rank; i++)
                {
                    returnList.Add(sortedWords[i]);
                }
            }
           
            return returnList;
        }
    }
}

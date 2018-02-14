using Amazon.DynamoDBv2.DataModel;

namespace WordRankerWorker
{
    [DynamoDBTable("WordRankerTable")]

    public class DynamoDBItem
    {
        [DynamoDBHashKey]
        public string url { get; set; }
        [DynamoDBProperty]
        public string result { get; set; }
    }
}
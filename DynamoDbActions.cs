using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace WordRankerWorker
{
    public class DynamoDbActions
    {
        public static string RetrieveItem(string url)
        {
            try
            {
                var client = new AmazonDynamoDBClient();
                string tableName = "WordRankerTable";

                var request = new GetItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { { "Id", new AttributeValue { N = url } } }
                };

                var response = client.GetItem(request);

                Dictionary<string, AttributeValue> item = response.Item;

                string result = "";

                foreach (var keyValuePair in item)
                {

                    result = keyValuePair.Value.S;
                }

                return result;
            }
            catch (AmazonDynamoDBException e)
            {
                return "";
            }
        }

        public static bool AddItem(string url, string result)
        {
            try
            {
                var client = new AmazonDynamoDBClient();
                var context = new DynamoDBContext(client);
                var item = new DynamoDBItem
                {
                    url = url,
                    result = result
                };

                context.Save(item);
            }
            catch (AmazonDynamoDBException e)
            {
                return false;
            }

            return true;


        }
    }
}
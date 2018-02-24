using Amazon.SQS;
using Amazon.SQS.Model;
using System.Net;

namespace WordRankerWorker
{
    public class SQSActions
    {

        private static string serviceURL = "http://sqs.us-west-2.amazonaws.com";

        public static ReceiveMessageResponse GetMessages(string queueURL)
        {

            var sqsConfig = new AmazonSQSConfig {ServiceURL = serviceURL};
            var sqsClient = new AmazonSQSClient(sqsConfig);

            var receiveMessageRequest = new ReceiveMessageRequest {QueueUrl = queueURL};

            var receiveMessageResponse = sqsClient.ReceiveMessage(receiveMessageRequest);
            return receiveMessageResponse;
        }

        public static bool DeleteMessage(string queueURL, string recieptHandle)
        {
            var sqsConfig = new AmazonSQSConfig {ServiceURL = serviceURL};
            var sqsClient = new AmazonSQSClient(sqsConfig);

            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = queueURL,
                ReceiptHandle = recieptHandle
            };

            var response = sqsClient.DeleteMessage(deleteMessageRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK) return true;

            return false;
        }

        public static bool AddMessage(string queueURL, string messageBody)
        {
            var sqsConfig = new AmazonSQSConfig { ServiceURL = serviceURL };
            var sqsClient = new AmazonSQSClient(sqsConfig);

            var sendMessageRequest = new SendMessageRequest
            {
                MessageBody = messageBody,
                QueueUrl = queueURL
            };

            var response = sqsClient.SendMessage(sendMessageRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK) return true;

            return false;
        }
        
    }
}
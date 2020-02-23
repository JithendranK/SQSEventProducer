using System;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace ConsoleApp1
{

    public class Study 
    {
        public string StudyName { get; set; }
        public string StudyStartDate { get; set; }
        public string EstimatedCompletionDate { get; set; }
        public string ProtocolID { get; set; }
        public string StudyGroup { get; set; }
        public string Phase { get; set; }
        public string PrimaryIndication { get; set; }
        public string SecondaryIndication { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pushing Messages to Aws Queue...........");

            Study s1;
          
            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
            var queueUrl = "https://sqs.ap-south-1.amazonaws.com/066325793814/cytel-load-test.fifo";
            //var sqsRequest = new CreateQueueRequest()
            //{
            //    QueueName = "CytelSimulationQueueTest",
            //};
            //var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;
            //var myQueueQurl = createQueueResponse.QueueUrl;
            //var listQueuesRequest = new ListQueuesRequest();
            //var listQueuesResponse = sqs.ListQueuesAsync(listQueuesRequest);
            //Console.WriteLine("List of Queues");
            //foreach(var queueUrl in listQueuesResponse.Result.QueueUrls)
            //{
            //    Console.WriteLine($"Queue Url: {queueUrl}");
            //}

            for (int i = 1; i <= 5000; i++)
            {
             
                s1   = new Study
                {
                    StudyName = $"Study- {i}",
                    StudyStartDate = "01 Jan 2010",
                    EstimatedCompletionDate = "05 Jan 2019",
                    ProtocolID = $"ProtocolID - {i}",
                    StudyGroup = $"Groupd - {i}",
                    Phase = "Phase1",
                    PrimaryIndication = "Indication 1",
                    SecondaryIndication = "Secondary Indication1"
                };

                var sqsmessageRequest = new SendMessageRequest()
                {
                    QueueUrl = queueUrl,
                    MessageBody = JsonConvert.SerializeObject(s1),
                    MessageGroupId = "CytelMessages1",
                    MessageDeduplicationId = $"CytemDeduplication{i}"
                };
                sqs.SendMessageAsync(sqsmessageRequest);
            }
            Console.WriteLine("Completed Sending messages.....");
            Console.ReadLine();
        }
    }
}

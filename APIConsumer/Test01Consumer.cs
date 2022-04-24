using Models.ModelRequest;
using Models.ModelResponse;
using Models.ModelMQ;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;


namespace APIConsumer
{
    public static class Test01Consumer
    {
        public static void ConsumedAPITest01(string message)
        {
            try
            {
                MqResponseBase<MQTest01Request> messageConsumed = JsonConvert.DeserializeObject<MqResponseBase<MQTest01Request>>(message);
                string baseUrl = "https://localhost:5001/";
                switch (messageConsumed.command)
                {
                    case "create":
                        string endpoint = "test01/save";
                        Console.WriteLine(PostRequest(baseUrl, endpoint, messageConsumed.data).GetAwaiter().GetResult());
                        break;
                    case "update":
                        endpoint = "test01/update";
                        Console.WriteLine(PutRequest(baseUrl, endpoint, messageConsumed.data).GetAwaiter().GetResult());
                        break;
                    case "delete":
                        endpoint = "test01/remove";
                        Console.WriteLine(DeleteRequest(baseUrl, endpoint, messageConsumed.data).GetAwaiter().GetResult());
                        break;
                    default:
                        Console.WriteLine("There is no recognizeable queue command");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Execute MQ : {ex.Message}");
            }
        }

        public static async Task<string> PostRequest(string baseUrl, string endpoint, MQTest01Request data)
        {
            string response = "";
            Test01Request requestData = new Test01Request()
            {
                Nama = data.Nama,
                Status = data.Status ?? 0
            };
            try
            {
                string url = $"{baseUrl}{endpoint}";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "text/plain; charset=utf-8");
                request.AddBody(JsonConvert.SerializeObject(requestData), "application/json");

                var execute = await client.PostAsync(request);
                response = execute.Content;
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }

        public static async Task<string> PutRequest(string baseUrl, string endpoint, MQTest01Request data)
        {
            string response = "";
            Test01Request requestData = new Test01Request()
            {
                Nama = data.Nama,
                Status = data.Status ?? 0
            };
            try
            {
                string url = $"{baseUrl}{endpoint}/{data.Id}";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.Method = Method.Put;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "text/plain; charset=utf-8");
                request.AddBody(JsonConvert.SerializeObject(requestData), "application/json");

                var execute = await client.PutAsync(request);
                response = execute.Content;
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }

        public static async Task<string> DeleteRequest(string baseUrl, string endpoint, MQTest01Request data)
        {
            string response = "";
            try
            {
                string url = $"{baseUrl}{endpoint}/{data.Id}";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.Method = Method.Delete;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "text/plain; charset=utf-8");

                var execute = await client.DeleteAsync(request);
                response = execute.Content;
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.ModelMQ;
using Models.ModelRequest;
using Newtonsoft.Json;
using RabbitMqModels.Request;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace WebServiceMQPublisher.Controllers
{
    [Route("api/mq/test01/")]
    [ApiController]
    public class Test01MqController : ControllerBase
    {
        const string URL_PUBLISH = "http://localhost:15672/api/exchanges/%2f/amq.default/publish";
        const string ROUTING_KEY = "qtest1";
        public IConfiguration Configuration;
 
        public Test01MqController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpPost("save")]
        public async Task<IActionResult> Post([FromBody] Test01Request paramRequest)
        {
            try
            {
                var client = new RestClient(URL_PUBLISH);
                client.Authenticator = new HttpBasicAuthenticator("guest", "guest");

                var messageRequest = new MqResponseBase<MQTest01Request>()
                {
                    command = "create",
                    data = new MQTest01Request()
                    {
                        Id = 0,
                        Nama = paramRequest.Nama,
                        Status = paramRequest.Status
                    }
                };

                var dataRequest = new PublishRequest()
                {
                    properties = new Property(),
                    routing_key = ROUTING_KEY,
                    payload = JsonConvert.SerializeObject(messageRequest),
                    payload_encoding = "string"
                };

                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "*/*");
                request.AddBody(JsonConvert.SerializeObject(dataRequest), "application/json");

                var execute = await client.PostAsync(request);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Test01Request paramRequest)
        {
            try
            {
                var client = new RestClient(URL_PUBLISH);
                client.Authenticator = new HttpBasicAuthenticator("guest", "guest");

                var messageRequest = new MqResponseBase<MQTest01Request>()
                {
                    command = "update",
                    data = new MQTest01Request()
                    {
                        Id = id,
                        Nama = paramRequest.Nama,
                        Status = paramRequest.Status
                    }
                };

                var dataRequest = new PublishRequest()
                {
                    properties = new Property(),
                    routing_key = ROUTING_KEY,
                    payload = JsonConvert.SerializeObject(messageRequest),
                    payload_encoding = "string"
                };

                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "*/*");
                request.AddBody(JsonConvert.SerializeObject(dataRequest), "application/json");

                var execute = await client.PostAsync(request);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {

                var client = new RestClient(URL_PUBLISH);
                client.Authenticator = new HttpBasicAuthenticator("guest", "guest");

                var messageRequest = new MqResponseBase<MQTest01Request>()
                {
                    command = "delete",
                    data = new MQTest01Request()
                    {
                        Id = id,
                        Nama = "",
                        Status = 0
                    }
                };

                var dataRequest = new PublishRequest()
                {

                    properties = new Property(),
                    routing_key = ROUTING_KEY,
                    payload = JsonConvert.SerializeObject(messageRequest),
                    payload_encoding = "string"
                };

                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "*/*");
                request.AddBody(JsonConvert.SerializeObject(dataRequest), "application/json");

                var execute = await client.PostAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
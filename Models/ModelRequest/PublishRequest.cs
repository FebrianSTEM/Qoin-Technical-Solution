using Models.ModelMQ;
using System;

namespace RabbitMqModels.Request
{
    public class PublishRequest
    {
        public Property properties { get; set; }
        public string routing_key { get; set; }
        public string payload { get; set; }
        public string payload_encoding { get; set; }
    }
}

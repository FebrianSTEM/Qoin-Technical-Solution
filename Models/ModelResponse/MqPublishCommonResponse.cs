using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ModelResponse
{
    public class MqPublishCommonResponse
    {
        public string Error { get; set; }
        public string[] Reason { get; set; } = new string[0];
        public bool Routed { get; set; }
    }
}

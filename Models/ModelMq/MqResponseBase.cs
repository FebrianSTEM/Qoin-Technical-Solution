using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ModelMQ
{
    public class MqResponseBase<T>
    {
        public string command { get; set; }
        public T data { get; set; }
    }
}
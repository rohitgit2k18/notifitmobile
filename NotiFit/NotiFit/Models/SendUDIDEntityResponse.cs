using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.Models
{
    public class SendUDIDEntityResponse
    {
        public Responses Response { get; set; }
    }
    public class Responses
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}

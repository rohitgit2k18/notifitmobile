using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.Models
{
    public class UploadProfileResponse
    {
        public Response Response { get; set; }
    }
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}

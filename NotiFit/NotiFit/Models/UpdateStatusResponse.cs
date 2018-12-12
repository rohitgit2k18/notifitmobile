using System;
namespace NotiFit.Models
{
    public class UpdateStatusResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Int64 WorkoutId { get; set; }
    }
}

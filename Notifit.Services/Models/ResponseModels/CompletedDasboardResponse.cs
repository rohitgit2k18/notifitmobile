using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
   public class CompletedDasboardResponse
    {
        public CompResponse Response { get; set; }
    }

    public class CompWorkout
    {
        public int CompletedWorkout { get; set; }
    }

    public class CompResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public CompWorkout compWorkout { get; set; }
    }
  
}

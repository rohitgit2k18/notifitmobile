using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
   public class AddActualExerciseResponse
    {
        public ActulResponse Response { get; set; }
    }
    public class ActulResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    
}

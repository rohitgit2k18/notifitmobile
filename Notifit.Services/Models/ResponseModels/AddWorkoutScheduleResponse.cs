using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
   public class AddWorkoutScheduleResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int WorkoutId { get; set; }
    }
}

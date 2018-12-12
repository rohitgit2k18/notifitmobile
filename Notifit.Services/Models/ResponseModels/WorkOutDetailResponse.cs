using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
   public class WorkOutDetailResponse
    {
        public WKDetailResponse Response { get; set; }
    }
    public class WKDetailResponse
    {
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string DateOfWorkout { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string WorkoutNotes { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool AutoFinishTime { get; set; }
    }

   
}

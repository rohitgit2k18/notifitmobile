using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.RequestModels
{
   public class AddWorkoutRequest
    {
        public long UserId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfWorkout { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public string WorkoutNotes { get; set; }
        public bool AutoFinishTime { get; set; }
        ///////
       
    }
}

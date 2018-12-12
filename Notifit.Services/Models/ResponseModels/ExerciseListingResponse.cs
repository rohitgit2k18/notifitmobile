using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
   public class ExerciseListingResponse
    {
        public ExercResponse Response { get; set; }
    }

    public class Workoutlist
    {
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime WorkDate { get; set; }
        public string DateOfWorkout { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string WorkoutNotes { get; set; }
        public DateTime Createdate { get; set; }
        public bool IsActive { get; set; }
        public bool RowStatus { get; set; }
        public string Status { get; set; }
        public bool ScheduleWorkout { get; set; }
    }

    public class ExercResponse
    {
        public List<Workoutlist> workoutlist { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

   
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.RequestModels
{
       public class AddWorkoutScheduleRequest
    {
        public string WorkoutText { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public int RecurrenceId { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public TimeSpan TextMeTime { get; set; }
        public TimeSpan TextFriendTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

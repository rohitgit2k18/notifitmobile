using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
   public class ScheduleListingResponse
    {
        public ScheduleListResponse Response { get; set; }
    }
    public class ScheduleList
    {
        public int ScheduleId { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public int WorkOutId { get; set; }
        public int ScheduleWeekly { get; set; }
        public string ScheduleTime { get; set; }
        public int TextMeTime { get; set; }
        public int TextFriendTime { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public object WorkoutStartTime { get; set; }
        public object WorkoutFinishTime { get; set; }
        public bool AutoFinishTime { get; set; }
        public bool ScheduleOrNot { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }

    public class ScheduleListResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        //public List<ScheduleList> scheduleLists { get; set; }
        public List<ScheduleList> scheduleLists { get; set; }
    }

    
}

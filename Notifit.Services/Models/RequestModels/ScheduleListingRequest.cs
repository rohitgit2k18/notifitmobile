﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.RequestModels
{
   public class ScheduleListingRequest
    {
        public long UserId { get; set; }
        public string Search { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.ResponseModels
{
  public class AddWorkoutResponse
    {
        public Int64 WorkoutId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}

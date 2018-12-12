using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.ViewModels
{
    public class MeasurementViewModel
    {
        public List<MeasurementEntity> measurementWightEntities { get; }
        = new List<MeasurementEntity>()
        {
            new MeasurementEntity()
            {
                Id = 1,
                Name = "Kg"
            },
            new MeasurementEntity()
            {
                Id = 2,
                Name = "lbs"
            }
        };

        public List<MeasurementDistance> measurementDistance { get; }
        = new List<MeasurementDistance>()
        {
            new MeasurementDistance()
            {
                Id = 1,
                DistanceName = "Km"
            },
            new MeasurementDistance()
            {
                Id = 2,
                DistanceName = "miles"
            }
        };
    }
}

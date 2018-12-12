using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.RequestModels
{
    public class AddExerciseWorkoutRequest
    {
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
        public List<MainList> mainList { get; set; }
      
    }

    public class WeightList
    {
        public int TotalWeight { get; set; }
        public int TotalRaps { get; set; }
        public string ImperialType { get; set; }
    }

    public class LevelList
    {
        public int TotalWeight { get; set; }
        public int TotalRaps { get; set; }
        public string ImperialType { get; set; }
    }

    public class TimedList
    {
        public int TimedSet { get; set; }
    }

    public class RapList
    {
        public int RepsSets { get; set; }
    }

    public class Distance
    {
        public int RepsSetsTime { get; set; }
    }

    public class TextList
    {
        public string Text { get; set; }
    }

    public class MainList
    {
        public MainList()
        {
            weightList = new List<WeightList>();
            levelList = new List<LevelList>();
            timedList = new List<TimedList>();
            rapList = new List<RapList>();
            distance = new Distance();
            textList = new TextList();
        }
        public string ExerciseName { get; set; }
        public int ExerciseTypeId { get; set; }
        public int SetsNumber { get; set; }
        public double DistanceInKm { get; set; }
        public string ImperialType { get; set; }
        public string Text { get; set; }
        public List<WeightList> weightList { get; set; }
        public List<LevelList> levelList { get; set; }
        public List<TimedList> timedList { get; set; }
        public List<RapList> rapList { get; set; }
        public Distance distance { get; set; }
        public TextList textList { get; set; }
    }

    
}

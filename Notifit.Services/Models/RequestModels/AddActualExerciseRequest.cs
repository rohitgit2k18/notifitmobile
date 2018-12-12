using System;
using System.Collections.Generic;
using System.Text;

namespace Notifit.Services.Models.RequestModels
{
  public  class AddActualExerciseRequest
    {
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
        public List<UpdateList> UpdateList { get; set; }
    }
    public class ActualWeightList
    {
        public decimal TotalWeight { get; set; }
        public int TotalRaps { get; set; }
        public string ImperialType { get; set; }
    }

    public class LevList
    {
        public decimal TotalWeight { get; set; }
        public int TotalRaps { get; set; }
        public string ImperialType { get; set; }
    }

    public class TimList
    {
        public int TimedSet { get; set; }
    }

    public class ActualRapList
    {
        public int RepsSets { get; set; }
    }

    public class DisList
    {
        public int RepsSetsTime { get; set; }
    }

    public class TextListOfActual
    {
        public string Text { get; set; }
    }

    public class UpdateList
    {
        public UpdateList()
        {
            weightList = new List<ActualWeightList>();
            levList = new List<LevList>();
            timList = new List<TimList>();
            rapList = new List<ActualRapList>();
            disList = new List<DisList>();
            textListOfActual = new List<TextListOfActual>();
        }
        public int ExcerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int ExerciseTypeId { get; set; }
        public int? SetsNumber { get; set; }
        public double DistanceInKm { get; set; }
        public string ImperialType { get; set; }
        public string Text { get; set; }
        public List<ActualWeightList> weightList { get; set; }
        public List<LevList> levList { get; set; }
        public List<TimList> timList { get; set; }
        public List<ActualRapList> rapList { get; set; }
        public List<DisList> disList { get; set; }
        public List<TextListOfActual> textListOfActual { get; set; }
    }

   
}

using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.ViewModels
{
   public class WorkoutTypesViewModel
    {
        public List<WorkoutTypesEntity>WorkOutSource { get; }
      = new List<WorkoutTypesEntity>() {
                new WorkoutTypesEntity() {
                   TypeID=1,
                   TypeName="Sets X Reps with weight"
                },
                new WorkoutTypesEntity() {
                    TypeID=2,
                   TypeName="Sets X Reps with level"
                },
                            new WorkoutTypesEntity() {
                   TypeID=3,
                   TypeName="Timed Sets (Seconds)"
                },
                            new WorkoutTypesEntity() {
                    TypeID=4,
                   TypeName=" Sets X Reps"
                },
                              new WorkoutTypesEntity() {
                    TypeID=5,
                   TypeName="Distance X Time"
                },
                           new WorkoutTypesEntity() {
                    TypeID=6,
                   TypeName="Free Text"
                }
          };
    }
}

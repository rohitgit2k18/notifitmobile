using Notifit.Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScheduleGoalListing : ContentPage
	{
        #region Variable Declaration
        private ExerciseDetailsByWorkoutIDResponse _objExerciseDetailsByWorkoutIDResponse;
        private List<EditExercise> editexcercise = new List<EditExercise>();
        public int count = 0;
        #endregion

        public ScheduleGoalListing (ExerciseDetailsByWorkoutIDResponse ObjExerciseDetailsByWorkoutIDResponse)
		{
			InitializeComponent ();
            _objExerciseDetailsByWorkoutIDResponse = new ExerciseDetailsByWorkoutIDResponse();
            _objExerciseDetailsByWorkoutIDResponse = ObjExerciseDetailsByWorkoutIDResponse;
            if (_objExerciseDetailsByWorkoutIDResponse.Response.editExercise.Count > 0)
            {
                foreach (var item in _objExerciseDetailsByWorkoutIDResponse.Response.editExercise)
                {

                    var excercisetypename = item.ExerciseTypename;
                    var setdescription = item.SetsDescription;
                    if (excercisetypename == item.ExerciseTypename && setdescription == item.SetsDescription && count == 0)
                    {
                        editexcercise.Add(item);
                        count = 1;
                    }

                }
                ExerciseList.ItemsSource = editexcercise;
                //ExerciseList.ItemsSource = _objExerciseDetailsByWorkoutIDResponse.Response.editExercise;
            }
        }

        private void XFBtnFillFromGoal_Clicked(object sender, EventArgs e)
        {

        }

        private void XFBtnUpdateExercise_Clicked(object sender, EventArgs e)
        {

        }
    }
}
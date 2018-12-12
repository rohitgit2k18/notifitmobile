using Notifit.Services.Models.ResponseModels;
using NotiFit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Views.Workouts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkoutExerciseDetailListing : ContentPage
	{
        #region Variable Declaration
        public string MyStatus { get; set; }
        private ExerciseDetailsByWorkoutIDResponse _objExerciseDetailsByWorkoutIDResponse;
        private List<EditExercise> editexcercise = new List<EditExercise>();
        public int count = 0;
        #endregion
        public WorkoutExerciseDetailListing (ExerciseDetailsByWorkoutIDResponse ObjExerciseDetailsByWorkoutIDResponse)
		{
			InitializeComponent ();
            _objExerciseDetailsByWorkoutIDResponse = new ExerciseDetailsByWorkoutIDResponse();
            _objExerciseDetailsByWorkoutIDResponse = ObjExerciseDetailsByWorkoutIDResponse;
            if (_objExerciseDetailsByWorkoutIDResponse.Response.editExercise.Count > 0)
            {
                foreach(var item in _objExerciseDetailsByWorkoutIDResponse.Response.editExercise)
                {
                    
                    var excercisetypename = item.ExerciseTypename;
                    var setdescription = item.SetsDescription;
                    if(excercisetypename == item.ExerciseTypename && setdescription == item.SetsDescription && count == 0)
                    {
                        editexcercise.Add(item);
                        count = 1;
                    }
                    item.Status = Settings.StatusName;
                }
                ExerciseList.ItemsSource = editexcercise;
            }
            //MyStatus = Settings.StatusName;
            //lblStatus.Text = MyStatus;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    lblStatus.Text = Settings.StatusName;
        //}

        

        private void XFBtnFillFromGoal_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                var selectedItem = ((Button)sender);               
                var ObjBindings = selectedItem.BindingContext as EditExercise;
                var ValidObjectID = ObjBindings.ExerciseTypeId;
                ObjBindings.IsFillfromGoal = true;
                if (ValidObjectID>0)
                {
                    App.NavigationPage.Navigation.PushAsync(new UpdateActualExercisePage(ObjBindings));
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
           //
        }

        private void XFBtnUpdateExercise_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = ((Button)sender);
                var ObjBindings = selectedItem.BindingContext as EditExercise;
                var ValidObjectID = ObjBindings.ExerciseTypeId;
                ObjBindings.IsFillfromGoal = false;
                if (ValidObjectID > 0)
                {
                    App.NavigationPage.Navigation.PushAsync(new UpdateActualExercisePage(ObjBindings));
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
           // ObjBindings.IsFillfromGoal = false;
          //  App.NavigationPage.Navigation.PushAsync(new UpdateActualExercisePage(null));
        }
    }
}
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
	public partial class EditExercisePage : ContentPage
	{
		public EditExercisePage ()
		{
			InitializeComponent ();
		}

        private void EntryNoOfSets_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void dropdownExerciseType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnSubmitExercise_Clicked(object sender, EventArgs e)
        {

        }
    }
}
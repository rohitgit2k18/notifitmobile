using Android.Content;
using Android.Graphics.Drawables;
using NotiFit.Droid.Renderers;
using Xamarin.Forms;
using NotiFit.CustomeControls;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomeButton), typeof(CustomeButtonRenderer))]
namespace NotiFit.Droid.Renderers
{
    public class CustomeButtonRenderer : ButtonRenderer
    {
        public CustomeButtonRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.mybutton);
                // Control.SetBackground(Android.Graphics.Drawables.Drawable.mybutton);
                //int[][] states = new int[2][]
                //              {
                //    new int[] { Android.Resource.Attribute.StateEnabled, 0},
                //    new int[] { -Android.Resource.Attribute.StateEnabled, 0}
                //              };

                //int[] colors = new int[2]
                //{
                //    Xamarin.Forms.Color.FromHex("#ffff00").ToAndroid(),
                //    Xamarin.Forms.Color.FromHex("#ff0000").ToAndroid()
                //};
                //var gd = new GradientDrawable();
                //gd.SetCornerRadius(10);
                //gd.SetStroke(1, Xamarin.Forms.Color.FromHex("#00ff00").ToAndroid());
                //gd.SetColor(new Android.Content.Res.ColorStateList(states, colors));

                //Control.SetBackground(gd);
                Control.TextAlignment = Android.Views.TextAlignment.Center;
                Control.InputType = Android.Text.InputTypes.TextFlagCapSentences;
            }

        }
    }
}
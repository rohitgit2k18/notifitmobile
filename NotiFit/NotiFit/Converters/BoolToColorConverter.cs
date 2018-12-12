using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiFit.Converters
{
    //class BoolToColorConverter
    //{
    //}
    public class BoolToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var IsSelected = (Boolean)value;
            if (IsSelected)
            {
                return Color.FromHex("#FF7C12");
            }
            else
            {
                return Color.FromHex("#64BAEB");
            }
            //var valueAsString = value.ToString();
            //switch (valueAsString)
            //{
            //    case ("Urgent"):
            //        {
            //            return Color.FromHex("#EF3D60");
            //        }
            //    case ("Normal"):
            //        {
            //            return Color.FromHex("#231F20");
            //        }
            //    default:
            //        {
            //            return Color.FromHex("#231F20");
            //        }
            //}
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

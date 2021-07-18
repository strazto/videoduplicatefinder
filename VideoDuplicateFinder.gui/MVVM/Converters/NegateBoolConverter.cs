using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace VideoDuplicateFinder.gui.MVVM.Converters
{
    public class NegateBoolConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
    }
	// This was copied from the windows project - Consider Sharing
	sealed class SizeToStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => DuplicateFinderEngine.Utils.BytesToString((long)value);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}

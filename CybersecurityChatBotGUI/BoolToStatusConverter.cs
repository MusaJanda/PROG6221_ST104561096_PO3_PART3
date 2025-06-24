using System;
using System.Globalization;
using System.Windows.Data;

namespace CybersecurityChatBotGUI // <-- CRITICAL: ENSURE THIS IS EXACTLY THE SAME AS YOUR PROJECT'S NAMESPACE
{
    public class BoolToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isCompleted)
            {
                return isCompleted ? "Done" : "Pending";
            }
            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
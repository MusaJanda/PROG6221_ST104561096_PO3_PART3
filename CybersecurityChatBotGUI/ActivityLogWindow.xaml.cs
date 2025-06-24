using System.Collections.ObjectModel; // Required for ObservableCollection
using System.Windows;

namespace CybersecurityChatBotGUI
{
    /// <summary>
    /// Interaction logic for ActivityLogWindow.xaml
    /// </summary>
    public partial class ActivityLogWindow : Window
    {
        // Public property to bind the ListView to
        public ObservableCollection<ActivityLogEntry> LogEntries { get; set; } // LogEntries property added for binding

        public ActivityLogWindow(ObservableCollection<ActivityLogEntry> logEntries) // Constructor correctly accepts ObservableCollection
        {
            InitializeComponent();
            // Set the ListView's ItemsSource to the passed log entries
            LogEntries = logEntries; // Assigns the passed log entries to the LogEntries property
            this.DataContext = this; // Set DataContext for binding
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.ComponentModel; // For INotifyPropertyChanged

namespace CybersecurityChatBotGUI
{
    public class Reminder : INotifyPropertyChanged
    {
        private string _text = string.Empty; // Initialized to empty string
        private DateTime? _dueDate;

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value ?? string.Empty; // Handle null assignment, ensures non-null
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public DateTime? DueDate
        {
            get { return _dueDate; }
            set
            {
                if (_dueDate != value)
                {
                    _dueDate = value;
                    OnPropertyChanged(nameof(DueDate));
                    OnPropertyChanged(nameof(DisplayDueDate)); // Notify change for DisplayDueDate as well
                }
            }
        }

        // Calculated property for displaying the due date nicely in the UI
        public string DisplayDueDate => DueDate.HasValue ? DueDate.Value.ToShortDateString() : "No Date";

        // Constructor to create a new Reminder
        public Reminder(string text, DateTime? dueDate)
        {
            Text = text ?? string.Empty; // Ensure text is not null, uses property setter
            DueDate = dueDate;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
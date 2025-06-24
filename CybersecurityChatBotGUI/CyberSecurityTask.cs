using System;
using System.ComponentModel; // Required for INotifyPropertyChanged

namespace CybersecurityChatBotGUI
{
    public class CybersecurityTask : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private DateTime? _dueDate;
        private bool _isCompleted;

        public string Title // Fix: Ensure initialization or use 'required'
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Description // Fix: Ensure initialization or use 'required'
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                if (_dueDate != value)
                {
                    _dueDate = value;
                    OnPropertyChanged(nameof(DueDate));
                }
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        // Constructor
        public CybersecurityTask(string title, string description, DateTime? dueDate)
        {
            _title = title; // Initializing title
            _description = description; // Initializing description
            _dueDate = dueDate;
            _isCompleted = false; // Default
        }

        // Parameterless constructor for deserialization (if needed by JSON.NET, though not always strictly required with properties)
        public CybersecurityTask()
        {
            // Default values for properties if a parameterless constructor is used
            _title = string.Empty; // Provide a default non-null value
            _description = string.Empty; // Provide a default non-null value
            _isCompleted = false;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
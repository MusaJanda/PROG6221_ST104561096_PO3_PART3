using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace CybersecurityChatBotGUI
{
    /// <summary>
    /// Interaction logic for TaskAssistantForm.xaml
    /// </summary>
    public partial class TaskAssistantForm : Window
    {
        private const string TasksFileName = "tasks.json";
        public ObservableCollection<CybersecurityTask> Tasks { get; set; }

        public TaskAssistantForm(object? dataContext)
        {
            InitializeComponent();
            Tasks = new ObservableCollection<CybersecurityTask>();
            LoadTasks();
            this.DataContext = this;
        }

        public static void AddTaskStatic(CybersecurityTask newTask)
        {
            ObservableCollection<CybersecurityTask> currentTasks = LoadTasksFromFile();
            currentTasks.Add(newTask);
            SaveTasksToFile(currentTasks);

            MainWindow.LogActivity("Task Added Statically", $"Added task: {newTask.Title}");
        }

        private void LoadTasks()
        {
            ObservableCollection<CybersecurityTask> loadedTasks = LoadTasksFromFile();
            foreach (var task in loadedTasks)
            {
                Tasks.Add(task);
            }
        }

        private static ObservableCollection<CybersecurityTask> LoadTasksFromFile()
        {
            if (File.Exists(TasksFileName))
            {
                try
                {
                    string json = File.ReadAllText(TasksFileName);
                    return JsonConvert.DeserializeObject<ObservableCollection<CybersecurityTask>>(json) ?? new ObservableCollection<CybersecurityTask>();
                }
                catch (JsonSerializationException ex)
                {
                    MessageBox.Show($"Error loading tasks: {ex.Message}\nStarting with empty task list.", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MainWindow.LogActivity("Task Load Error", $"JSON deserialization failed: {ex.Message}");
                    return new ObservableCollection<CybersecurityTask>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred loading tasks: {ex.Message}\nStarting with empty task list.", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MainWindow.LogActivity("Task Load Error", $"Unexpected error: {ex.Message}");
                    return new ObservableCollection<CybersecurityTask>();
                }
            }
            return new ObservableCollection<CybersecurityTask>();
        }

        private void SaveTasks()
        {
            SaveTasksToFile(Tasks);
        }

        private static void SaveTasksToFile(ObservableCollection<CybersecurityTask> tasksToSave)
        {
            try
            {
                string json = JsonConvert.SerializeObject(tasksToSave, Formatting.Indented);
                File.WriteAllText(TasksFileName, json);
                MainWindow.LogActivity("Tasks Saved", "All tasks saved to file.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tasks: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow.LogActivity("Task Save Error", $"Failed to save tasks: {ex.Message}");
            }
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Task title cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime? dueDate = null;
            if (chkEnableDueDate.IsChecked == true && dpDueDate.SelectedDate.HasValue)
            {
                dueDate = dpDueDate.SelectedDate.Value;
            }

            CybersecurityTask newTask = new CybersecurityTask(
                txtTaskTitle.Text,
                txtTaskDescription.Text,
                dueDate
            );

            Tasks.Add(newTask);
            SaveTasks();
            MainWindow.LogActivity("Task Added", $"Added task: {newTask.Title}");

            txtTaskTitle.Clear();
            txtTaskDescription.Clear();
            dpDueDate.SelectedDate = null;
            chkEnableDueDate.IsChecked = false;
            dpDueDate.IsEnabled = false;
        }

        private void BtnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (tasksListView.SelectedItem is CybersecurityTask selectedTask)
            {
                selectedTask.IsCompleted = !selectedTask.IsCompleted;
                SaveTasks();
                MainWindow.LogActivity("Task Status Changed", $"Task '{selectedTask.Title}' set to IsCompleted: {selectedTask.IsCompleted}");
            }
            else
            {
                MessageBox.Show("Please select a task to mark complete/incomplete.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (tasksListView.SelectedItem is CybersecurityTask selectedTask)
            {
                // Fix: Use MessageBoxResult.Yes for comparison
                var result = MessageBox.Show($"Are you sure you want to delete '{selectedTask.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes) // Corrected line
                {
                    Tasks.Remove(selectedTask);
                    SaveTasks();
                    MainWindow.LogActivity("Task Deleted", $"Deleted task: {selectedTask.Title}");
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChkEnableDueDate_Checked(object sender, RoutedEventArgs e)
        {
            dpDueDate.IsEnabled = true;
        }

        private void ChkEnableDueDate_Unchecked(object sender, RoutedEventArgs e)
        {
            dpDueDate.IsEnabled = false;
            dpDueDate.SelectedDate = null;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
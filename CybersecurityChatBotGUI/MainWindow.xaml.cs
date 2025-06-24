using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CybersecurityChatBotGUI
{
    public partial class MainWindow : Window
    {
        // --- Chatbot Dictionaries and State ---
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
        {
            { "hello", new List<string> { "Hi there!", "Hello! How can I help you today?", "Greetings!" } },
            { "how are you", new List<string> { "I'm a bot, so I don't have feelings, but I'm ready to assist you!", "I'm functioning perfectly, thank you for asking!" } },
            { "what can you do", new List<string> { "I can provide cybersecurity tips, explain concepts, help you with tasks, and even test your knowledge with a mini-game!", "I'm here to help you improve your cybersecurity awareness." } },
            { "security tips", new List<string> { "Always use strong, unique passwords.", "Enable two-factor authentication (2FA) wherever possible.", "Be wary of suspicious emails and links.", "Keep your software updated." } },
            { "password", new List<string> { "A strong password should be at least 12 characters long, include a mix of uppercase and lowercase letters, numbers, and symbols.", "Never reuse passwords across different accounts." } },
            { "phishing", new List<string> { "Phishing is a cyberattack that uses disguised email, text message or telephone call to trick users into revealing sensitive information.", "Always check the sender's email address and hover over links before clicking." } },
            { "malware", new List<string> { "Malware is malicious software designed to disrupt, damage, or gain unauthorized access to a computer system.", "Keep your antivirus software updated to protect against malware." } },
            { "encryption", new List<string> { "Encryption is the process of converting information or data into a code, to prevent unauthorized access.", "It's essential for protecting sensitive data online." } },
            { "2fa", new List<string> { "Two-factor authentication (2FA) adds an extra layer of security by requiring two different methods of verification.", "It significantly reduces the risk of unauthorized access." } },
            { "update software", new List<string> { "Regularly updating your software patches security vulnerabilities.", "Outdated software is a common entry point for cyberattacks." } },
            { "privacy settings", new List<string> { "Reviewing your privacy settings on social media and other platforms helps control who sees your personal information.", "Make sure to limit data sharing to only what's necessary." } },
            { "vpn", new List<string> { "A Virtual Private Network (VPN) encrypts your internet connection and masks your IP address.", "It's useful for protecting your data on public Wi-Fi networks." } },
            { "backup data", new List<string> { "Regularly backing up your important data ensures you can recover it in case of data loss due to cyberattack, hardware failure, or accidents.", "Use cloud services or external drives for backups." } },
            { "social engineering", new List<string> { "Social engineering is the psychological manipulation of people into performing actions or divulging confidential information.", "Be suspicious of unusual requests, even if they seem to come from a trusted source." } },
            { "ransomware", new List<string> { "Ransomware is a type of malware that threatens to publish the victim's data or perpetually block access to it unless a ransom is paid.", "Regular backups are your best defense against ransomware." } },
            { "wi-fi security", new List<string> { "Always use strong encryption (WPA2/WPA3) for your home Wi-Fi.", "Avoid using public Wi-Fi for sensitive transactions without a VPN." } },
            { "incident response", new List<string> { "An incident response plan outlines steps to take in case of a cybersecurity breach.", "Knowing what to do can minimize damage and recovery time." } }
        };

        static Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>()
        {
            { "passwords", new List<string> { "Let's talk about creating strong passwords. Avoid common words and use a mix of characters.", "Password managers can be a great tool for generating and storing complex passwords." } },
            { "online safety", new List<string> { "Online safety involves being aware of the risks on the internet, such as scams, phishing, and malware.", "Always think before you click or share personal information online." } },
            { "data protection", new List<string> { "Protecting your data involves encryption, regular backups, and being careful about what information you share online.", "Understanding privacy settings is key to data protection." } },
            { "scams", new List<string> { "Scams often try to trick you into giving away money or personal information. Be skeptical of unsolicited offers or urgent requests.", "If it sounds too good to be true, it probably is." } },
            { "cybercrime", new List<string> { "Cybercrime refers to criminal activities carried out using computers or the internet.", "Understanding common cybercrimes like identity theft and fraud helps in preventing them." } }
        };

        static Dictionary<string, List<string>> sentimentResponses = new Dictionary<string, List<string>>()
        {
            { "positive", new List<string> { "That's great to hear! How else can I assist you?", "Fantastic! I'm glad to help.", "Excellent! Keep up the good work on cybersecurity." } },
            { "negative", new List<string> { "I understand this might be frustrating. What can I do to make things clearer or help resolve the issue?", "I'm sorry to hear that. Let's work through this together.", "It sounds like you're having a tough time. Don't worry, we can figure this out." } },
            { "neutral", new List<string> { "Alright. What's next?", "Understood. Is there anything specific you'd like to discuss?", "Okay. How can I proceed to help you?" } }
        };

        static Random random = new Random();
        static string? currentTopic = null;
        static Dictionary<string, string?> userMemory = new Dictionary<string, string?>();

        private SpeechSynthesizer? speechSynthesizer;

        public static ObservableCollection<ActivityLogEntry> ActivityLog { get; set; } = new ObservableCollection<ActivityLogEntry>();

        public static void LogActivity(string action, string? details = null)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ActivityLog.Insert(0, new ActivityLogEntry(action, details));

                while (ActivityLog.Count > 50)
                {
                    // FIX: Change 'ActivityData.ActivityLog' to just 'ActivityLog'
                    ActivityLog.RemoveAt(ActivityLog.Count - 1);
                }
            });
            System.Diagnostics.Debug.WriteLine($"LOG: {action} - {details}");
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeChatbot();
            LogActivity("Chatbot Started", "Application launched successfully.");
        }

        private void InitializeChatbot()
        {
            try
            {
                speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SetOutputToDefaultAudioDevice();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Speech synthesis not available: {ex.Message}. Text-to-speech will be disabled.", "Speech Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                speechSynthesizer = null;
                LogActivity("Speech Init Error", $"Speech synthesizer failed to initialize: {ex.Message}");
            }

            staticLogoTextBlock.Text = ImageDisplay.GetCybersecurityAsciiArt();

            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "robot.png");

            if (File.Exists(imagePath))
            {
                asciiArtImageTextBlock.Text = ImageDisplay.GetAsciiArtFromImage(imagePath, 100);
            }
            else
            {
                // Fallback or log if image not found
                asciiArtImageTextBlock.Text = ImageDisplay.GetCybersecurityAsciiArt(); // Use static ASCII if file not found
                LogActivity("Image Info", $"Robot image not found at {imagePath}. Using default ASCII art.");
            }

            try
            {
                AudioPlayer.PlayAudio("Greetings.wav");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing greeting: {ex.Message}", "Audio Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LogActivity("Audio Error", $"Failed to play greeting audio: {ex.Message}");
            }

            AppendChatResponse("Hello! I'm your Cybersecurity Awareness Chatbot. What's your name?");
            LogActivity("Chatbot Greeted User", "Initial greeting message displayed.");

            userInputTextBox.KeyUp += UserInputTextBox_KeyUp;
        }

        private void AppendChatResponse(string message, bool isUser = false)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Margin = new Thickness(0);

                Run prefixRun = new Run(isUser ? "You: " : "Chatbot: ");
                prefixRun.Foreground = new SolidColorBrush(isUser ? Colors.Blue : Colors.Red);
                prefixRun.FontWeight = FontWeights.Bold;
                paragraph.Inlines.Add(prefixRun);

                Run messageRun = new Run(message);
                messageRun.Foreground = new SolidColorBrush(Colors.Black);
                paragraph.Inlines.Add(messageRun);

                chatDisplayRichTextBox.Document.Blocks.Add(paragraph);

                chatDisplayRichTextBox.ScrollToEnd();

                if (!isUser)
                {
                    try
                    {
                        speechSynthesizer?.SpeakAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Speech synthesis error: {ex.Message}");
                        LogActivity("Speech Error", $"Failed to synthesize speech: {ex.Message}");
                    }
                }
            });
        }

        private void UserInputTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SendButton_Click(this, new RoutedEventArgs());
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = userInputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
            {
                return;
            }

            AppendChatResponse(userInput, isUser: true);
            ProcessUserInput(userInput);
            userInputTextBox.Clear();
        }

        private void ClearChat_Click(object sender, RoutedEventArgs e)
        {
            chatDisplayRichTextBox.Document.Blocks.Clear();
            AppendChatResponse("Chat history cleared. How can I help you next?");
            LogActivity("UI Action", "Chat history cleared via button.");
        }

        private void BtnShowTasks_Click(object sender, RoutedEventArgs e)
        {
            TaskAssistantForm taskWindow = new TaskAssistantForm(null);
            taskWindow.ShowDialog();
            LogActivity("UI Action", "User opened Task Assistant via button.");
        }

        private void BtnStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow();
            quizWindow.ShowDialog(); // ShowDialog makes the call blocking until the quiz window closes

            // After QuizWindow is closed, retrieve results
            if (quizWindow.FinalCorrectAnswers >= 0) // Check if quiz was played and score was set
            {
                string resultMessage = $"You completed the quiz! You answered {quizWindow.FinalCorrectAnswers} out of {quizWindow.TotalQuestions} questions correctly.";
                AppendChatResponse(resultMessage);
                LogActivity("Quiz Completed", $"Score: {quizWindow.FinalCorrectAnswers}/{quizWindow.TotalQuestions}");
            }
            else
            {
                LogActivity("Quiz Closed", "Quiz window was closed without a final score being recorded.");
            }
        }

        private void BtnViewActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogWindow logWindow = new ActivityLogWindow(ActivityLog);
            LogActivity("UI Action", "User opened Activity Log via button.");
            logWindow.ShowDialog();
            //LogActivity("UI Action", "User opened Activity Log via button.");
        }

        // ... (rest of ProcessUserInput and helper methods remain unchanged) ...
        private void ProcessUserInput(string input)
        {
            string response = "I'm sorry, I don't understand that. Please ask me about cybersecurity, tasks, or the quiz.";
            string lowerInput = input.ToLower();
            bool handled = false;
            DateTime? dueDate = null;

            if (currentTopic == "addTaskTitle")
            {
                userMemory["taskTitle"] = input;
                response = $"Got it. The task title is '{input}'. Can you provide a short description for this task? Or type 'no description'.";
                currentTopic = "addTaskDescription";
                handled = true;
                LogActivity("NLP Flow", $"Task title received: '{input}'");
            }
            else if (currentTopic == "addTaskDescription")
            {
                userMemory["taskDescription"] = (lowerInput.Contains("no description") || lowerInput.Contains("skip")) ? "" : input;
                response = $"Description added. Do you want to set a due date for this task? Please provide it (e.g., '2025-01-20' or 'tomorrow'). Or type 'no due date'.";
                currentTopic = "addTaskDueDate";
                handled = true;
                LogActivity("NLP Flow", $"Task description received: '{input}'");
            }
            else if (currentTopic == "addTaskDueDate")
            {
                if (lowerInput.Contains("no due date") || lowerInput.Contains("no") || lowerInput.Contains("skip"))
                {
                    dueDate = null;
                }
                else
                {
                    dueDate = ParseReminderDate(input);
                }

                if (dueDate.HasValue || lowerInput.Contains("no due date") || lowerInput.Contains("no") || lowerInput.Contains("skip"))
                {
                    string title = userMemory.ContainsKey("taskTitle") ? userMemory["taskTitle"] ?? "Untitled Task" : "Untitled Task";
                    string description = userMemory.ContainsKey("taskDescription") ? userMemory["taskDescription"] ?? "No description" : "No description";

                    CybersecurityTask newTask = new CybersecurityTask(title, description, dueDate);
                    TaskAssistantForm.AddTaskStatic(newTask);

                    response = $"Okay, I've successfully created the task: '{newTask.Title}'. " +
                               $"Due: {newTask.DueDate?.ToShortDateString() ?? "None"}. " +
                               $"You can view and manage all your tasks by clicking 'View My Tasks' or by asking me to 'show tasks'.";

                    LogActivity("Task Created via NLP", $"Title: {newTask.Title}, Due: {newTask.DueDate?.ToShortDateString() ?? "None"}");
                    userMemory.Clear();
                    currentTopic = null;
                    handled = true;
                }
                else
                {
                    response = "I couldn't understand the due date. Please try again (e.g., '2025-01-20', 'tomorrow', 'next week', or 'no due date').";
                    LogActivity("NLP Flow Error", $"Invalid due date input for task: '{input}'");
                    AppendChatResponse(response);
                    return;
                }
            }
            else if (lowerInput.Contains("add task") || lowerInput.Contains("create task") || lowerInput.Contains("new task"))
            {
                response = "Okay, let's create a new task. What is the title of the task?";
                currentTopic = "addTaskTitle";
                handled = true;
                LogActivity("NLP Trigger", "User initiated task creation.");
            }
            else if (lowerInput.Contains("view tasks") || lowerInput.Contains("show tasks") || lowerInput.Contains("my tasks") || lowerInput.Contains("open task assistant"))
            {
                TaskAssistantForm taskWindow = new TaskAssistantForm(null);
                taskWindow.ShowDialog();
                response = "Opening your task assistant.";
                handled = true;
                LogActivity("NLP Trigger", "User requested to view tasks.");
                currentTopic = null;
            }
            else if (lowerInput.Contains("set reminder") || lowerInput.Contains("create reminder"))
            {
                LogActivity("Command Recognition", "User requested 'Set Reminder'. Basic reminder acknowledgment.");
                response = "Got it! While I don't have a full reminder system yet, I can help you add tasks with due dates in the Task Assistant. Would you like to 'add task' instead?";
                handled = true;
                currentTopic = null;
            }
            else if (lowerInput.Contains("start quiz") || lowerInput.Contains("play quiz") || lowerInput.Contains("quiz me") || lowerInput.Contains("cybersecurity quiz"))
            {
                // This block is now handled by the BtnStartQuiz_Click directly for the button.
                // If you want to keep NLP trigger for quiz, you can call BtnStartQuiz_Click(null, null);
                // or replicate its logic here. For simplicity, let's just respond if triggered via NLP.
                response = "Launching the cybersecurity quiz!"; // This response will be overwritten by the score message.
                handled = true; // Mark as handled but let the button handler do the work
                LogActivity("NLP Trigger", "User initiated the cybersecurity quiz.");
                currentTopic = null;
            }
            else if (lowerInput.Contains("show activity log") || lowerInput.Contains("view activity log") || lowerInput.Contains("what have you done for me"))
            {
                ActivityLogWindow logWindow = new ActivityLogWindow(ActivityLog);
                logWindow.ShowDialog();
                response = "Here's your activity log.";
                handled = true;
                LogActivity("NLP Trigger", "User requested to view activity log.");
                currentTopic = null;
            }
            else if (lowerInput.Contains("clear chat") || lowerInput.Contains("clear screen"))
            {
                chatDisplayRichTextBox.Document.Blocks.Clear();
                AppendChatResponse("Chat history cleared. How can I help you next?");
                handled = true;
                LogActivity("UI Action", "Chat history cleared.");
                currentTopic = null;
            }

            if (!handled)
            {
                if (!userMemory.ContainsKey("name") && lowerInput.Contains("my name is"))
                {
                    int nameIndex = lowerInput.IndexOf("my name is") + "my name is".Length;
                    string name = input.Substring(nameIndex).Trim();
                    userMemory["name"] = name;
                    response = $"Nice to meet you, {name}! How can I help you with cybersecurity today?";
                    handled = true;
                    LogActivity("User Interaction", $"User provided name: {name}");
                }
                else if (userMemory.ContainsKey("name") && lowerInput.Contains("call me"))
                {
                    int nameIndex = lowerInput.IndexOf("call me") + "call me".Length;
                    string name = input.Substring(nameIndex).Trim();
                    userMemory["name"] = name;
                    response = $"Got it, I'll call you {name} from now on!";
                    handled = true;
                    LogActivity("User Interaction", $"User changed name to: {name}");
                }
                else if (userMemory.ContainsKey("name") && (lowerInput.Contains("hello") || lowerInput.Contains("hi")))
                {
                    response = $"Hello {userMemory["name"]}! How can I assist you today?";
                    handled = true;
                }
                else if (lowerInput.Contains("how are you"))
                {
                    response = "I'm doing well, thank you for asking! How can I assist you with cybersecurity today?";
                    handled = true;
                }
            }

            if (!handled)
            {
                foreach (var kvp in topicResponses)
                {
                    if (lowerInput.Contains(kvp.Key))
                    {
                        response = kvp.Value[random.Next(kvp.Value.Count)];
                        handled = true;
                        LogActivity("Topic Match", $"Matched topic: '{kvp.Key}' for input: '{input}'");
                        break;
                    }
                }
            }

            if (!handled)
            {
                foreach (var kvp in keywordResponses)
                {
                    if (lowerInput.Contains(kvp.Key))
                    {
                        response = kvp.Value[random.Next(kvp.Value.Count)];
                        handled = true;
                        LogActivity("Keyword Match", $"Matched keyword: '{kvp.Key}' for input: '{input}'");
                        break;
                    }
                }
            }

            if (!handled)
            {
                string sentiment = DetectSentiment(input);
                if (sentimentResponses.TryGetValue(sentiment, out List<string>? responsesList))
                {
                    response = responsesList[random.Next(responsesList.Count)];
                    handled = true;
                    LogActivity("Sentiment Detected", $"Sentiment: '{sentiment}' for input: '{input}'");
                }
            }

            // Only append a response if it hasn't been handled already (e.g., by a dialog showing)
            // Or if it's the specific case for addTaskDueDate that needs a response from here
            if (handled && !string.IsNullOrEmpty(response) && !(currentTopic == "addTaskDueDate" && !(dueDate.HasValue || lowerInput.Contains("no due date") || lowerInput.Contains("no") || lowerInput.Contains("skip"))))
            {
                if (!(lowerInput.Contains("start quiz") || lowerInput.Contains("play quiz"))) // Don't append if quiz is starting, score will be appended later
                {
                    AppendChatResponse(response);
                }
            }
            else if (!handled)
            {
                AppendChatResponse(response);
            }
        }
        private DateTime? ParseReminderDate(string input)
        {
            string lowerInput = input.ToLower().Trim();

            if (DateTime.TryParse(input, out DateTime exactDate))
            {
                return exactDate;
            }

            DateTime today = DateTime.Today;

            if (lowerInput.Contains("today"))
                return today;
            else if (lowerInput.Contains("tomorrow"))
                return today.AddDays(1);
            else if (lowerInput.Contains("next week"))
                return today.AddDays(7);
            else if (lowerInput.Contains("next month"))
                return today.AddMonths(1);
            else if (lowerInput.Contains("in") && lowerInput.Contains("day"))
            {
                var words = lowerInput.Split(' ');
                for (int i = 0; i < words.Length - 1; i++)
                {
                    if (words[i] == "in" && int.TryParse(words[i + 1], out int days))
                    {
                        return today.AddDays(days);
                    }
                }
            }

            return null;
        }

        private string DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            string[] positiveWords = { "good", "great", "excellent", "awesome", "fantastic", "love", "like", "happy", "pleased", "satisfied", "wonderful", "amazing", "perfect" };
            string[] negativeWords = { "bad", "terrible", "awful", "hate", "dislike", "angry", "frustrated", "annoyed", "upset", "sad", "disappointed", "horrible", "worst" };

            int positiveCount = 0;
            int negativeCount = 0;

            foreach (string word in positiveWords)
            {
                if (lowerInput.Contains(word))
                    positiveCount++;
            }

            foreach (string word in negativeWords)
            {
                if (lowerInput.Contains(word))
                    negativeCount++;
            }

            if (positiveCount > negativeCount)
                return "positive";
            else if (negativeCount > positiveCount)
                return "negative";
            else
                return "neutral";
        }
    }
}
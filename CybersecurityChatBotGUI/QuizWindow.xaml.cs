using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CybersecurityChatBotGUI
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private List<QuizQuestion> QuizQuestions;
        private int currentQuestionIndex = 0;
        private int correctAnswersCount = 0;

        // New properties to expose the quiz results
        public int FinalCorrectAnswers { get; private set; }
        public int TotalQuestions { get; private set; }

        public QuizWindow()
        {
            InitializeComponent();
            InitializeQuizQuestions();
            DisplayQuestion();
            this.DataContext = this;
        }

        private void InitializeQuizQuestions()
        {
            QuizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What is phishing?",
                    new List<string> { "A type of malware", "An attempt to trick you into revealing information", "A method for encrypting data", "A strong password technique" },
                    "An attempt to trick you into revealing information"
                ),
                new QuizQuestion(
                    "What does 2FA stand for?",
                    new List<string> { "Two-Factor Authorization", "Two-Factor Authentication", "Two-Form Application", "Two-File Access" },
                    "Two-Factor Authentication"
                ),
                new QuizQuestion(
                    "Why is it important to keep software updated?",
                    new List<string> { "To get new features", "To improve performance", "To patch security vulnerabilities", "To change the user interface" },
                    "To patch security vulnerabilities"
                ),
                 new QuizQuestion(
                    "What is a VPN used for?",
                    new List<string> { "To speed up internet connection", "To encrypt internet connection and mask IP", "To download files faster", "To block ads" },
                    "To encrypt internet connection and mask IP"
                ),
                new QuizQuestion(
                    "What is ransomware?",
                    new List<string> { "Software that optimizes your PC", "Malware that encrypts files and demands payment", "A tool for secure communication", "A type of strong antivirus" },
                    "Malware that encrypts files and demands payment"
                ),
                // Adding 10 more true/false QuizQuestions
                new QuizQuestion(
                    "True or False: Using the same password for all your online accounts is a recommended security practice.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: Public Wi-Fi networks are generally safe for sensitive transactions without a VPN.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: Antivirus software is enough to protect you from all types of cyber threats.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: It is safe to click on links in suspicious emails if they look like they are from a known company.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: Two-Factor Authentication (2FA) adds an extra layer of security to your accounts.",
                    new List<string> { "True", "False" },
                    "True"
                ),
                new QuizQuestion(
                    "True or False: Phishing attacks only happen via email.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: Regularly backing up your data helps protect against data loss from ransomware attacks.",
                    new List<string> { "True", "False" },
                    "True"
                ),
                new QuizQuestion(
                    "True or False: Strong passwords should be easy to remember and short.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: Social engineering relies on technical vulnerabilities rather than human manipulation.",
                    new List<string> { "True", "False" },
                    "False"
                ),
                new QuizQuestion(
                    "True or False: Keeping your operating system and applications updated helps fix security flaws.",
                    new List<string> { "True", "False" },
                    "True"
                )
            };
            // Shuffle questions for variety (optional)
            QuizQuestions = QuizQuestions.OrderBy(a => System.Guid.NewGuid()).ToList();

            // Set total questions count here
            TotalQuestions = QuizQuestions.Count;
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex < QuizQuestions.Count)
            {
                QuizQuestion question = QuizQuestions[currentQuestionIndex];
                QuestionTextBlock.Text = question.Question;
                OptionsPanel.Children.Clear();
                FeedbackTextBlock.Text = string.Empty;

                foreach (string option in question.Options)
                {
                    RadioButton radioButton = new RadioButton
                    {
                        Content = option,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    OptionsPanel.Children.Add(radioButton);
                }
            }
            else
            {
                // Quiz finished - Set the final score here before closing
                FinalCorrectAnswers = correctAnswersCount;
                TotalQuestions = QuizQuestions.Count; // Ensure this is set
                QuestionTextBlock.Text = $"Quiz Completed! You got {FinalCorrectAnswers} out of {TotalQuestions} questions correct.";
                OptionsPanel.Children.Clear();
                FeedbackTextBlock.Text = "Great job!";
                // You might want to disable the "Submit" and "Next" buttons here
                // Example: SubmitButton.IsEnabled = false; NextButton.IsEnabled = false;
            }
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex >= QuizQuestions.Count) return;

            QuizQuestion currentQuestion = QuizQuestions[currentQuestionIndex];
            string? selectedAnswer = null;

            foreach (var child in OptionsPanel.Children)
            {
                if (child is RadioButton radioButton && radioButton.IsChecked == true)
                {
                    selectedAnswer = radioButton.Content?.ToString();
                    break;
                }
            }

            if (selectedAnswer != null)
            {
                if (selectedAnswer == currentQuestion.CorrectAnswer)
                {
                    FeedbackTextBlock.Text = "Correct!";
                    FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                    correctAnswersCount++;
                }
                else
                {
                    FeedbackTextBlock.Text = $"Incorrect. The correct answer was: {currentQuestion.CorrectAnswer}";
                    FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
            else
            {
                FeedbackTextBlock.Text = "Please select an answer.";
                FeedbackTextBlock.Foreground = System.Windows.Media.Brushes.Orange;
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestionIndex < QuizQuestions.Count - 1)
            {
                currentQuestionIndex++;
                DisplayQuestion();
            }
            else
            {
                // This is the last question. When "Next" is clicked after the last question,
                // we should finalize the score and display the completion message.
                currentQuestionIndex++; // Increment to go beyond the last question index
                DisplayQuestion(); // This will trigger the "Quiz Completed!" logic
                // No need to close the window immediately, user might want to see the final score.
            }
        }

        private void FinishQuiz_Click(object sender, RoutedEventArgs e)
        {
            // Ensure scores are finalized before closing if the quiz wasn't fully completed via "Next" button
            // If the user clicks "Finish Quiz" before going through all questions:
            if (currentQuestionIndex < QuizQuestions.Count)
            {
                // If they finish early, their score is based on questions answered so far.
                FinalCorrectAnswers = correctAnswersCount;
                TotalQuestions = QuizQuestions.Count; // Still out of total available questions
            }
            this.Close();
        }
    }
}
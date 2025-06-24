# PROG6221_ST104561096_PO3_PART3
## Cybersecurity Awareness Chatbot GUI

### Project Overview

The **Cybersecurity Awareness Chatbot** is a desktop application built with Windows Presentation Foundation (WPF). It is designed to promote cybersecurity awareness by engaging users through interactive conversations, assisting in task management, and testing their knowledge through quizzes.

---

### Features

* **💬 Interactive Chat Interface**
  Engage with a chatbot that responds to keywords and sentiments related to cybersecurity.

* **📚 Topic-Based Information**
  Learn about common cybersecurity topics such as:

  * Phishing
  * Malware
  * Passwords
  * Encryption
  * VPNs
  * Firewalls
  * Two-Factor Authentication (2FA)
  * Software Updates
  * Ransomware
  * Social Engineering

* **🗒️ Task Assistant**
  Manage personal cybersecurity-related tasks:

  * Add tasks with title, description, and optional due date
  * Mark tasks complete/incomplete
  * View or delete tasks

  > Tasks are saved to a local `tasks.json` file

* **🧠 Cybersecurity Quiz**
  Test your knowledge through an interactive quiz. The score is displayed in the chat window after completion.

* **📜 Activity Log**
  View a log of all interactions and major application actions in a separate window.

* **🗣️ Text-to-Speech**
  Optional voice responses using Windows' speech synthesizer.

* **🤖 ASCII Art Display**
  Dynamically displays ASCII art in the chat interface (main title and robot image).

* **🔊 Audio Greetings**
  Plays a greeting sound upon startup.

---

### Technologies Used

* **C#** – Primary programming language
* **WPF (.NET Framework/.NET Core)** – GUI framework
* **System.Speech.Synthesis** – For text-to-speech
* **System.Media.SoundPlayer** – For audio playback
* **System.Drawing.Common** – Used for image processing to create ASCII art
* **Newtonsoft.Json** – JSON serialization/deserialization
* **ObservableCollection<T>** – Data binding in UI

---

### Setup and Installation

#### Prerequisites

* Visual Studio 2019 or newer
* .NET Desktop Development workload installed

#### Steps

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/MusaJanda/PROG6221_ST104561096_PO3_PART3.git
   cd CybersecurityChatbotGUI
   ```

2. **Open the Project in Visual Studio:**

   * Launch Visual Studio
   * Go to `File > Open > Project/Solution...`
   * Open `CybersecurityChatbotGUI.sln`

3. **Restore NuGet Packages:**

   * Visual Studio should prompt you to restore missing packages
   * If not: Right-click the solution > `Restore NuGet Packages`
   * Ensure `Newtonsoft.Json` and `System.Drawing.Common` are installed

4. **Add System.Speech Reference:**

   * Right-click the project > `Add > Reference...`
   * Go to `Assemblies > Framework` and check `System.Speech`

5. **Place Required Assets:**

   * Create an `Images/` folder in the project root
   * Add `robot.png` inside `Images/`
   * Add `Greetings.wav` to the root folder
   * Set both files' **"Copy to Output Directory"** to **"Copy if newer"**

6. **Build the Project:**

   * `Build > Build Solution` (or press `Ctrl+Shift+B`)

7. **Run the Application:**

   * Press `F5` or click the **Start** button

---

### Usage

#### Chat Commands (Type in Input Box)

* `Hello` / `Hi`
* `What is phishing?`
* `Tell me about malware`
* `Add task`
* `View tasks` / `Open task assistant`
* `Start quiz` / `Cybersecurity quiz`
* `Show activity log`
* `Clear chat`

#### UI Buttons (Bottom of Window)

* **View My Tasks**
* **Start Quiz**
* **View Activity Log**
* **Clear Chat History**

---

### Project Structure

| File / Folder                    | Description               |
| -------------------------------- | ------------------------- |
| `MainWindow.xaml` / `.cs`        | Main interface            |
| `TaskAssistantForm.xaml` / `.cs` | Task management UI        |
| `QuizWindow.xaml` / `.cs`        | Cybersecurity quiz        |
| `ActivityLogWindow.xaml` / `.cs` | Displays the activity log |
| `CybersecurityTask.cs`           | Task model                |
| `ActivityLogEntry.cs`            | Log entry model           |
| `BoolToStatusConverter.cs`       | Converts task status      |
| `ImageDisplay.cs`                | ASCII image generator     |
| `AudioPlayer.cs`                 | Plays startup greeting    |
| `tasks.json`                     | Stores user tasks         |
| `Images/robot.png`               | ASCII image source        |
| `Greetings.wav`                  | Startup greeting sound    |

---

### Contributing

Contributions are welcome and appreciated!

1. **Fork the Repository**
   Click the **Fork** button in GitHub.

2. **Create a Feature Branch**

   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Make Your Changes**

4. **Commit and Push**

   ```bash
   git add .
   git commit -m "Add your message here"
   git push origin feature/your-feature-name
   ```

5. **Create a Pull Request**
   Open a pull request from your fork on GitHub.

**Note:** Make sure to follow the existing coding style and structure. Include detailed comments where necessary.

---

### License

This project is licensed under the MIT License.
You are free to use, modify, and distribute this software, as long as the original license is included.

See the [LICENSE](LICENSE) file in the repository for full details.

---

### Author

**Musa Janda**
Cybersecurity Enthusiast & Developer
GitHub: [@MusaJanda](https://github.com/MusaJanda)

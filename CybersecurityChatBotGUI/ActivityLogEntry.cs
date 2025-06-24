using System;

namespace CybersecurityChatBotGUI
{
    public class ActivityLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }

        public ActivityLogEntry(string action, string? details = null)
        {
            Timestamp = DateTime.Now;
            Action = action;
            Details = details ?? string.Empty; // Ensure Details is not null
        }

        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Action}: {Details}";
        }
    }
}
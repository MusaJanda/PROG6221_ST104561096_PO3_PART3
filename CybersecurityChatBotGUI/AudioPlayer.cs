using System;
using System.IO;
using System.Media;

namespace CybersecurityChatBotGUI
{
    public static class AudioPlayer
    {
        public static void PlayAudio(string fileName)
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.Play();
                }
                else
                {
                    throw new FileNotFoundException($"Audio file not found: {audioPath}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error playing audio: {ex.Message}", ex);
            }
        }

        public static void PlayAudioSync(string fileName)
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.PlaySync();
                }
                else
                {
                    throw new FileNotFoundException($"Audio file not found: {audioPath}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error playing audio: {ex.Message}", ex);
            }
        }
    }
}
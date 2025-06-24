using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace CybersecurityChatBotGUI
{
    public static class ImageDisplay
    {
        public static string GetCybersecurityAsciiArt()
        {
            return @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
╚██████╗   ██║   ██████╔╝███████╗██║  ██║
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝
    
███████╗███████╗ ██████╗██╗   ██╗██████╗ ██╗████████╗██╗   ██╗
██╔════╝██╔════╝██╔════╝██║   ██║██╔══██╗██║╚══██╔══╝╚██╗ ██╔╝
███████╗█████╗  ██║     ██║   ██║██████╔╝██║   ██║    ╚████╔╝ 
╚════██║██╔══╝  ██║     ██║   ██║██╔══██╗██║   ██║     ╚██╔╝  
███████║███████╗╚██████╗╚██████╔╝██║  ██║██║   ██║      ██║   
╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝      ╚═╝   
                                                              
            🔒 CYBERSECURITY ASSISTANT 🔒
            Your Digital Guardian Bot
";
        }

        public static string GetAsciiArtFromImage(string imagePath, int width = 80)
        {
            try
            {
                if (!File.Exists(imagePath))
                {
                    return "🤖 CHATBOT 🤖\n" +
                           "┌─────────────┐\n" +
                           "│  ◉     ◉   │\n" +
                           "│      ▼      │\n" +
                           "│   \\─────/   │\n" +
                           "│             │\n" +
                           "└─────────────┘\n" +
                           "  I'm ready to help!";
                }

                using (Bitmap bitmap = new Bitmap(imagePath))
                {
                    return ConvertToAscii(bitmap, width);
                }
            }
            catch (Exception)
            {
                // Return a simple ASCII robot if image processing fails
                return "🤖 CHATBOT 🤖\n" +
                       "┌─────────────┐\n" +
                       "│  ◉     ◉   │\n" +
                       "│      ▼      │\n" +
                       "│   \\─────/   │\n" +
                       "│             │\n" +
                       "└─────────────┘\n" +
                       "  I'm ready to help!";
            }
        }

        private static string ConvertToAscii(Bitmap bitmap, int width)
        {
            StringBuilder result = new StringBuilder();
            string asciiChars = "@%#*+=-:. ";

            int height = (int)(bitmap.Height * width / (double)bitmap.Width * 0.5); // Adjust for character aspect ratio

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixelX = x * bitmap.Width / width;
                    int pixelY = y * bitmap.Height / height;

                    Color pixel = bitmap.GetPixel(pixelX, pixelY);
                    int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    int asciiIndex = grayValue * (asciiChars.Length - 1) / 255;

                    result.Append(asciiChars[asciiIndex]);
                }
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
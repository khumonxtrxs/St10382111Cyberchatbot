using System;
using System.IO;
using System.Media;
using System.Speech.Synthesis;

namespace St10382111Cyberchatbot.Utilities
{
    public static class HelperSpeech
    {
        public static void PlayGreeting()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "CyrexChat.wav");

            if (File.Exists(filePath))
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            else
            {
                Console.WriteLine("Warning: CyrexChat.wav not found.");
            }
        }

        public static void SpeakText(string text)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.Rate = 0;
                synth.Speak(text);
            }
        }
    }
}

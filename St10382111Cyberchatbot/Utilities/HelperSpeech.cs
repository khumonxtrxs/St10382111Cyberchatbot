using System;
using System.IO;
using System.Media;
using System.Speech.Synthesis;

namespace St10382111Cyberchatbot.Utilities{

    public static class HelperSpeech
    {
        public static void PlayGreeting()
        {
            string filePath = "C:\\Users\\Khumo Twala\\source\\repos\\St10382111Cyberchatbot\\St10382111Cyberchatbot\\CyrexChat.wav";
            if (File.Exists(filePath))
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            else
            {
                Console.WriteLine("Warning: Cyrex.wav not found.");
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
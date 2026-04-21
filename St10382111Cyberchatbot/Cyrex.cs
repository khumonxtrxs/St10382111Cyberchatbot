using St10382111Cyberchatbot.Utilities;
using System;

namespace St10382111Cyberchatbot
{

 
        public static class Cyrex
        {
            public static void Start(string name)
            {
                while (true)
                {
                    Console.Write("\nYou: ");
                    string input = Console.ReadLine();
                    if (input?.ToLower() == "exit") break;

                    string response = GetResponse(input);
                    Console.WriteLine($"Cyrex: {response}");
                    HelperSpeech.SpeakText(response);
                }
            }

        public static string GetResponse(string input)
        {
            string clean = input?.ToLower() ?? "";

            if (clean.Contains("how are you")) return "I'm just a code, haha haha but i am funtioning well thank you.";
            if (clean.Contains("purpose")) return "My purpose is to answer your cybersecurity questions.";
            if (clean.Contains("what can i ask")) return "You can ask me about passwords, phishing and safe browsing.";
            if (clean.Contains("password")) return "Use strong, unique passwords.";
            if (clean.Contains("phishing")) return "Never click links from unknown emails.";
            if (clean.Contains("safe browsing")) return "Make use of HTTPS, and enamble two (2) factor authentication and avoid links that asr suspicious.";

            return "I'm not sure about that. Can you rephrase?";
        }
    }
}   
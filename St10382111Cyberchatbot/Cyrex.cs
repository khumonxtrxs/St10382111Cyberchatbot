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
                if (input.Contains("password")) return "Use strong passwords.";
                return "I'm listening.";
            }
        }
    }
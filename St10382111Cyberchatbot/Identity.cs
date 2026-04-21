
using St10382111Cyberchatbot.Utilities;
using System;

namespace St10382111Cyberchatbot
{
   
        public static class Identity
        {
            public static void NamePrompt()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WHAT IS YOUR NAME?");
                string name = Console.ReadLine();

                Console.ResetColor();
                AsciiHelper.SyncSpeakAndType($"Nice to meet you {name}. Let's chat.");
                Cyrex.Start(name);
            }
        }
    }
using St10382111Cyberchatbot.Utilities;
using System;

namespace St10382111Cyberchatbot
{
    public static class Identity
    {
        // Stores the user's name so Cyrex can personalise responses
        public static string UserName { get; private set; } = "Guest";

        public static void NamePrompt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WHAT IS YOUR NAME?");
            string name = Console.ReadLine() ?? "Guest";

            Console.ResetColor();
            AsciiHelper.SyncSpeakAndType($"Nice to meet you {name}. Let's chat.");
            SetName(name);
            Cyrex.Start(name);
        }

        // Called by ChatWindow when the user types their name in the GUI
        public static void SetName(string name)
        {
            UserName = string.IsNullOrWhiteSpace(name) ? "Guest" : name.Trim();
        }
    }
}
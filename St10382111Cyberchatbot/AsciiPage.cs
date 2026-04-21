using St10382111Cyberchatbot.Utilities;
using System;

namespace St10382111Cyberchatbot
{
  

        public static class AsciiPage
        {
            public static void Show()
            {
                HelperSpeech.PlayGreeting();

                Console.Clear();
                Console.Title = "Cyrex Chat";
                Console.ForegroundColor = ConsoleColor.Green;

                string asciiart = @"  _______       _______  ________   __   _____ _   _       _______ ____   ____ _______ 
 / ____\ \     / / __ \|  ____\ \ / /  / ____| |  | |   /\__   __| _ \ / __ \__   __|
| |     \ \_/ /| |__) | |__   \ V /  | |    | |__| |  /  \  | |  | |_) | |  | | | |   
| |      \   / |  _  /|  __|   > <   | |    |  __  | / /\ \ | |  |  _ <| |  | | | |   
| |____   | |  | | \ \| |____ / . \  | |____| |  | |/ ____ \| |  | |_) | |__| | | |   
 \_____|  |_|  |_|  \_\______/_/ \_\  \_____|_|  |_/_/    \_\_|  |____/ \____/  |_|   ";

                AsciiHelper.DrawBorderBox(asciiart);
                Console.WriteLine();
                AsciiHelper.SyncSpeakAndType("Hello! I am Cyrex. Making Cybersecurity better for you.");

                Console.ResetColor();
            }
        }
    }
using System;
using System.Threading;

namespace St10382111Cyberchatbot.Utilities
{
   
        public static class AsciiHelper
        {
            public static void DrawBorderBox(string content)
            {
                int width = 80;
                string horizontal = new string('*', width);
                Console.WriteLine(horizontal);
                string[] lines = content.Split('\n');
                foreach (string line in lines)
                {
                    string padded = line.PadRight(width - 4);
                    Console.WriteLine("* " + padded + " *");
                }
                Console.WriteLine(horizontal);
            }

            public static void TypeEffect(string message)
            {
                foreach (char c in message)
                {
                    Console.Write(c);
                    Thread.Sleep(30);
                }
                Console.WriteLine();
            }

            public static void SyncSpeakAndType(string message)
            {
                Thread typingThread = new Thread(() => TypeEffect(message));
                typingThread.Start();
                HelperSpeech.SpeakText(message);
            }
        }
    }

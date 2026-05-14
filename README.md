Cyrex Cybersecurity Awareness Chatbot
St10382111 - Part 2 – GUI, Dynamic Responses, Sentiment Detection & Memory

Project Overview
Cyrex is a Cybersecurity Awareness Chatbot built in C# using WPF . The chatbot informs on cybersecurity topics using a graphical interface, featuring keyword recognition, memory, sentiment detection, and text-to-speech voice output.

How to Run

Open Visual Studio
Open the solution file St10382111Cyberchatbot.slnx
Press F5 to build and run
The Cyrex chat window will open, enter your name to begin


Requirements: .NET 10.0 (Windows), Windows OS


Project Structure
St10382111Cyberchatbot/
│
├── Assets/
│   └── CyrexChat.wav           ← Startup greeting sound
│
├── Models/
│   └── Sentiment.cs            ← SentimentResult &  SentimentDetector class
│
├── Utilities/
│   ├── AsciiHelper.cs          ← ASCII art border and type effect from part 1
│   └── HelperSpeech.cs         ← WAV playback and text-to-speech
│
├── App.xaml / App.xaml.cs      ← WPF application entry point
├── ChatWindow.xaml             ← GUI layout
├── ChatWindow.xaml.cs          ← Window logic and chat bubble rendering
├── AsciiPage.cs                ← ASCII banner class
├── Cyrex.cs                    ← Chatbot logic
├── Identity.cs                 ← User name prompt and storage
└── St10382111Cyberchatbot.csproj

<img width="840" height="558" alt="Screenshot 2026-05-14 142951" src="https://github.com/user-attachments/assets/eb4841ca-3eb8-47ec-8fa7-50f932e03fb1" />


Features Implemented

1. GUI Design (WPF)
- Dark cybersecurity themed interface
- ASCII art logo displayed in the header
- Colour-coded chat bubbles for user and Cyrex
- Timestamps on every message
- Typing indicator while Cyrex processes input
- Voice toggle button (mute/unmute)
-Live sentiment and user name display

2. Keyword Recognition
Cyrex recognises:
- password - phishing - scam - privacy
- malware - safe browsing - vpn - two factor

3. Random Responses
Each keyword topic has a List<string> of 4 different responses.
A Random object selects one each time, keeping conversations varied and engaging.

5. Conversation Flow
Type these prompts to continue the last topic without starting over:
- "tell me more" - "explain more"
- "give me another tip" - "more details"
- "continue"

5. Memory and Recall

Say "My name is [name]" → Cyrex remembers and uses your name
Say "I'm interested in [topic]" → Cyrex remembers your interest and references it in later responses
Ask "What's my name?" or "What do I like?" → Cyrex recalls stored information

Example:
User: "I'm interested in privacy."
Cyrex: "Great! I'll remember that you're interested in privacy. It's a crucial part of staying safe online."

6. Sentiment Detection
Cyrex detects the emotional tone of your message and adjusts its response:
<img width="776" height="300" alt="Screenshot 2026-05-14 143830" src="https://github.com/user-attachments/assets/17a552e5-e841-42eb-8f77-98330b716d3f" />


7. Error Handling
Default responses for unrecognised input


8. Code Structure (OOP)

Dictionary<string, List<string>> for keyword-to-response mapping
SentimentDetector static class for tone detection
SentimentResult enum for clean switch expressions
Separated concerns across classes: Cyrex, Identity, HelperSpeech, AsciiHelper


9. Voice Features

Startup WAV plays when the application launches (CyrexChat.wav)
Text-to-speech reads every Cyrex response aloud using System.Speech.Synthesis
Voice toggle button in the input bar to mute & unmute at any time


GitHub Releases
Release1 [Part 1 – Console chatbot with ASCII art, voice, and basic keyword responses] 
Release2 [Part 2 – WPF GUI, sentiment detection, memory, random responses, conversation flow]

References

Microsoft WPF Documentation: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/
System.Speech.Synthesis: https://learn.microsoft.com/en-us/dotnet/api/system.speech.synthesis
Cybersecurity tips sourced from general best practices (SANS Institute, NCSC)

## Author
* Khumo Twala St10382111

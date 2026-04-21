# Cyrex Cyber Chatbot

Cyrex is a C# console-based cybersecurity awareness chatbot designed to provide interactive, voice-enabled assistance to users. It features custom ASCII art, text-to-speech capabilities, and a robust architecture for continuous integration.

## Features
* **Interactive UI:** Dynamic ASCII-art landing page.
* **Voice Synthesis:** Uses `System.Speech` to provide audible responses.
* **Greeting System:** Plays custom `.wav` files for an immersive introduction.
* **Cybersecurity Education:** Provides basic advice on passwords, phishing, and online safety.

## Getting Started

### NuGet Package: `System.Windows.Extensions` (Required for SoundPlayer).

### Setup
1. Clone the repository.
2. Place your `Cyrex.wav` file in the project root directory.
3. In Visual Studio, ensure the `Cyrex.wav` property "Copy to Output Directory" is set to "Copy always".
4. Restore NuGet packages and rebuild the solution.

## Continuous Integration (CI)
This project utilizes GitHub Actions to ensure code quality with every commit. The workflow automatically restores dependencies and builds the project to ensure no syntax errors exist.

**Build Status:**
<img width="1396" height="376" alt="Screenshot 2026-04-21 233530" src="https://github.com/user-attachments/assets/caada170-6380-4895-a929-6cd0ac063846" />


*(The screenshot above confirms the latest build passed successfully via GitHub Actions.)*

## Architecture
* **`AsciiPage.cs`**: Handles the initial user interface.
* **`Cyrex.cs`**: The core logic engine and chatbot loop.
* **`Identity.cs`**: Manages user input and identity verification.
* **`Utilities/`**: Contains helper classes for Speech Synthesis (`HelperSpeech`) and ASCII rendering (`AsciiHelper`).

## Authors
* Khumo Twala St10382111

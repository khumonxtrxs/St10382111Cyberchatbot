using St10382111Cyberchatbot.Utilities;
using St10382111Cyberchatbot.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace St10382111Cyberchatbot
{

    /// ChatWindow is the WPF GUI for Part 2 and it uses the same Cyrex class (logic), Identity class (name prompt),
    
    public partial class ChatWindow : Window
    {
        // tracks whether if still waiting for the user's name & voice on/off
        private bool _awaitingName = true;
        private bool _voiceEnabled = true;

        private static readonly SolidColorBrush BotBubbleBg   = new(Color.FromRgb(17, 30, 50));
        private static readonly SolidColorBrush UserBubbleBg  = new(Color.FromRgb(0, 80, 45));
        private static readonly SolidColorBrush BotTextColour = new(Color.FromRgb(0, 212, 255));
        private static readonly SolidColorBrush UserTextColour= new(Color.FromRgb(226, 232, 240));
        private static readonly SolidColorBrush TimeColour    = new(Color.FromRgb(55, 80, 100));

        public ChatWindow()
        {
            InitializeComponent();

            Task.Run(() => HelperSpeech.PlayGreeting());

            Loaded += OnLoaded;
        }

  ─
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Show the ASCII art welcome
            AddBotBubble("Hello! I am Cyrex. Making Cybersecurity better for you.", "#00FF88");
            AddBotBubble("WHAT IS YOUR NAME?", "#FFD700");
            SetStatus("Waiting for your name…");
        }

    
        // INPUT EVENTS
      
        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(TxtInput.Text))
                HandleSend();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e) => HandleSend();

        private void BtnVoice_Click(object sender, RoutedEventArgs e)
        {
            _voiceEnabled = !_voiceEnabled;
            BtnVoice.Content = _voiceEnabled ? "🔊" : "🔇";
            SetStatus(_voiceEnabled ? "Voice enabled." : "Voice muted.");
        }

      ─
        // MAIN HANDLER
     
        private async void HandleSend()
        {
            string userText = TxtInput.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            TxtInput.Clear();
            AddUserBubble(userText);

            //  Name collection and store name
            if (_awaitingName)
            {
                _awaitingName = false;
                Identity.SetName(userText);          

                string greeting = $"Nice to meet you {userText}. Let's chat.";
                AddBotBubble(greeting, "#00FF88");
                SpeakAsync(greeting);

                TxtUserName.Text = $"User: {userText}";
                AddBotBubble("You can ask me about passwords, phishing, scams, privacy, malware, safe browsing, VPNs, and more.");
                SetStatus("Ready — ask me anything about cybersecurity.");
                return;
            }

            //  Conversation and calls Cyrex.GetResponse 
            SetStatus("Cyrex is thinking…");
            var typing = AddTypingIndicator();

            string response = await Task.Run(() => Cyrex.GetResponse(userText));
            SentimentResult sentiment = SentimentDetector.Detect(userText);

            ChatPanel.Children.Remove(typing);

            AddBotBubble(response);
            UpdateSentimentLabel(sentiment);
            SpeakAsync(response);
            SetStatus("Ready.");
            ScrollToBottom();
        }

        
        // BUBBLE HELPERS
        
        private void AddUserBubble(string text)
        {
            ChatPanel.Children.Add(MakeBubble(text, UserBubbleBg, UserTextColour,
                                              HorizontalAlignment.Right, "You"));
            ScrollToBottom();
        }

        private void AddBotBubble(string text, string? hexColour = null)
        {
            var fg = hexColour != null
                ? (SolidColorBrush)new BrushConverter().ConvertFrom(hexColour)!
                : BotTextColour;
            ChatPanel.Children.Add(MakeBubble(text, BotBubbleBg, fg,
                                              HorizontalAlignment.Left, "Cyrex"));
            ScrollToBottom();
        }

        private static Border MakeBubble(string text, SolidColorBrush bg, SolidColorBrush fg,
                                         HorizontalAlignment align, string sender)
        {
            var stack = new StackPanel();

            stack.Children.Add(new TextBlock
            {
                Text       = sender,
                Foreground = TimeColour,
                FontSize   = 10,
                FontWeight = FontWeights.Bold,
                Margin     = new Thickness(0, 0, 0, 2)
            });

            stack.Children.Add(new TextBlock
            {
                Text        = text,
                Foreground  = fg,
                FontSize    = 13,
                TextWrapping= TextWrapping.Wrap,
                LineHeight  = 20
            });

            stack.Children.Add(new TextBlock
            {
                Text              = DateTime.Now.ToString("HH:mm"),
                Foreground        = TimeColour,
                FontSize          = 9,
                Margin            = new Thickness(0, 3, 0, 0),
                HorizontalAlignment = align == HorizontalAlignment.Right
                                      ? HorizontalAlignment.Right
                                      : HorizontalAlignment.Left
            });

            return new Border
            {
                Background         = bg,
                CornerRadius       = new CornerRadius(10),
                Padding            = new Thickness(12, 8, 12, 8),
                Margin             = new Thickness(
                                         align == HorizontalAlignment.Right ? 80 : 4, 4,
                                         align == HorizontalAlignment.Right ? 4  : 80, 4),
                HorizontalAlignment= align,
                MaxWidth           = 540,
                Child              = stack
            };
        }

        private Border AddTypingIndicator()
        {
            var b = new Border
            {
                Background         = BotBubbleBg,
                CornerRadius       = new CornerRadius(10),
                Padding            = new Thickness(12, 6, 12, 6),
                Margin             = new Thickness(4, 4, 80, 4),
                HorizontalAlignment= HorizontalAlignment.Left,
                Child              = new TextBlock
                {
                    Text       = "● ● ●",
                    Foreground = TimeColour,
                    FontSize   = 12
                }
            };
            ChatPanel.Children.Add(b);
            ScrollToBottom();
            return b;
        }

        private void ScrollToBottom() =>
            Dispatcher.InvokeAsync(() => ChatScroll.ScrollToBottom(),
                System.Windows.Threading.DispatcherPriority.Background);

        private void SetStatus(string msg) => TxtStatus.Text = msg;

        private void UpdateSentimentLabel(SentimentResult s)
        {
            (string label, string hex) = s switch
            {
                SentimentResult.Positive   => ("Mood: Positive 😊",   "#00C864"),
                SentimentResult.Worried    => ("Mood: Worried 😟",    "#FFB800"),
                SentimentResult.Frustrated => ("Mood: Frustrated 😤", "#FF5050"),
                SentimentResult.Curious    => ("Mood: Curious 🤔",    "#00D4FF"),
                _                          => ("Mood: Neutral 😐",    "#607B8B")
            };
            TxtSentiment.Text       = label;
            TxtSentiment.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(hex)!;
        }

        private void SpeakAsync(string text)
        {
            if (!_voiceEnabled) return;
            Task.Run(() =>
            {
                try { HelperSpeech.SpeakText(text); }
                catch { /* voice is optional */ }
            });
        }
    }
}

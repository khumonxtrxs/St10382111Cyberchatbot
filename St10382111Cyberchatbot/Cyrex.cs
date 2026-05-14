using St10382111Cyberchatbot.Utilities;
using St10382111Cyberchatbot.Models;
using System;
using System.Collections.Generic;

namespace St10382111Cyberchatbot
{
    public static class Cyrex
    {
        // Random for picking varied responses; _lastTopic Tracks the last topic for follow-up flow; Using Dictionary for memory to store name and favourite topic
        private static readonly Random _rng = new();
        private static string _lastTopic = string.Empty;
        private static readonly Dictionary<string, string> _memory =
            new(StringComparer.OrdinalIgnoreCase);


        
        //  KEYWORD & RESPONSES
   
        private static readonly Dictionary<string, List<string>> _keywordResponses =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new List<string>
            {
                "Use strong, unique passwords for every account — at least 12 characters with letters, numbers and symbols.",
                "Never reuse the same password across sites. A password manager like Bitwarden makes this easy.",
                "Avoid obvious passwords like birthdays or 'password123'. Hackers use dictionary attacks to crack these.",
                "Enable multi-factor authentication alongside a strong password for an extra layer of protection."
            },
                ["phishing"] = new List<string>
            {
                "Never click links from unknown emails. Scammers disguise themselves as trusted organisations.",
                "Check the sender's actual email address — phishing emails use slight misspellings of real domains.",
                "If an email creates urgency ('Your account will close!'), treat it as suspicious.",
                "When in doubt, navigate to the website directly instead of clicking the link in the email."
            },
                ["scam"] = new List<string>
            {
                "Common scams include fake tech support, lottery wins and romance scams. If it seems too good to be true, it is.",
                "Never send money or gift cards to someone you haven't met in person — a major scam red flag.",
                "Scammers often impersonate banks or government agencies. Hang up and call the official number to verify.",
                "Report scams to the South African Police Service (SAPS) or the Banking Association of SA."
            },
                ["privacy"] = new List<string>
            {
                "Review your app permissions regularly — many apps request more data than they actually need.",
                "Use a VPN on public Wi-Fi to encrypt your traffic and protect your personal information.",
                "Check your social media privacy settings and limit who can see your posts and personal details.",
                "Read privacy policies before signing up for services, especially regarding third-party data sharing."
            },
                ["malware"] = new List<string>
            {
                "Keep your operating system and apps updated — most malware exploits vulnerabilities that patches fix.",
                "Install reputable antivirus software and run regular scans to catch threats early.",
                "Never download software from untrusted sources. Stick to official websites or verified app stores.",
                "Ransomware encrypts your files for payment. Regular offline backups are your best defence."
            },
                ["safe browsing"] = new List<string>
            {
                "Make use of HTTPS, enable two-factor authentication and avoid suspicious links.",
                "Always look for 'https://' and the padlock icon before entering personal information on a website.",
                "Use a browser extension like uBlock Origin to block malicious ads and tracking scripts.",
                "Clear your browser cookies and cache periodically to reduce your tracking footprint."
            },
                ["vpn"] = new List<string>
            {
                "A VPN encrypts all traffic between your device and the internet — essential on public Wi-Fi.",
                "Choose a reputable no-logs VPN provider. Free VPNs often sell your data.",
                "A VPN hides your IP address, making it harder for websites and advertisers to track you."
            },
                ["two factor"] = new List<string>
            {
                "Two-factor authentication adds a one-time code on top of your password — enable it everywhere.",
                "Use an authenticator app like Google Authenticator instead of SMS codes for stronger 2FA.",
                "Even if a hacker steals your password, 2FA stops them from accessing your account."
            }
            };

       
        public static void Start(string name)
        {
            while (true)
            {
                Console.Write("\nYou: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.ToLower() == "exit") break;

                string response = GetResponse(input);
                Console.WriteLine($"Cyrex: {response}");
                HelperSpeech.SpeakText(response);
            }
        }

      
        //  GetResponse expanded 
        
        public static string GetResponse(string input)
        {
            string clean = input?.ToLower().Trim() ?? string.Empty;

           
            SentimentResult sentiment = SentimentDetector.Detect(clean);
            string prefix = BuildSentimentPrefix(sentiment);

            if (IsFollowUp(clean))
                return HandleFollowUp();

            if (clean.Contains("my name is") || clean.StartsWith("i am ") || clean.StartsWith("i'm "))
                return StoreAndGreetName(clean);

            if (clean.Contains("i'm interested in") || clean.Contains("interested in") || clean.Contains("i like "))
                return StoreInterest(clean);

            if (clean.Contains("what's my name") || clean.Contains("my name"))
                return RecallName();

            if (clean.Contains("what do i like") || clean.Contains("my interest") || clean.Contains("my favourite"))
                return RecallInterest();


            if (clean.Contains("how are you")) return prefix + "I'm just a code, haha but I am functioning well, thank you.";
            if (clean.Contains("purpose")) return prefix + "My purpose is to answer your cybersecurity questions.";
            if (clean.Contains("what can i ask")) return "You can ask me about passwords, phishing, scams, privacy, malware, safe browsing, VPNs, and two-factor authentication.";
            if (clean.Contains("hello") || clean.Contains("hi"))
                return prefix + $"Hello{GetNameTag()}! Ask me anything about cybersecurity.";


            // Keyword recognition with random responses 
            foreach (var kv in _keywordResponses)
            {
                if (clean.Contains(kv.Key))
                {
                    _lastTopic = kv.Key;
                    string response = PickRandom(kv.Value);
                    return prefix + Personalise(response);
                }
            }

            // Default &  error handling 
            _lastTopic = string.Empty;
            return PickRandom(new List<string>
            {
                "I'm not sure about that. Can you rephrase?",
                "I didn't quite catch that. Try asking about passwords, phishing, scams or privacy.",
                "That's outside what I know. Try a cybersecurity topic like malware or safe browsing.",
                "I'm not sure I understand. Can you try rephrasing?"
            });
        }

      
        //  SENTIMENT PREFIX  
        
        private static string BuildSentimentPrefix(SentimentResult s)
        {
            string name = GetNameTag();
            return s switch
            {
                SentimentResult.Worried =>
                    $"It's completely understandable to feel that way{name}. You're taking the right step by learning about it.\n\n",
                SentimentResult.Frustrated =>
                    $"I hear you{name} — cybersecurity can feel overwhelming. Let me make this as clear as possible:\n\n",
                SentimentResult.Positive =>
                    $"Glad to hear it{name}! 😊 Here's what you need to know:\n\n",
                SentimentResult.Curious =>
                    $"Great question{name}! Here's what you should know:\n\n",
                _ => string.Empty
            };
        }

        
        //  CONVERSATION FLOW
      
        private static bool IsFollowUp(string clean) =>
            clean.Contains("tell me more") || clean.Contains("explain more") ||
            clean.Contains("give me another") || clean.Contains("more info") ||
            clean.Contains("another tip") || clean.Contains("more details") ||
            clean.Contains("continue") || clean.Contains("go on") ||
            clean.Contains("say more");

        private static string HandleFollowUp()
        {
            if (string.IsNullOrEmpty(_lastTopic))
                return "What topic would you like to continue? Try passwords, phishing, scams, privacy, malware, safe browsing or VPNs.";

            if (_keywordResponses.TryGetValue(_lastTopic, out var list))
                return $"Here's another tip on {_lastTopic}:\n\n{Personalise(PickRandom(list))}";

            return "Could you let me know which topic you'd like to explore further?";
        }

       
        //  MEMORY
     
        private static string StoreAndGreetName(string clean)
        {
            string[] patterns = { "my name is ", "i am ", "i'm " };
            foreach (var p in patterns)
            {
                int idx = clean.IndexOf(p, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    string extracted = clean[(idx + p.Length)..].Trim().Split(' ')[0];
                    if (!string.IsNullOrEmpty(extracted))
                    {
                        extracted = char.ToUpper(extracted[0]) + extracted[1..];
                        _memory["name"] = extracted;
                        Identity.SetName(extracted);
                        return $"Nice to meet you, {extracted}! I'll remember that. Feel free to ask me anything about cybersecurity.";
                    }
                }
            }
            return "I'd love to know your name! You can say 'My name is [your name]'.";
        }

        private static string StoreInterest(string clean)
        {
            string[] patterns = { "i'm interested in ", "interested in ", "i like " };
            foreach (var p in patterns)
            {
                int idx = clean.IndexOf(p, StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    string topic = clean[(idx + p.Length)..].Trim();
                    if (!string.IsNullOrEmpty(topic))
                    {
                        _memory["interest"] = topic;
                        _lastTopic = topic;
                        return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.\n\nSay 'tell me more' to keep going on this topic.";
                    }
                }
            }
            return "What topic interests you? (e.g. 'I'm interested in privacy')";
        }

        private static string RecallName()
        {
            if (_memory.TryGetValue("name", out string? name))
                return $"Your name is {name}! I remember everything you share with me. 😊";
            return "I don't know your name yet. You can tell me by saying 'My name is [your name]'.";
        }

        private static string RecallInterest()
        {
            if (_memory.TryGetValue("interest", out string? interest))
                return $"You told me you're interested in {interest}. You might want to explore the latest tips on {interest} security!";
            return "I don't have a recorded interest for you yet. Tell me what cybersecurity topic you care about!";
        }

        
        //  HELPERS
       
        private static string GetNameTag()
        {
            string name = _memory.TryGetValue("name", out var n) ? n : Identity.UserName;
            return name != "Guest" && !string.IsNullOrEmpty(name) ? $", {name}" : string.Empty;
        }

        private static string Personalise(string response)
        {
            if (_memory.TryGetValue("interest", out string? interest) && _rng.Next(3) == 0)
                response += $"\n\n💡 As someone interested in {interest}, this is especially relevant to you.";
            return response;
        }

        private static T PickRandom<T>(List<T> list) => list[_rng.Next(list.Count)];
    }
}
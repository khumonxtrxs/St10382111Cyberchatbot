using System;

namespace St10382111Cyberchatbot.Models
{
    /// <summary>Possible emotional tones detected in the user's message.</summary>
    public enum SentimentResult
    {
        Neutral,
        Positive,
        Worried,
        Frustrated,
        Curious
    }

    /// Detects sentiment from user input by checking for keyword signals.
   
    public static class SentimentDetector
    {
        public static SentimentResult Detect(string input)
        {
            string clean = input?.ToLower() ?? string.Empty;

            if (ContainsAny(clean, "worried", "scared", "afraid", "fear", "anxious", "nervous", "concerned", "panic"))
                return SentimentResult.Worried;

            if (ContainsAny(clean, "frustrated", "annoyed", "angry", "upset", "terrible", "hate", "useless"))
                return SentimentResult.Frustrated;

            if (ContainsAny(clean, "curious", "wondering", "how does", "what is", "explain", "tell me", "how do"))
                return SentimentResult.Curious;

            if (ContainsAny(clean, "great", "awesome", "thanks", "thank you", "love", "good", "amazing", "helpful"))
                return SentimentResult.Positive;

            return SentimentResult.Neutral;
        }

        private static bool ContainsAny(string text, params string[] terms)
        {
            foreach (var t in terms)
                if (text.Contains(t, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}

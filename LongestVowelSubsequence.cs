namespace pusula_may;

public class LongestVowelSubsequence
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        List<string> longestSubsequence = new List<string>();
        List<string> currentSubsequence = new List<string>();

        foreach (var word in words)
        {
            if (word.Length > 0 && vowels.Contains(word[0]))
            {
                currentSubsequence.Add(word);
            }
            else
            {
                if (currentSubsequence.Count > longestSubsequence.Count)
                {
                    longestSubsequence = new List<string>(currentSubsequence);
                }
                currentSubsequence.Clear();
            }
        }

        if (currentSubsequence.Count > longestSubsequence.Count)
        {
            longestSubsequence = currentSubsequence;
        }

        return System.Text.Json.JsonSerializer.Serialize(longestSubsequence);
    }
}
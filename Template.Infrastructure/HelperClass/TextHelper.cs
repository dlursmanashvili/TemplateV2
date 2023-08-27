using System.Text.RegularExpressions;

namespace Template.Infrastructure.HelperClass;

public static class TextHelper
{
    public static Dictionary<string, string>? GetKeysFromText(string? text)
    {
        if (text == null)
        {
            return null;
        }
        var dictionary = new Dictionary<string, string>();
        string pattern = @"<[^>]+>";
        var matches = Regex.Matches(text, pattern);
        foreach (Match match in matches)
        {
            if (!dictionary.ContainsKey(match.Value.Substring(1, match.Value.Length - 2)))
            {
                dictionary.Add(match.Value.ToString().Substring(1, match.Value.Length - 2), "");
            }
        }
        return dictionary;
    }

    public static string GetGeneratedAndCangedText(string? text, Dictionary<string, string>? listofCahngedWord)
    {
        if (text == null)
        {
            return null;
        }
        string pattern = @"<[^>]+>";
        var keys = GetKeysFromText(text);

        if (listofCahngedWord != null && listofCahngedWord.Count != 0 && keys != null && keys.Any())
        {
            foreach (var item in listofCahngedWord)
            {
                if (keys.Any(x => x.Key == item.Key))
                {
                    text = text.Replace($"<{item.Key}>", item.Value.ToString());
                }
            }
        }
        return text;
    }
}

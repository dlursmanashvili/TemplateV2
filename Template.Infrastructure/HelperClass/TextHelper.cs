using Microsoft.IdentityModel.Tokens;
using Template.Model.Exceptions;

namespace Template.Infrastructure.HelperClass;

public static class TextHelper
{
    public static List<string> GetChangedlist(List<string> CutTextList, Dictionary<string, string> listofCahngedWord)
    {
        var count = 0;
        for (int i = 0; i < CutTextList.Count; i++)
        {
            if (CutTextList[i].ToCharArray().Length > 2 && CutTextList[i].ToCharArray()[0] == '<' && CutTextList[i].ToCharArray().Last() == '>')
            {
                count++;
                string modifiedItem = CutTextList[i].Substring(1, CutTextList[i].Length - 2);
                Console.WriteLine(CutTextList[i] + $" {modifiedItem}");

                var result = listofCahngedWord.FirstOrDefault(x => x.Key == modifiedItem);
                if (result.Value != null)
                {
                    CutTextList[i] = result.Value;
                }
            }
        }
        Console.WriteLine(count);
        return CutTextList;
    }

    public static List<string> CutText(string text)
    {
        if (text.IsNullOrEmpty())
        {
            throw new BadRequestException();
        }

        char[] punctuationMarks = { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '=', '?', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };

        var listOfWord = new List<string>();
        string word = "";
        var count = 0;
        for (int i = 0; i < text.Length; i++)
        {
            string? filtredText = null;

            if (i == text.Length - 1)
            {
                word += text[i];
                if (word != null && punctuationMarks.Contains(word.First()))
                {
                    listOfWord.Add(word.First().ToString());
                    word = word.Substring(1);
                }
                if (word != null && punctuationMarks.Contains(word.Last()))
                {
                    filtredText = word.Last().ToString();
                    word = word.Substring(0, word.Length - 1);
                }
                if (word != null && word.Length != 0)
                {
                    listOfWord.Add(word);
                }
                if (filtredText != null)
                {
                    listOfWord.Add(filtredText);
                }
                break;
            }
            else if (text[i].ToString() != " ")
            {
                word += text[i];
            }
            else if (text[i].ToString() == " ")
            {
                if (word != null && punctuationMarks.Contains(word.First()))
                {
                    listOfWord.Add(word.First().ToString());
                    word = word.Substring(1);
                }
                if (word != null && punctuationMarks.Contains(word.Last()))
                {
                    filtredText = word.Last().ToString();
                    word = word.Substring(0, word.Length - 1);
                }
                if (word != null && word.Length != 0)
                {
                    listOfWord.Add(word);
                }
                if (filtredText != null)
                {
                    listOfWord.Add(filtredText);
                }
                listOfWord.Add(" ");
                word = null;
            }
        }
        return listOfWord;
    }

    public static IEnumerable<string> ValidateText(List<string> CutTextList)
    {
        for (int i = 0; i < CutTextList.Count; i++)
        {
            if (CheckTtext(CutTextList[i]).Any(x => x == '<' || x == '>'))
            {
                throw new Exception($"            '{CheckTtext(CutTextList[i].ToString())}'           text does not meet the standard. Please edit the text\n\n\n\n");
            }
            else
            {
                yield return CutTextList[i];
            }
        }
    }

    public static string CheckTtext(string CutTextItem)
    {
        if (CutTextItem.ToCharArray().Length > 2 && CutTextItem.ToCharArray()[0] == '<' && CutTextItem.ToCharArray().Last() == '>')
        {
            string modifiedItem = CutTextItem.Substring(1, CutTextItem.Length - 2);
            return modifiedItem;
        }
        return CutTextItem;
    }

    public static string GetCutTextSum(IEnumerable<string> strings)
    {
        var text = "";
        foreach (string s in strings)
        {
            text += s;
        }
        return text;
    }


    public static Dictionary<string, string>? ReturnDictionaryKeysFromText(string text)
    {
        var textlist = CutText(text);
        ValidateText(textlist);
        var dictionary = new Dictionary<string, string>();
        for (int i = 0; i < textlist.Count; i++)
        {
            if (textlist[i].ToCharArray().Length > 2 && textlist[i].ToCharArray()[0] == '<' && textlist[i].ToCharArray().Last() == '>')
            {
                string modifiedItem = textlist[i].Substring(1, textlist[i].Length - 2);
                if (!dictionary.ContainsKey(modifiedItem))
                    dictionary.Add(modifiedItem, "");
            }
        }
        return dictionary;
    }

    public static string GetGeneratedAndCangedText(string text, Dictionary<string, string> listofCahngedWord)
    {
        var Cuttext = CutText(text);
        ValidateText(Cuttext);
        return GetCutTextSum(GetChangedlist(Cuttext, listofCahngedWord));
    }

    /// <summary>
    /// ფუნქცია დაჭრის, შეამოწმებს ტექსტ თუ ტექსტი ვალიდურია მაშინ დააბრუნებს გადაცემულ ტექსტს
    /// </summary>
    public static string CheckNewText(string text)
    {
        return GetCutTextSum(ValidateText(CutText(text)));
    }
}

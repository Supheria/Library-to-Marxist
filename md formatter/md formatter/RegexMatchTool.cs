using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace md_formatter;

public static class RegexMatchTool
{
    public static bool GetMatch(this string input, string pattern, [NotNullWhen(true)] out Match? match)
    {
        match = null;
        try
        {
            match = Regex.Match(input, pattern);
            return match.Success;
        }
        catch
        {
            return false;
        }
    }

    public static bool GetMatchIgnoreAllBlanks(this string input, string pattern, [NotNullWhen(true)] out Match? match) =>
        GetMatch(Regex.Replace(input, @"\s", ""), pattern, out match);
}
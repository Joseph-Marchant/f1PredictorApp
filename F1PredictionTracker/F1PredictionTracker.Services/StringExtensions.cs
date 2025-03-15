namespace F1PredictionTracker.Services;

public static class StringExtensions
{
    public static string ToPascalCase(this string s)
    {
        return char.ToUpperInvariant(s[0]) + s.Substring(1).ToLowerInvariant();
    }
}

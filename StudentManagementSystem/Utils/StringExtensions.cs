namespace StudentManagementSystem.Utils;

public static class StringExtensions
{
    public static string Capitalize(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        char first = value[0];
        string rest = value[1..];

        return first.ToString().ToUpper() + rest.ToLower();
    }

    public static string RemoveDoubleSpaces(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        while (value.Contains("  "))
        {
            value = value.Replace("  ", " ");
        }

        return value.Trim();
    }
}
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
}
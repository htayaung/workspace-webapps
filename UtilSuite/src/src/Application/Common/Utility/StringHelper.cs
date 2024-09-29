namespace Application.Common.Utility;

public static class StringHelper
{
    public static bool IsEqual(string valueA, string valueB)
    {
        return string.Equals(valueA, valueB, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool IsNull(this string value)
    {
        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}

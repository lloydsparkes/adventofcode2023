namespace AdventOfCode2023;

public static class Utils
{
    public static int[] GetIntArray(this string raw, char seperator = ' ')
    {
        return raw.Split(seperator)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => int.Parse(s.Trim()))
            .ToArray();
    }
    
    public static long[] GetLongArray(this string raw, char seperator = ' ')
    {
        return raw.Split(seperator)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => long.Parse(s.Trim()))
            .ToArray();
    }
}
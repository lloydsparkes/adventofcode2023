namespace AdventOfCode2023;

public class Day4 : IDay
{
    public string RunPartOne(string[] inputLines)
    {
        return inputLines.Select(Parse).Select(c => 2 ^ (c.MatchCount())).Sum().ToString();
    }

    public string RunPartTwo(string[] inputLines)
    {
        return "";
    }

    public Card Parse(string line)
    {
        var bits = line.Split(':')[1].Split('|');
        var winning = bits[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => int.Parse(s.Trim())).ToArray();
        var numbers = bits[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => int.Parse(s.Trim())).ToArray();

        return new Card(winning, numbers);
    }

    public record Card(int[] WinningNumbers, int[] CardNumbers)
    {
        public int MatchCount() => CardNumbers.Intersect(WinningNumbers).Count();
    }
}
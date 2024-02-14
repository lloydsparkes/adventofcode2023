using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day1 : IDay
{
    public string RunPartOne(string[] inputLines)
    {
        return Solve(inputLines, @"\d");
    }

    public string RunPartTwo(string[] inputLines)
    {
        return Solve(inputLines, @"\d|one|two|three|four|five|six|seven|eight|nine");
    }

    public string Solve(string[] inputLines, string regex)
    {
        var numbers = new List<int>();
        
        foreach (var line in inputLines)
        {
            var first = Regex.Match(line, regex).Value;
            var last = Regex.Match(line, regex, RegexOptions.RightToLeft).Value;
            
            numbers.Add((ParseValue(first) * 10) + ParseValue(last));
        }

        return numbers.Sum().ToString();

        int ParseValue(string v) => v switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            var d => int.Parse(d)
        };
    }
}
namespace AdventOfCode2023;

public class Day4 : IDay
{
    public string RunPartOne(string[] inputLines)
    {
        var cards = inputLines.Select(Parse).ToList();
        
        //cards.ForEach(c => Console.WriteLine(
        //    $"{c.MatchCount()} = W: {string.Join(',', c.WinningNumbers)} - C: {string.Join(',', c.CardNumbers)}"));
        
        return cards.Where(c => c.MatchCount() > 0).Select(c => Math.Pow(2, c.MatchCount()-1)).Sum().ToString();
    }

    public string RunPartTwo(string[] inputLines)
    {
        var cards = inputLines.Select(Parse).ToArray();
        var counts = cards.Select(c => 1).ToArray();

        for (var i = 0; i < cards.Length; i++)
        {
            for (int j = 0; j < cards[i].MatchCount(); j++)
            {
                counts[i + j + 1] += counts[i];
            }
        }

        return counts.Sum().ToString();
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
        public int MatchCount() => WinningNumbers.Intersect(CardNumbers).Count();
    }
}
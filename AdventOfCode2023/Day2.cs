using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day2 : IDay
{
    public string RunPartOne(string[] inputLines)
    {
        var games = inputLines.Select(Parse).ToList();

        int maxRed = 12, maxBlue = 14, maxGreen = 13;

        var validGames = games
            .Where(g => g.Rounds.All(r => r.Blue <= maxBlue && r.Red <= maxRed && r.Green <= maxGreen))
            .Select(g => g.Id).Sum();

        return validGames.ToString();
    }

    public string RunPartTwo(string[] inputLines)
    {
        var games = inputLines.Select(Parse).ToList();
        
        var minimumCubes = games.Select(g =>
            new Round(g.Rounds.Max(r => r.Red), 
                g.Rounds.Max(r => r.Green), 
                g.Rounds.Max(r => r.Blue)))
            .Select(r => r.Blue * r.Green * r.Red)
            .Sum();
        
        return minimumCubes.ToString();
    }

    public Game Parse(string lines)
    {
        var gameId = int.Parse(Regex.Match(lines, "[0-9]+").Value);
        var rounds = lines.Split(':')[1].Trim().Split(';');

        var parsedRounds = new List<Round>();
        
        foreach (var round in rounds)
        {
            int blue = 0, red = 0, green = 0;
            var bits = round.Split(',');

            foreach (var bit in bits)
            {
                var color = Regex.Match(bit, "blue|green|red").Value;
                var number = int.Parse(Regex.Match(bit, "[0-9]+").Value);

                (blue, red, green) = color switch
                {
                    "blue" => (number, red, green),
                    "red" => (blue, number, green),
                    "green" => (blue, red, number)
                };
            }
            parsedRounds.Add(new Round(red, green, blue));
        }

        return new Game(gameId, parsedRounds.ToArray());
    }

    public record Game(int Id, Round[] Rounds)
    {
        public override string ToString()
        {
            return $"Game: {Id} - {string.Join<Round>(';', Rounds)}";
        }
    }

    public record Round(int Red, int Green, int Blue)
    {
        public override string ToString()
        {
            return $"Blue={Blue},Red={Red},Green={Green}";
        }
    }
}
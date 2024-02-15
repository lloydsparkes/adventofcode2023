namespace AdventOfCode2023;

public class Day3 : IDay
{
    public string RunPartOne(string[] inputLines)
    {
        var map = inputLines.Select(l => l.ToArray()).ToArray();
        var validNumbers = new List<int>();

        for (int x = 0; x < map.Length; x++)
        {
            var currentNumber = string.Empty;
            var currentNumberIsTouched = false;
            
            for (int y = 0; y < map[x].Length; y++)
            {
                if (Char.IsDigit(map[x][y]))
                {
                    currentNumber += map[x][y];
                    if (!currentNumberIsTouched)
                    {
                        currentNumberIsTouched = CheckSurroundingForSymbols(map, x, y, c => !Char.IsDigit(c)) != null;
                    }
                }
                else
                {
                    if (currentNumberIsTouched)
                    {
                        validNumbers.Add(int.Parse(currentNumber));
                    }

                    currentNumber = string.Empty;
                    currentNumberIsTouched = false;
                }
            }

            if (currentNumber != string.Empty)
            {
                if (currentNumberIsTouched)
                {
                    validNumbers.Add(int.Parse(currentNumber));
                }
            }
        }
        
        //validNumbers.ForEach(Console.WriteLine);
        return validNumbers.Sum().ToString();
    }

    public string RunPartTwo(string[] inputLines)
    {
        var map = inputLines.Select(l => l.ToArray()).ToArray();
        var validNumbers = new List<(int number, Point gearLocation)>();

        for (int x = 0; x < map.Length; x++)
        {
            var currentNumber = string.Empty;
            Point? gearLocation = null;
            
            for (int y = 0; y < map[x].Length; y++)
            {
                if (Char.IsDigit(map[x][y]))
                {
                    currentNumber += map[x][y];
                    if (gearLocation == null)
                    {
                        gearLocation = CheckSurroundingForSymbols(map, x, y, c => c == '*');
                    }
                }
                else
                {
                    if (gearLocation != null)
                    {
                        validNumbers.Add((int.Parse(currentNumber), gearLocation));
                    }

                    currentNumber = string.Empty;
                    gearLocation = null;
                }
            }

            if (currentNumber != string.Empty)
            {
                if (gearLocation != null)
                {
                    validNumbers.Add((int.Parse(currentNumber), gearLocation));
                }
            }
        }

        var gears = validNumbers.GroupBy(x => x.gearLocation)
            .Where(gs => gs.Count() == 2)
            .Sum(gs => 
                gs.Select(g => g.number).Aggregate((a, b) => a * b));

        return gears.ToString();
    }

    public record Point(int x, int y);

    public Point? CheckSurroundingForSymbols(Char[][] map, int x, int y, Func<Char, bool> check)
    {
        var checks = new[]
        {
            (1, 0),
            (1, -1),
            (0, -1),
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, 1),
            (1, 1)
        };
        
        foreach(var toCheck in checks)
        {
            var testX = x + toCheck.Item1;
            var testY = y + toCheck.Item2;
            if (testX >= 0 && testX < map.Length && testY >= 0 && testY < map[testX].Length)
            {
                if (check(map[testX][testY]) && map[testX][testY] != '.')
                {
                    return new Point(testX, testY);
                }
            }
        }

        return null;
    }
}
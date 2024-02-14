using AdventOfCode2023;

class Program
{
    static void Main(string[] args)
    {
        var (dayToRun, day) = FindDay();
        var (testLines1, testLines2, actualLines) = LoadInputs(day);
        
        Console.WriteLine($"Loaded and Running for Day {day}");

        if (testLines1 == null)
        {
            Console.WriteLine($"Could not load Test inputs for Day {day} Part 1");
        }
        else
        {
            Console.WriteLine($"Day {day} - Test Part 1 Result: {dayToRun.RunPartOne(testLines1)}");
        }
        
        if (testLines2 == null)
        {
            Console.WriteLine($"Could not load Test inputs for Day {day} Part 2");
        }
        else
        {
            Console.WriteLine($"Day {day} - Test Part 2 Result: {dayToRun.RunPartTwo(testLines2)}");
        }
        
        if (actualLines == null)
        {
            Console.WriteLine("Could not load actual inputs for Day 1");
        }
        else
        {
            Console.WriteLine($"Day {day} - Part 1 Result: {dayToRun.RunPartOne(actualLines)}");
            Console.WriteLine($"Day {day} - Part 2 Result: {dayToRun.RunPartTwo(actualLines)}");
        }
    }

    public static (string[] testLines1, string[] testLines2, string[] actualLines) LoadInputs(int day)
    {
        // Assume files are in our bin/Inputs directory
        string[] testInput1 = null, testInput2 = null, actualInput = null;
        if (File.Exists($"Inputs/Day{day}TestPart1.txt"))
        {
            testInput1 = File.ReadAllLines($"Inputs/Day{day}TestPart1.txt");
        }
        
        if (File.Exists($"Inputs/Day{day}TestPart2.txt"))
        {
            testInput2 = File.ReadAllLines($"Inputs/Day{day}TestPart2.txt");
        }
        
        if (File.Exists($"Inputs/Day{day}.txt"))
        {
            actualInput = File.ReadAllLines($"Inputs/Day{day}.txt");
        }

        return (testInput1, testInput2, actualInput);
    }
    
    public static (IDay dayToRun, int day) FindDay(int? day = null)
    {
        var dayDict = typeof(IDay).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDay)) && t != typeof(IDay))
            .ToDictionary(ExtractDay, t => t);

        if (day.HasValue)
        {
            return ((IDay)Activator.CreateInstance(dayDict[day.Value]), day.Value);
        }

        return ((IDay)Activator.CreateInstance(dayDict[dayDict.Keys.Max()]), dayDict.Keys.Max());
    }

    public static int ExtractDay(Type t)
    {
        return int.Parse(t.Name.Replace("Day", ""));
    }
}
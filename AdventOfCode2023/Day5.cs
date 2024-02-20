namespace AdventOfCode2023;

public class Day5 : IDay
{
    public string RunPartOne(string[] inputLines)
    {
        var alamac = Parse(inputLines);

        return alamac.GetSeedToLocationMap().Values.Min().ToString();
    }

    public string RunPartTwo(string[] inputLines)
    {
        var alamac = Parse(inputLines);

        var seedBuckets = alamac.Seeds.Chunk(2)
            .AsParallel()
            .Select(b => GetMapPart(b[0], b[1]))
            .Min();

        return seedBuckets.ToString();

        long GetMapPart(long seedStart, long range)
        {
            long minResult = long.MaxValue;

            for (var seed = seedStart; seed <= (seedStart + range); seed++)
            {
                long source = seed;
                foreach (var map in alamac.Maps)
                {
                    source = map.LookupDestination(source);
                }

                if (minResult > source)
                {
                    minResult = source;
                }
            }

            return minResult;
        }
    }

    public Alamac Parse(string[] inputLines)
    {
        long[] seeds = Array.Empty<long>();
        List<AlamacMap> maps = new();
        List<AlamacRow> rows = new();
        
        foreach (var line in inputLines)
        {
            if (line.StartsWith("seeds:"))
            {
                seeds = line.Split(':')[1].GetLongArray();
                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                if (rows.Any())
                {
                    maps.Add(new AlamacMap(rows.ToArray()));
                    rows = new();
                }

                continue;
            }
            
            if(line.Contains("map:")) continue;

            var bits = line.GetLongArray();
            rows.Add(new AlamacRow(bits[0], bits[1], bits[2]));
        }
        
        if (rows.Any())
        {
            maps.Add(new AlamacMap(rows.ToArray()));
            rows = new();
        }

        return new Alamac(seeds, maps.ToArray());
    }

    public record Alamac(long[] Seeds, AlamacMap[] Maps)
    {
        public Dictionary<long, long> GetSeedToLocationMap()
        {
            var results = new Dictionary<long, long>();
            
            foreach (var seed in Seeds)
            {
                long source = seed;
                foreach (var map in Maps)
                {
                    source = map.LookupDestination(source);
                }
                results.Add(seed, source);
            }

            return results;
        }
    }
    
    public record AlamacRow(long DestinationStart, long SourceStart, long Length);

    public record AlamacMap(AlamacRow[] Rows)
    {
        public long LookupDestination(long source)
        {
            foreach (var row in Rows)
            {
                if (row.SourceStart <= source && source <= (row.SourceStart + row.Length))
                {
                    return row.DestinationStart + (source - row.SourceStart);
                }
            }
            
            return source;
        }
    }
}
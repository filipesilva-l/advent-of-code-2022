var lines = File.ReadAllLines("input");

List<long> elves = new(lines.Count());

long currentTotal = 0;
foreach (var line in lines)
{
	if (string.IsNullOrEmpty(line))
	{
		elves.Add(currentTotal);
		currentTotal = 0;
		continue;
	}

	Console.WriteLine("Input: {0}", line);

	var totalInLine = long.Parse(line);
	
	currentTotal += totalInLine;
}

Console.WriteLine("Total elfs: {0}", elves.Count());

var sorted = elves.OrderByDescending(t => t);

var totalMostCalories = sorted.Take(3).Sum();

Console.WriteLine("Total most calories: {0}", totalMostCalories);


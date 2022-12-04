var rucksacks = File.ReadAllLines("input");

var totalPriority = 0;
foreach (var group in rucksacks.Chunk(3))
{
	string accm = "";
	foreach (var rucksack in group) {
		if (string.IsNullOrEmpty(accm)) {
			accm = rucksack;
			continue;
		}

		accm = string.Join("", accm.Intersect(rucksack));
	}

	totalPriority += accm.Select(GetPriority).Sum();
}

Console.WriteLine("total: {0}", totalPriority);


int GetPriority(char letter)
{
	var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	return alphabet.IndexOf(letter) + 1;
}

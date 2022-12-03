var rucksacks = File.ReadAllLines("input");

var totalPriority = 0;
foreach (var rucksack in rucksacks)
{
	var firstCompartment = rucksack.Substring(0, rucksack.Length / 2);
	var lastCompartment = rucksack.Substring(rucksack.Length / 2);

	var letters = firstCompartment.Intersect(lastCompartment);

	totalPriority += letters.Select(l => GetPriority(l.ToString())).Sum();
}

Console.WriteLine("total: {0}", totalPriority);


int GetPriority(string letter)
{
	var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	return alphabet.IndexOf(letter) + 1;
}

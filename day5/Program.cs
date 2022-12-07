var lines = File.ReadAllLines("input").ToList();
var cratesString = new List<string>();

foreach (var line in lines) {
	if (string.IsNullOrEmpty(line))
		break;

	cratesString.Add(line);
}

cratesString.Reverse();

var width = cratesString.First().Length;
var height = cratesString.Count;

lines.RemoveRange(0, height + 1);

List<List<char>> crates = new();
for (var col = 0; col < width; col++) {
	List<char> elements = new();

	for (var row = 0; row < height; row++) {
		var element = cratesString[row][col]; 

		if (char.IsWhiteSpace(element))
			continue;

		elements.Add(element);
	}

	crates.Add(elements);
}

foreach (var line in lines) {
	var splitted = line.Split(',').Select(int.Parse).ToArray();

	var quantity = splitted[0];
	var from = splitted[1];
	var to = splitted[2];

	var crateFrom = crates[from - 1];
	var crateTo = crates[to - 1];

	var crateFromCount = crateFrom.Count;

	var fixedIndex = crateFromCount - quantity;

	while (quantity > 0) {
		var element = crateFrom[fixedIndex];

		crateTo.Add(element);
		crateFrom.RemoveAt(fixedIndex);

		quantity--;
	}
}

var word = new String(crates.Select(c => c.Last()).ToArray());

Console.WriteLine("word: {0}", word);


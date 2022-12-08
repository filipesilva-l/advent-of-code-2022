var lines = System.IO.File.ReadAllLines("input");

var rootDir = new Directory("/", new Dictionary<string, IObject>(), null);

Directory dir = rootDir;
foreach (var line in lines)
{
    var operation = GetOperation(line);

    if (operation is CdOperation cd)
    {
        if (cd.CdType is OutCdType)
            dir = dir.Parent!;

        if (cd.CdType is IntoCdType into)
            dir = (dir.Contents[into.Directory] as Directory)!;
    }

    if (operation is null)
    {
        var obj = GetOutputObject(line, dir);

        dir.Contents[obj.Name] = obj;
    }
}

const long TotalSize = 70000000;
const long TotalFreeSpaceNeeded = 30000000;

List<long> spacesThatCouldBeUsed = new();
long unusedSpace = 0, neededSpace = 0;

var rootSize = CalculateSize(rootDir, false);

unusedSpace = TotalSize - rootSize;
neededSpace = TotalFreeSpaceNeeded - unusedSpace;

CalculateSize(rootDir, true);

Console.WriteLine(spacesThatCouldBeUsed.OrderBy(s => s).First());

IOperation? GetOperation(string line)
{
    if (line.StartsWith("$ cd"))
    {
        var splitted = line.Split(' ');

        ICdType type = splitted[2] switch
        {
            ".." => new OutCdType(),
            _ => new IntoCdType(splitted[2]) };
        return new CdOperation(type);
    }
    else if (line.StartsWith("$ ls"))
    {
        return new LsOperation();
    }

    return null;
}

IObject GetOutputObject(string line, Directory parent)
{
    var splitted = line.Split(' ');

    var name = splitted[1];

    return splitted[0] switch
    {
        "dir" => new Directory(name, new Dictionary<string, IObject>(), parent),
        _ => new File(name, long.Parse(splitted[0]))
    };
}

long CalculateSize(Directory dir, bool populateList)
{
	long total = 0;
	foreach (var (_, item) in dir.Contents)
	{
		if (item is Directory subDir)
			total += CalculateSize(subDir, populateList);

		if (item is File file)
			total += file.Size;
	}

	if (populateList && total > neededSpace)
		spacesThatCouldBeUsed.Add(total);

	return total;
}


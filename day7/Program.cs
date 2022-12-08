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

long smallTotal = 0;

var total = CalculateSize(rootDir);

Console.WriteLine("total: {0}", smallTotal);

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

long CalculateSize(Directory dir)
{
	long total = 0;
	foreach (var (_, item) in dir.Contents)
	{
		if (item is Directory subDir)
			total += CalculateSize(subDir);

		if (item is File file)
			total += file.Size;
	}

	if (total < 100000)
		smallTotal += total;

	return total;
}


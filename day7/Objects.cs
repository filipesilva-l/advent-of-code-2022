interface IObject {
	string Name { get; }
}

record Directory(string Name, Dictionary<String, IObject> Contents, Directory? Parent): IObject;

record File(string Name, long Size): IObject;

interface IOperation {}

record LsOperation() : IOperation;

record CdOperation(ICdType CdType) : IOperation;

interface ICdType { }

record IntoCdType(string Directory): ICdType;

record OutCdType(): ICdType;


using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace UnionTypeTest;

public class FilesDataConnection : DataConnection
{
    public FilesDataConnection(LinqToDBConnectionOptions<FilesDataConnection> options)
        : base(options)
    {
    }

    public ITable<FileOrFolder> FilesAndFolders => this.GetTable<FileOrFolder>();
}

public abstract class FileOrFolder
{
    public int Id { get; set; }

    public int FolderId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; }
}

public class File : FileOrFolder
{
    public int Size { get; set; }
}

public class Folder : FileOrFolder
{
    public string? Description { get; set; } = string.Empty;
}

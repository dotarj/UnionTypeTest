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

    public ITable<Tenant> Tenants => this.GetTable<Tenant>();
}

public class Tenant
{
    public int Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public List<FileOrFolder> Files { get; set; } = new();
}

public abstract class FileOrFolder
{
    public int Id { get; set; }

    public int FolderId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = String.Empty;

    public int TenantId { get; set; }
}

public class File : FileOrFolder
{
    public int Size { get; set; }
}

public class Folder : FileOrFolder
{
    public string? Description { get; set; } = string.Empty;
}

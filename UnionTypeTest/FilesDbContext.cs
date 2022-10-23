using Microsoft.EntityFrameworkCore;

namespace UnionTypeTest;

public class FilesDbContext : DbContext
{
    public FilesDbContext(DbContextOptions<FilesDbContext> options)
        : base(options)
    {
    }

    public DbSet<FileOrFolder> FilesAndFolders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileOrFolder>(entity =>
        {
            entity.ToTable("FilesAndFolders");

            entity
                .HasDiscriminator<string>("Type")
                .HasValue<File>("FILE")
                .HasValue<Folder>("FOLDER");
        });
    }
}

[UnionType("FileOrFolder")]
public interface IFileOrFolder
{
    int Id { get; set; }

    int FolderId { get; set; }

    string Name { get; set; }
}

public abstract class FileOrFolder : IFileOrFolder
{
    public int Id { get; set; }

    public int FolderId { get; set; }

    public string Name { get; set; } = string.Empty;
}

public class File : FileOrFolder
{
    public int Size { get; set; }
}

public class Folder : FileOrFolder
{
    public string? Description { get; set; } = string.Empty;
}

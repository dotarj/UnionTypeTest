namespace UnionTypeTest;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<IFileOrFolder> GetFilesAndFolders([Service] FilesDbContext dbContext)
        => dbContext.FilesAndFolders;
}

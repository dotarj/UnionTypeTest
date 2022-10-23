using HotChocolate.Types.Pagination;

namespace UnionTypeTest;

public class Query
{
    public IQueryable<FileOrFolder> GetFilesAndFolders([Service] FilesDbContext dbContext)
        => dbContext.FilesAndFolders;
}

public class QueryObjectType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(query => query.GetFilesAndFolders(default!))
            .Type<ListType<FileOrFolderUnionType>>()
            .UseOffsetPaging(options: new PagingOptions() { IncludeTotalCount = true })
            .UseProjection()
            .UseFiltering()
            .UseSorting();
    }
}

public class FileOrFolderUnionType : UnionType
{
    protected override void Configure(IUnionTypeDescriptor descriptor)
    {
        descriptor.Name("FileOrFolder");
        descriptor.Type<FileObjectType>();
        descriptor.Type<FolderObjectType>();
    }
}

public class FileObjectType : ObjectType<File>
{
    protected override void Configure(IObjectTypeDescriptor<File> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }
}

public class FolderObjectType : ObjectType<Folder>
{
    protected override void Configure(IObjectTypeDescriptor<Folder> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }
}

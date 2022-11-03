using HotChocolate.Data.Sorting;
using HotChocolate.Types.Pagination;

namespace UnionTypeTest;

public class Query
{
    public IQueryable<object> GetTenants([Service] FilesDataConnection dataConnection)
        => dataConnection.Tenants;
}

public class QueryObjectType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(query => query.GetTenants(default!))
            .Type<ListType<TenantObjectType>>()
            .UseOffsetPaging<TenantObjectType>(options: new PagingOptions() { IncludeTotalCount = true })
            .UseProjection()
            .UseFiltering<Tenant>()
            .UseSorting<TenantSortInputType>();
    }
}

public class TenantObjectType : ObjectType<Tenant>
{
    protected override void Configure(IObjectTypeDescriptor<Tenant> descriptor)
    {
        var objectTypeDescriptor = descriptor.BindFieldsImplicitly();

        objectTypeDescriptor
            .Field(tenant => tenant.Files)
            .Type<ListType<FileOrFolderUnionType>>();
    }
}

public class FileOrFolderUnionType : UnionType
{
    protected override void Configure(IUnionTypeDescriptor descriptor)
    {
        descriptor.Name(nameof(FileOrFolder));
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

public class TenantSortInputType : SortInputType<Tenant>
{
    protected override void Configure(ISortInputTypeDescriptor<Tenant> descriptor)
    {
        descriptor.BindFieldsImplicitly();
    }
}

using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using LinqToDB.Mapping;
using UnionTypeTest;
using File = UnionTypeTest.File;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLinqToDBContext<FilesDataConnection>((provider, options) =>
    {
        options
            .UseMySql("")
            .UseDefaultLogging(provider);
    });

var fluentMappingBuilder = MappingSchema.Default.GetFluentMappingBuilder();

fluentMappingBuilder
    .Entity<Tenant>()
    .HasTableName("Tenants")
    .Association(tenant => tenant.Files, (tenant, file) => tenant.Id == file.TenantId);

fluentMappingBuilder
    .Entity<FileOrFolder>()
    .HasTableName("FilesAndFolders")
    .Inheritance(fileOrFolder => fileOrFolder.Type, "FILE", typeof(File))
    .Inheritance(fileOrFolder => fileOrFolder.Type, "FOLDER", typeof(Folder));

builder.Services
    .AddGraphQLServer()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<QueryObjectType>()
    .AddType<FileObjectType>()
    .AddType<FolderObjectType>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapGraphQL());

app.Run();
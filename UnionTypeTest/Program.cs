using Microsoft.EntityFrameworkCore;
using UnionTypeTest;
using File = UnionTypeTest.File;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FilesDbContext>(options =>
{
    var connectionString = "";
    var serverVersion = new MariaDbServerVersion(new Version(10, 4, 0));

    options.UseMySql(connectionString, serverVersion);
});

builder.Services
    .AddGraphQLServer()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContext<FilesDbContext>()
    .AddQueryType<QueryObjectType>()
    .AddType<FileObjectType>()
    .AddType<FolderObjectType>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapGraphQL());

app.Run();
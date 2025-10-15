IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> database = builder.AddPostgres("database")
    .WithPgAdmin()
    .WithDataVolume()
    .AddDatabase("library-db");

builder.AddProject<Projects.LibraryApp_API>("libraryapp-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();

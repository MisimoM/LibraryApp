var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithHostPort(5432)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var libraryDatabase = postgres.AddDatabase("library-db");

builder.AddProject<Projects.LibraryApp_API>("libraryapp-api")
    .WithReference(libraryDatabase)
    .WaitFor(libraryDatabase);

builder.Build().Run();

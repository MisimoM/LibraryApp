var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithHostPort(5432)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var libraryDatabase = postgres.AddDatabase("library-db");

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

builder.AddProject<Projects.LibraryApp_API>("libraryapp-api")
    .WithReference(libraryDatabase)
    .WaitFor(libraryDatabase)
    .WithReference(rabbitMq);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LibraryApp_API>("libraryapp-api");

builder.Build().Run();

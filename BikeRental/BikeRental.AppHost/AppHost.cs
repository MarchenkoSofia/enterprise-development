var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DbPassword", secret: true);

var sqlserver = builder.AddSqlServer("bikerental", password: password)
                       .AddDatabase("BikeRentalDb");
var generatorGrpc = builder.AddProject<Projects.BikeRental_Grpc_Server>("bikerental-grpc")
       .WithHttpEndpoint(port: 7298, name: "grpc");
builder.AddProject<Projects.BikeRental_Api>("bikerental-api")
       .WithReference(sqlserver, "Database")
       .WaitFor(sqlserver)
       .WithEnvironment("GeneratorGrpcUrl", generatorGrpc.GetEndpoint("grpc"))
       .WaitFor(generatorGrpc); 

builder.Build().Run();

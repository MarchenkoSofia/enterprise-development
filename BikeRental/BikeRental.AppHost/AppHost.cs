var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DbPassword", secret: true);

var sqlserver = builder.AddSqlServer("bikerental", password: password)
                       .AddDatabase("BikeRentalDb");

builder.AddProject<Projects.BikeRental_Api>("bikerental-api")
       .WithReference(sqlserver, "Database")
       .WaitFor(sqlserver); 

builder.Build().Run();

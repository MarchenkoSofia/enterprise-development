using BikeRental.Grpc.Server;
using BikeRenter.ServiceDefaults;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGrpcService<ModelGeneratorService>();

app.Run();

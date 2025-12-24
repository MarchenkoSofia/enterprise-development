using BikeRental.Grpc.Server;
using BikeRenter.ServiceDefaults;
using Microsoft.AspNetCore.Builder;
using BikeRental.Grpc.Server.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddGrpc();
builder.Services.AddSingleton<IBikeModelFactory, RandomBikeModelFactory>();

var app = builder.Build();
app.MapDefaultEndpoints();
app.MapGrpcService<ModelGeneratorService>();
app.Run();

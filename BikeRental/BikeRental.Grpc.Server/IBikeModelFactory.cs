using Bikes.Contracts.Grpc;


namespace BikeRental.Grpc.Server;
public interface IBikeModelFactory
{
    AddModelRequest Create();
}


using Grpc.Net.Client;
using LabApp.gRPC; 
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {

        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        using var channel = GrpcChannel.ForAddress("https://localhost:7199", new GrpcChannelOptions
        {
            HttpHandler = handler
        });
        var client = new DeviceServiceGrpc.DeviceServiceGrpcClient(channel);

        Console.Write("Podaj ID urządzenia: ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Niepoprawne ID");
            return;
        }

        var reply = await client.GetDeviceByIdAsync(new DeviceRequest { Id = id });

        Console.WriteLine($"ID: {reply.Id}");
        Console.WriteLine($"Nazwa: {reply.Name}");
        Console.WriteLine($"Model: {reply.Model}");
        Console.WriteLine($"Czy sprawne: {(reply.IsOperational ? "Tak" : "Nie")}");
    }
}

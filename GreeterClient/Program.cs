using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GreeterClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Client GRPC");

            // Create a gRPC channel to the server at localhost:7001
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7001");

            // Create a client for the Greeter service
            Greeter.GreeterClient client = new Greeter.GreeterClient(channel);

            // Call the SayHello method on the server with the name "World"
            //var request = new HelloRequest { Name = "World" };
            //var response = await client.SayHelloAsync(request);

            //// Output the greeting message received from the server
            //Console.WriteLine("Greeting: " + response.Message);

            // Clean up resources
            await channel.ShutdownAsync();


            Task tskRequest = Task.Run(async () =>
            {
                int counter = 1;
                while (true)
                {
                    HelloRequest request = new HelloRequest { Name = "World " + counter++ };
                    var response = await client.SayHelloAsync(request);

                    // Output the greeting message received from the server
                    Console.WriteLine("Greeting: " + response);

                    Thread.Sleep(4500);
                }
            });

            Task tskStream= Task.Run(async () =>
            {
                HelloRequest request = new HelloRequest { Name = "World Streaming" };

                using AsyncServerStreamingCall<HelloReply> call = client.SayHelloStream(request);

                // Asynchronously read the streamed responses from the server
                await foreach (HelloReply reply in call.ResponseStream.ReadAllAsync<HelloReply>())
                {
                    Console.WriteLine("Streamed Greeting: " + reply.Message);
                }
            });


            Task.WaitAll(tskRequest, tskStream);
        }
    }

}
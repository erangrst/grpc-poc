using Grpc.Core;
using GrpcServer;
using System.Threading.Tasks;

namespace GreeterServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private static int counter = 1;
        private static int counter2 = 100;

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine("server: " + request.Name);

            var message = $"Hello, {request.Name}!";
            var reply = new HelloReply { Message = message };
            return Task.FromResult(reply);
        }

        public override Task<HelloReply> SayHello2(HelloRequest2 request, ServerCallContext context)
        {
            Console.WriteLine("server Hello 2: " + request.Name);

            var message = $"Hello, {request.Name} {request.Id}!  {counter2}";
            var reply = new HelloReply { Message = message };

            counter2 += 10;
            return Task.FromResult(reply);
        }

        public override async Task<HelloReply> SayHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            Console.WriteLine("GET SayHelloStream of " + request.Name);

            // For example, send 5 greeting messages
            while (true)
            {
                Console.WriteLine("start round server: " + request.Name);

                // Create a message that changes with each iteration
                var reply = new HelloReply { Message = $"Hello streaming Message {request.Name}  {counter++}" };

                // Write the message to the response stream
                await responseStream.WriteAsync(reply);

                // Simulate a delay between messages (e.g., 1 second)
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }


    }
}

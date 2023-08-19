using NATS.Client;
using Newtonsoft.Json;
using Service.Dtos;
using Service.Services;
using System.Text;

namespace Service.BackgroundServices
{
    public class Subscriber : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Subscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Subscribe();
        }

        private async Task Subscribe()
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                /*****SignUp User*****/

                connection.SubscribeAsync("user-signup-request", (sender, args) =>
                {
                    string request = Encoding.UTF8.GetString(args.Message.Data);
                    Console.WriteLine($"Received request: {request}");

                    // Process the request and generate a response

                    string signUpResult = "";

                    Task.Run(async () =>
                    {
                        signUpResult = await SignUpUser(request);
                    });

                    string signUpResponse = $"User Created Successfully , {request}!";
                    byte[] signUpResponsePayload = Encoding.UTF8.GetBytes(signUpResponse);

                    // Publish the response to the reply subject
                    connection.Publish(args.Message.Reply, signUpResponsePayload);
                });

                /*****Get User*****/

                connection.SubscribeAsync("get-user-request", (sender, args) =>
                {
                    string request = Encoding.UTF8.GetString(args.Message.Data);
                    Console.WriteLine($"Received request: {request}");

                    string getResult = "";

                    Task.Run(async () =>
                    {
                        getResult = await SignUpUser(request);
                    });

                    string getUserResponse = $"User Created Successfully , {request}!";
                    byte[] getUserResponsePayload = Encoding.UTF8.GetBytes(getResult);

                    connection.Publish(args.Message.Reply, getUserResponsePayload);
                });

                Console.WriteLine("User Server is listening...");
                Console.ReadLine();


            }
        }

        private async Task<string> SignUpUser(string request)
        {
            var newUser = JsonConvert.DeserializeObject<RequestDto>(request);

            if (newUser is null)
            {
                return "No User Added !";
            }

            using IServiceScope scope = _serviceProvider.CreateScope();

            var _userManagement = scope.ServiceProvider.GetRequiredService<IUserManagement>();

            var result = await _userManagement.SignUp(newUser);

            return JsonConvert.SerializeObject(result);
        }

        private async Task<string> GetUser(string request)
        {
            var userId = JsonConvert.DeserializeObject<Guid>(request);

            if (userId == Guid.Empty)
            {
                return "User Not Found !";
            }

            using IServiceScope scope = _serviceProvider.CreateScope();

            var _userManagement = scope.ServiceProvider.GetRequiredService<IUserManagement>();

            var result = await _userManagement.Get(userId);

            return JsonConvert.SerializeObject(result);
        }

    }
}

using Client.ViewModels;
using NATS.Client;
using System.Text;
using System.Text.Json;

using (IConnection connection = new ConnectionFactory().CreateConnection())
{
    /******User SignUp******/
    // The request data

    var signUpRequest = new CreateUserRequestVM()
    {
        Name = "shivaa",
        Age = 30,
        Password = "aA123"
    }; 

    var signUpRequestMessage = JsonSerializer.Serialize(signUpRequest);

    // Create an inbox subject for receiving the response
    string signUpInbox = Guid.NewGuid().ToString();
    IAsyncSubscription subscription = connection.SubscribeAsync(signUpInbox, (sender, args) =>
    {
        string response = Encoding.UTF8.GetString(args.Message.Data);
        Console.WriteLine($"Received response: {response}");
    });

    // Publish the request and expect a response on the inbox subject
    byte[] signUpRequestPayload = Encoding.UTF8.GetBytes(signUpRequestMessage);
    connection.Publish("user-signup-request", signUpInbox, signUpRequestPayload);

    Console.WriteLine("User Client sent request...");
    Console.ReadLine();


    /******Get User******/

    var getUserRequest = Guid.NewGuid().ToString();

    string getUserInbox = Guid.NewGuid().ToString();

    IAsyncSubscription getSubscription = connection.SubscribeAsync(getUserInbox, (sender, args) =>
    {
        string response = Encoding.UTF8.GetString(args.Message.Data);
        Console.WriteLine($"Received response: {response}");
    });

    byte[] getUserRequestPayload = Encoding.UTF8.GetBytes(getUserRequest);
    connection.Publish("get-user-request", signUpInbox, getUserRequestPayload);

    Console.WriteLine("User Client sent request...");
    Console.ReadLine();
}


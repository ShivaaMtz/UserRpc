//using Client.Mapping;
//using Client.ViewModels;
//using Grpc.Net.Client;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//using UserGrpcService.Protos;

//namespace Client.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly GrpcChannel _channel;
//        private readonly UserService.UserServiceClient _userServiceClient;
//        private readonly IConfiguration _configuration;

//        public UserController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//            _channel = GrpcChannel.ForAddress(_configuration.GetValue<String>("UserServiceUrl"));
//            _userServiceClient = new UserService.UserServiceClient(_channel);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetAsync(Guid id)
//        {
//            var request = new UserId { Value = id.ToString() };

//            var result = await _userServiceClient.GetAsync(request);

//            return Ok(result);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequestVM request)
//        {
//            var result = await _userServiceClient.SignUpAsync(request.ToRequestModel());

//            return StatusCode((int)HttpStatusCode.Created, result);
//        }

//    }
//}

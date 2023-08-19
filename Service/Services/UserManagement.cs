using Service.Dtos;
using Service.Mapping;
using Service.Repositories;

namespace Service.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly IUserRepository _userRepository;

        public UserManagement(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Get(Guid id)
        {
            var result = await _userRepository.GetAsync(id);

            return result.ToDto();
        }

        public async Task<UserDto> SignUp(RequestDto requestDto)
        {
            var result = await _userRepository.AddAsync(requestDto.ToModel());

            return result.ToDto();
        }
    }
}

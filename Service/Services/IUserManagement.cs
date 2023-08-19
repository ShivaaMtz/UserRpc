using Service.Dtos;

namespace Service.Services
{
    public interface IUserManagement
    {
        Task<UserDto> SignUp(RequestDto requestDto);

        Task<UserDto> Get(Guid id);
    }
}

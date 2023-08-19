using Service.Dtos;
using Service.Entities;
using Service.Helpers;

namespace Service.Mapping
{
    internal static  class UserMapper
    {
        internal static User ToModel(this RequestDto request)
        {
            return new User
            {
                Name = request.Name,
                Age = request.Age,
                PasswordHash = request.Password.CreateHash()
            };
        }

        internal static UserDto ToDto(this User model)
        {
            return new UserDto()
            {
                Id = model.Id,
                Name = model.Name,
                Age = model.Age,
                PasswordHash = model.PasswordHash,
            };
        }

    }
}

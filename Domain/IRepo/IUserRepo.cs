using Domain.AccountProperties.UserEntity;

namespace Domain.IRepo;

public interface IUserRepo
{
    public Task<User?> GetUserById(Guid id);
    public Task UpdateUser(User user);
}
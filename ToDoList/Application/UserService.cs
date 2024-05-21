using System.Threading.Tasks;
using ToDoList.Domain;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            return await _repository.Authenticate(username, password);
        }

        public async Task<User> Register(User user)
        {
            // Check if the username already exists
            var existingUser = await _repository.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                throw new Exception("Username is already taken.");
            }

            // Assign a unique ID to the new user
            user.UserId = _repository.GetMaxUserId() + 1;

            // Add the user to the repository
            await _repository.Add(user);

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _repository.GetUserByUsername(username);
        }
        public async Task Add(User user)
        {
            await _repository.Add(user);
        }

    }

}

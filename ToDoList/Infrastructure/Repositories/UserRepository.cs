using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain;

namespace ToDoList.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _inMemoryDatabase;

        public UserRepository()
        {
            // Initialize the in-memory database with sample users
            _inMemoryDatabase = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Username = "user1",
                    Password = "password1"
                    // Add any other properties you want to initialize
                },
                 new User
                {
                    UserId = 1,
                    Username = "hazel",
                    Password = "sagre"
                    // Add any other properties you want to initialize
                },
                 new User
                {
                    UserId = 1,
                    Username = "paolo",
                    Password = "deleon"
                    // Add any other properties you want to initialize
                },
                  new User
                {
                    UserId = 1,
                    Username = "kenjie",
                    Password = "baniqued"
                    // Add any other properties you want to initialize
                },
                new User
                {
                    UserId = 2,
                    Username = "kevin",
                    Password = "francisco"
                    // Add any other properties you want to initialize
                }
                // Add more sample users as needed
            };
        }


        public async Task<User> Authenticate(string username, string password)
        {
            // Find the user with the matching username and password
            return await Task.FromResult(_inMemoryDatabase.FirstOrDefault(u => u.Username == username && u.Password == password));
        }

        public async Task<User> Register(User user)
        {
            // Add the user to the in-memory database
            _inMemoryDatabase.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User> GetById(int id)
        {
            // Find the user with the matching ID
            return await Task.FromResult(_inMemoryDatabase.FirstOrDefault(u => u.UserId == id));
        }

        // Add the GetUserByUsername method
        public async Task<User> GetUserByUsername(string username)
        {
            // Find the user with the matching username
            return await Task.FromResult(_inMemoryDatabase.FirstOrDefault(u => u.Username == username));
        }
        public int GetMaxUserId()
        {
            return _inMemoryDatabase.Max(u => u.UserId);
        }
        public async Task Add(User user)
        {
            // Assign a unique ID to the new user
            user.UserId = _inMemoryDatabase.Count > 0 ? _inMemoryDatabase.Max(u => u.UserId) + 1 : 1;
            _inMemoryDatabase.Add(user);
            await Task.CompletedTask;
        }

    }
}

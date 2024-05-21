using System.Threading.Tasks;
using ToDoList.Domain;

namespace ToDoList.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(User user);
        Task<User> GetById(int id);
        Task<User> GetUserByUsername(string username);
        int GetMaxUserId();
        Task Add(User user);


    }
}

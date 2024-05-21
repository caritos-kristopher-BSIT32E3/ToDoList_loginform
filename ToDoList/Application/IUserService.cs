using System.Threading.Tasks;
using ToDoList.Domain;

namespace ToDoList.Application
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> Register(User user);
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task Add(User user);
    }
}

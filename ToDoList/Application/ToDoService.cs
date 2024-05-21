using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Domain;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Application
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task AddToDoItem(ToDoItem item)
        {
            await _repository.Add(item);
        }


        public async Task<List<ToDoItem>> GetPendingToDoItemsByUserId(int userId)
        {
            return await _repository.GetByUserIdAndStatus(userId, false);
        }

        public async Task<List<ToDoItem>> GetCompletedToDoItemsByUserId(int userId)
        {
            return await _repository.GetByUserIdAndStatus(userId, true);
        }
        public async Task<ToDoItem> GetById(int id)
        {
            return await _repository.GetById(id);
        }


        public async Task UpdateToDoItem(ToDoItem item)
        {
            await _repository.Update(item);
        }
        public async Task Update(ToDoItem item)
        {
            await _repository.Update(item);
        }

    }
}

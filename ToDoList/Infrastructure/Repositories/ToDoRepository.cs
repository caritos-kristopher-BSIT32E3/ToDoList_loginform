using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain;

namespace ToDoList.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly List<ToDoItem> _inMemoryDatabase;
        private readonly List<User> _inMemoryUserDatabase;

        public ToDoRepository()
        {
            _inMemoryDatabase = new List<ToDoItem>();
            _inMemoryUserDatabase = new List<User>();
        }

        public async Task Add(ToDoItem item)
        {
            lock (_inMemoryDatabase)
            {
                item.Id = _inMemoryDatabase.Count > 0 ? _inMemoryDatabase.Max(i => i.Id) + 1 : 1;
                _inMemoryDatabase.Add(item);
            }

            await Task.CompletedTask;
        }




        public async Task<List<ToDoItem>> GetByUserId(int userId)
        {
            return await Task.FromResult(_inMemoryDatabase.Where(i => i.UserId == userId).ToList());
        }

        public async Task<ToDoItem> GetById(int id)
        {
            return await Task.FromResult(_inMemoryDatabase.FirstOrDefault(i => i.Id == id));
        }

        public async Task<List<ToDoItem>> GetByUserIdAndStatus(int userId, bool isDone)
        {
            return await Task.FromResult(_inMemoryDatabase.Where(i => i.UserId == userId && i.IsDone == isDone).ToList());
        }

        public async Task Update(ToDoItem item)
        {
            var existingItem = _inMemoryDatabase.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Title = item.Title;
                existingItem.Description = item.Description;
                existingItem.DueDate = item.DueDate;
                existingItem.IsDone = item.IsDone;
            }
            await Task.CompletedTask;
        }
        public async Task Register(User user)
        {
            lock (_inMemoryUserDatabase) // Change this line
            {
                user.UserId = _inMemoryUserDatabase.Count > 0 ? _inMemoryUserDatabase.Max(u => u.UserId) + 1 : 1;
                _inMemoryUserDatabase.Add(user); // And this line
            }

            await Task.CompletedTask;
        }
        public async Task<ProgressReport> GetProgressReport(int userId)
        {
            var totalTasks = _inMemoryDatabase.Count(i => i.UserId == userId);
            var completedTasks = _inMemoryDatabase.Count(i => i.UserId == userId && i.IsDone);
            var pendingTasks = totalTasks - completedTasks;
            var completionPercentage = totalTasks > 0 ? (completedTasks / (double)totalTasks) * 100 : 0;

            var report = new ProgressReport
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                CompletionPercentage = completionPercentage
            };

            return await Task.FromResult(report);
        }


    }
}

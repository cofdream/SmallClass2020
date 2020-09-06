using System.Collections.Generic;

namespace TodoPro.Utility
{
    public interface ITodoStorage
    {
        void SaveTodos(List<Todo> todos);
        List<Todo> LoadTodos();
        void RemoveStorageFile();
    }
}

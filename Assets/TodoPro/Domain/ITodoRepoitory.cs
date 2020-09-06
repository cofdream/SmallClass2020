using System.Collections.Generic;

namespace TodoPro.Doma
{
    public interface ITodoRepoitory
    {
        void Add(Todo todo);
        void Remove(Todo todo);
        void Remove(string id);
        void Update(Todo todo);
        void Save();
        void Load();

        List<Todo> GetTodos();
    }
}

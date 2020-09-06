using System.Collections.Generic;
using TodoPro.Doma;

namespace TodoPro.System.Query
{
    public class GetAllTodosQuery
    {
        public List<Todo> Do()
        {
            var todoRepoitory = TodoProApp.DomainLayer.Get<ITodoRepoitory>();
            var todos = todoRepoitory.GetTodos();
            return todos;
        }
    }
}

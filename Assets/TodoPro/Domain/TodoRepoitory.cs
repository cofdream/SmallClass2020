using System.Collections.Generic;
using TodoPro.Command;
using TodoPro.Utility;

namespace TodoPro.Doma
{
    public class TodoRepoitory : ITodoRepoitory
    {
        private List<Todo> todos = null;

        public void Add(Todo todo)
        {
            todos.Add(todo);
        }

        public List<Todo> GetTodos()
        {
            return todos;
        }

        public void Remove(Todo todo)
        {
            todos.Remove(todo);
        }

        public void Remove(string id)
        {
            todos.RemoveAll(_todo => _todo.Id == id);
        }

        public void Save()
        {
            if (todos == null)
            {
                todos = new List<Todo>();
            }
            TodoProApp.UtilityLayer.Get<ITodoStorage>().SaveTodos(todos);
        }

        public void Load()
        {
            var data = TodoProApp.UtilityLayer.Get<ITodoStorage>().LoadTodos();

            if (data == null )
            {
                SetDefineTodoValue();
            }
            else
            {
                todos = data;
            }
        }

        public void Update(Todo todo) { }

        private void SetDefineTodoValue()
        {
            todos = new List<Todo>();
            new AddTodoCommand("6:30 起床").Execute();
            new AddTodoCommand("7:05 去上班").Execute();
            new AddTodoCommand("12:00 吃午饭").Execute();
            new AddTodoCommand("13：00 去午休").Execute();
            new AddTodoCommand("18：00 下班回家").Execute();
        }
    }
}

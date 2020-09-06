using System;
using TodoPro.Doma;

namespace TodoPro.Command
{
    public class AddTodoCommand
    {
        private readonly string content;

        private ITodoRepoitory todoRepoitory = null;
        public AddTodoCommand(string content)
        {
            this.content = content;
            todoRepoitory = TodoProApp.DomainLayer.Get<ITodoRepoitory>();
        }

        public void Execute()
        {
            var todo = new Todo() { Id = Guid.NewGuid().ToString(), Content = content, Finished = string.Empty };
            todoRepoitory.Add(todo);
        }
    }
}

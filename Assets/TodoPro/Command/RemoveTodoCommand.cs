using TodoPro.Doma;

namespace TodoPro.Command
{
    public class RemoveTodoCommand
    {
        private readonly string id = string.Empty;

        private ITodoRepoitory todoRepoitory = null;

        public RemoveTodoCommand(string id)
        {
            this.id = id;
            todoRepoitory = TodoProApp.DomainLayer.Get<ITodoRepoitory>();
        }

        public void Execute()
        {
            todoRepoitory.Remove(id);
        }
    }
}

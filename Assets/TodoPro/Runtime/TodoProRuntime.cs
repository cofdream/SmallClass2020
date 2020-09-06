using System.Collections.Generic;
using TodoPro.Command;
using TodoPro.Doma;
using TodoPro.System.Query;
using TodoPro.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace TodoPro.Runtime
{
    public class TodoProRuntime : MonoBehaviour
    {
        private List<Todo> todos;

        [SerializeField] private Transform todosTran = null;
        [SerializeField] private GameObject todoItem = null;

        [SerializeField] private InputField inputTodo = null;
        [SerializeField] private Button AddTodo = null;

        private void Awake()
        {
            TodoProApp.ConfigLayers();
            TodoProApp.DomainLayer.Get<ITodoRepoitory>().Load();

            todos = new GetAllTodosQuery().Do();
            RefreshView();

            AddTodo.onClick.AddListener(() =>
            {
                new AddTodoCommand(inputTodo.text).Execute();
                inputTodo.text = string.Empty;

                todos = new GetAllTodosQuery().Do();
                RefreshView();
            });
        }

        private void RefreshView()
        {
            foreach (Transform todoChild in todosTran)
            {
                Destroy(todoChild.gameObject);
            }
            todos.ForEach(todo =>
            {
                var item = Instantiate(todoItem).transform;
                item.Find("Content").GetComponent<Text>().text = todo.Content;
                item.Find("btn Delete").GetComponent<Button>().onClick.AddListener(() =>
                {
                    new RemoveTodoCommand(todo.Id).Execute();
                    RefreshView();
                });

                item.SetParent(todosTran);
            });
        }

        private void OnDestroy()
        {
            TodoProApp.DomainLayer.Get<ITodoRepoitory>().Save();
            TodoProApp.ClearLayers();
        }
    }
}

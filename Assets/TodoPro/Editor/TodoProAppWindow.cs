using System.Collections.Generic;
using TodoPro.Command;
using TodoPro.Doma;
using TodoPro.System.Query;
using TodoPro.Utility;
using UnityEditor;
using UnityEngine;

namespace TodoPro
{
    public class TodoProAppWindow : EditorWindow
    {
        private List<Todo> todos;
        private string inputContent;

        [MenuItem("FrameLearn/Todo Data Remove", false, 2)]
        public static void RemoveData()
        {
            TodoProApp.ConfigLayers();
            TodoProApp.UtilityLayer.Get<ITodoStorage>().RemoveStorageFile();
            TodoProApp.ClearLayers();
        }


        [MenuItem("FrameLearn/Todo Pro", false, 1)]
        public static void Open()
        {
            var window = GetWindow<TodoProAppWindow>();
            Vector2 size = new Vector2(Screen.width, Screen.height) * 0.5f;
            Vector2 pos = new Vector2(Screen.width - size.x, Screen.height - size.y) * 0.5f;
            window.position = new Rect(pos, size);
            window.titleContent = new GUIContent("Todo Pro");

            window.Init();
            window.Show();
        }


        void Init()
        {
            TodoProApp.ConfigLayers();
            TodoProApp.DomainLayer.Get<ITodoRepoitory>().Load();

            todos = new GetAllTodosQuery().Do();
        }

        private void OnDestroy()
        {
            TodoProApp.DomainLayer.Get<ITodoRepoitory>().Save();
            TodoProApp.ClearLayers();
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            {
                inputContent = GUILayout.TextField(inputContent);

                if (GUILayout.Button("Add", GUILayout.Width(35)))
                {
                    new AddTodoCommand(inputContent).Execute();

                    todos = new GetAllTodosQuery().Do();

                    inputContent = string.Empty;
                }
            }
            GUILayout.EndHorizontal();

            foreach (var todo in todos)
            {
                RenderTodo(todo, out bool isRemove);
                if (isRemove) return;
            }

        }

        private void RenderTodo(Todo todo, out bool isRemove)
        {
            GUILayout.BeginHorizontal("box");

            GUILayout.Toggle(false, todo.Content);
            isRemove = GUILayout.Button("Remove", GUILayout.Width(70));
            if (isRemove)
            {
                new RemoveTodoCommand(todo.Id).Execute();
                todos = new GetAllTodosQuery().Do();
            }

            GUILayout.EndHorizontal();
        }
    }

}
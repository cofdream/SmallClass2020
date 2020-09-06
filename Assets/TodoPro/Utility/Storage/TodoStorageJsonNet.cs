using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TodoPro.Utility;
using UnityEngine;

namespace TodoPro.Utility
{
    public class TodoStorageJsonNet : ITodoStorage
    {
        public readonly string Path  = Application.persistentDataPath + "/" + TodoStroageConfig.SOROTAGE_FILE_NAME;

        public TodoStorageJsonNet()
        {
            if (File.Exists(Path) == false)
            {
                File.Create(Path).Close();
            }
            Debug.Log(Path);
        }

        public List<Todo> LoadTodos()
        {
            if (File.Exists(Path))
            {
                var data = File.ReadAllText(Path);
                var todos = JsonConvert.DeserializeObject<List<Todo>>(data);
                return todos;
            }
            return null;
        }

        public void SaveTodos(List<Todo> todos)
        {
            if (File.Exists(Path))
            {
                var data = JsonConvert.SerializeObject(todos);
                File.WriteAllText(Path, data);
            }
        }

        public void RemoveStorageFile()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
    }
}

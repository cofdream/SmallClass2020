using FrameLearn.Fullstack;
using TodoPro.Doma;
using TodoPro.Utility;

namespace TodoPro
{
    public static class TodoProApp
    {
        public static IFrameworkLayer DomainLayer { get; private set; } = new FrameworkLayer();
        public static IFrameworkLayer UtilityLayer { get; private set; } = new FrameworkLayer();

        public static void ConfigLayers()
        {
            DomainLayer.Register<ITodoRepoitory>(new TodoRepoitory());
            UtilityLayer.Register<ITodoStorage>(new TodoStorageJsonNet());
        }

        public static void ClearLayers()
        {
            DomainLayer.Clear();
            UtilityLayer.Clear();
        }
    }
}
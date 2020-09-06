

namespace FrameLearn.Fullstack
{
    public interface IFrameworkLayer
    {
        void Register<T>(object obj);
        T Get<T>() where T : class;
        void Clear();
    }
}

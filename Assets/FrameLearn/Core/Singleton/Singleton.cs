namespace FrameLearn
{
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        protected Singleton() { }

        static Singleton() { Lock = new object(); }

        protected static T instance;

        protected static object Lock;

        public static T Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = SingletonCreator.CreateSingleton<T>();
                        instance.SingletonInit();
                    }
                }
                return instance;
            }
        }

        protected virtual void SingletonInit() { }

        protected virtual void Dispose() { instance = null; }
    }
}

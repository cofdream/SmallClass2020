using System;

namespace DevilAngel
{
    public class Registerations<T> : IRegisterations
    {
        public Action<T> OnReceives = (t) => { };
    }
}
using System;

namespace PPop.Core.Helpers 
{
    public class Singleton<T> where T : new()
    {
        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

        public static T Instance => lazy.Value;

        protected Singleton() { }
    }
}
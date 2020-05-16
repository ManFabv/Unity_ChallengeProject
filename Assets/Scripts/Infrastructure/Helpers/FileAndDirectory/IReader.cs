using System;

namespace PPop.Infrastructure.Helpers.FileAndDirectory
{
    public interface IReader
    {
        T Read<T>(string fileName) where T : UnityEngine.Object;
    
        string Read(string fileName);

        string ReadSchema(Type typeOfSchema);
    }
}

using System;

public interface IReader
{
    string Read(string fileName);
    string ReadSchema(Type typeOfSchema);
}

public interface ILevel
{
    void LoadLevel(string pathToLevel);
    bool IsLoaded { get; }
}
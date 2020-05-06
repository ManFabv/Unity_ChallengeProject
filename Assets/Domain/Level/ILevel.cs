public interface ILevel
{
    void LoadLevel(string source);
    bool IsLoaded { get; }
}
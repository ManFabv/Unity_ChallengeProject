public interface ILevel
{
    void LoadLevel(string pathToLevel);
    void LoadLevel(string rootFolder, int level);
    void FillMap<T>(ref T map);
    bool IsLoaded { get; }
}
using Zenject;

public class GridLevel : ILevel
{
    private readonly ILoaderService _loaderService;
    private LevelTileData Tiles;

    [Inject]
    public GridLevel(ILoaderService loaderService)
    {
        _loaderService = loaderService;
    }

    public void LoadLevel(string path)
    {
        Tiles = _loaderService.Read<LevelTileData>(path);
    }

    public bool IsLoaded => Tiles?.LevelTilesCount > 0;
}
using Zenject;

public class GridLevel : ILevel
{
    private readonly ILoaderService _loaderService;
    private LevelTileData TilesData;

    [Inject]
    public GridLevel(ILoaderService loaderService)
    {
        _loaderService = loaderService;
    }

    public void LoadLevel(string pathToLevel)
    {
        TilesData = _loaderService.Read<LevelTileData>(pathToLevel);
    }

    public bool IsLoaded => TilesCount > 0;

    public int TilesCount => TilesData?.Tiles?.Length ?? 0;
}
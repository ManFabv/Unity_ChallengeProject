namespace PPop.Game.LevelManagers 
{
    public interface ILevelStateManager<T>
    {
        void Init();
        void ChangeState(T newState);
        T GetCurrentState();
    }
}

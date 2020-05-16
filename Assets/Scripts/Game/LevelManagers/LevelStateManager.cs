using System;
using PPop.Core.Helpers;
using PPop.Domain.Tiles;

namespace PPop.Game.LevelManagers 
{
    public class LevelStateManager<T> : Singleton<T>, ILevelStateManager<T> where T : TileNode, new()
    {
        private IFSM<T> CurrentState;

        public LevelStateManager(IFSM<T> initialState, T node)
        {
            if (initialState is null) throw new ArgumentNullException($"Required initial state {typeof(IFSM<T>).Name} of parameter {nameof(initialState)} is null");

            CurrentState = initialState;
            CurrentState.Init(node);
        }

        public void Execute(T node)
        {
            CurrentState.Execute(node);
        }

        public void ChangeState(IFSM<T> newState, T node)
        {
            if (newState is null) throw new ArgumentNullException($"New state {typeof(IFSM<T>).Name} of parameter {nameof(newState)} is null");

            CurrentState.Exit(node);
            CurrentState = newState;
            CurrentState.Init(node);
        }

        public Type GetCurrentState()
        {
            return CurrentState.GetType();
        }
    }
}

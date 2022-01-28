using Domains.Core.Subdomains.Battle.Code.Interfaces;

namespace Domains.Core.Subdomains.Battle.Code.Implementations
{
    public class GameState : IGameState
    {
        public IFighter[] Fighters { get; }
        public IFighter CurrentFighter => Fighters[_currentFighterIndex];
        public bool IsGameOver => Winner != null;
        public IFighter Winner { get; private set; }

        private int _currentFighterIndex = 0;

        public GameState(IFighter[] fighters)
        {
            Fighters = fighters;
            Winner = null;
        }
    }
}
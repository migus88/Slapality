using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Implementations
{
    public class GameState : IGameState
    {
        public IFighter[] Fighters => new[] { _player1, _player2 };
        public IFighter CurrentFighter => Fighters[_currentFighterIndex];
        public IFighter CurrentEnemy => Fighters[EnemyIndex];
        public bool IsGameOver => Winner != null;
        public IFighter Winner { get; private set; }
        private int EnemyIndex => _currentFighterIndex == 0 ? 1 : 0;

        private int _currentFighterIndex = 0;

        [Inject(Id = PlayerType.Workaholic)] private IFighter _player1;
        [Inject(Id = PlayerType.Bum)] private IFighter _player2;

        public void NextRound()
        {
            _currentFighterIndex = EnemyIndex;
        }
    }
}
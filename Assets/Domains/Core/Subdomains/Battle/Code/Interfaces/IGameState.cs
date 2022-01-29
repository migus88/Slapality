using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IGameState
    {
        IFighter[] Fighters { get; }
        IFighter CurrentFighter { get; }
        IFighter CurrentEnemy { get; }
        bool IsGameOver { get; }
        IFighter Winner { get; }

        void NextRound();
    }
}
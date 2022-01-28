using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IFighterView
    {
        IFighter Fighter { get; }
        MovementDirection HeadPosition { get; }

        UniTask Attack(MovementDirection direction);
        UniTask TurnHead(MovementDirection direction);
        UniTask GetHit();
        UniTask Die();
    }
}
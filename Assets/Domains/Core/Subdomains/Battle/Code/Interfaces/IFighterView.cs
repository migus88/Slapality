using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IFighterView
    {
        void Attack(MovementDirection direction);
        void TurnHead(MovementDirection direction);
        void Die();
    }
}
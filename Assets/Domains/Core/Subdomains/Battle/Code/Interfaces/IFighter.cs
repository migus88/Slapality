using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IFighter
    {
        PlayerType PlayerType { get; }
        float CurrentHP { get; }
        bool IsDead { get; }
    }
}
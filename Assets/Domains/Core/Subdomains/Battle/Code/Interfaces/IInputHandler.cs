using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IInputHandler
    {
        bool IsChargeButtonPressed { get; }
        bool IsGameButtonPressed { get; }
        GameButtonType LastGameButtonPressed { get; }

        void ResetLastDirection();
    }
}
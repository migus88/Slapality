using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IFighterView
    {
        void AnimateDodge();
        void AnimateSlap();
        void AnimateCritSlap();
        void AnimateDeath();

        UniTask Hide();
        UniTask Show();
    }
}
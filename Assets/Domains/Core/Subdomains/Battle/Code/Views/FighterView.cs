using System;
using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Views
{
    public class FighterView : MonoBehaviour, IFighterView
    {
        private static readonly int DodgeTriggerAnimatorKey = Animator.StringToHash("DodgeTrigger");
        private static readonly int SlapTriggerAnimatorKey = Animator.StringToHash("SlapTrigger");
        private static readonly int CritSlapTriggerAnimatorKey = Animator.StringToHash("CritSlapTrigger");
        private static readonly int DeathTriggerAnimatorKey = Animator.StringToHash("DeathTrigger");
        private static readonly int ShowTriggerAnimatorKey = Animator.StringToHash("ShowTrigger");
        private static readonly int HideTriggerAnimatorKey = Animator.StringToHash("HideTrigger");
        

        [SerializeField] private Animator _animator;
        [SerializeField] private float _showHideDuration = 1f;
        

        public void AnimateDodge()
        {
            _animator.SetTrigger(DodgeTriggerAnimatorKey);
        }

        public void AnimateSlap()
        {
            _animator.SetTrigger(SlapTriggerAnimatorKey);
        }

        public void AnimateCritSlap()
        {
            _animator.SetTrigger(CritSlapTriggerAnimatorKey);
        }

        public void AnimateDeath()
        {
            _animator.SetTrigger(DeathTriggerAnimatorKey);
        }

        public async UniTask Hide()
        {
            _animator.SetTrigger(HideTriggerAnimatorKey);
            await UniTask.Delay(TimeSpan.FromSeconds(_showHideDuration));
        }

        public async UniTask Show()
        {
            _animator.SetTrigger(ShowTriggerAnimatorKey);
            await UniTask.Delay(TimeSpan.FromSeconds(_showHideDuration));
        }
    }
}
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
        private static readonly int HeadVectorAnimatorKey = Animator.StringToHash("HeadVector");
        private static readonly int AttackVectorAnimatorKey = Animator.StringToHash("AttackVector");
        private static readonly int DamageTriggerAnimatorKey = Animator.StringToHash("DamageTrigger");
        private static readonly int DeathTriggerAnimatorKey = Animator.StringToHash("DeathTrigger");

        [SerializeField] private Animator _animator;

        private bool _isAttackAnimationEnded = false;
        private bool _isHeadAnimationEnded = false;
        private bool _isDamageAnimationEnded = false;
        private bool _isDeathAnimationEnded = false;
        
        public async UniTask Attack(MovementDirection direction)
        {
            _isAttackAnimationEnded = false;
            
            switch (direction)
            {
                case MovementDirection.RightToLeft:
                    _animator.SetInteger(AttackVectorAnimatorKey, -1);
                    break;
                case MovementDirection.LeftToRight:
                    _animator.SetInteger(AttackVectorAnimatorKey, 1);
                    break;
                case MovementDirection.None:
                default:
                    _animator.SetInteger(AttackVectorAnimatorKey, 0);
                    break;
            }

            while (!_isAttackAnimationEnded)
            {
                await UniTask.NextFrame();
            }

            _isAttackAnimationEnded = false;
        }

        public async UniTask TurnHead(MovementDirection direction)
        {
            _isHeadAnimationEnded = false;
            
            switch (direction)
            {
                case MovementDirection.RightToLeft:
                    _animator.SetInteger(HeadVectorAnimatorKey, -1);
                    break;
                case MovementDirection.LeftToRight:
                    _animator.SetInteger(HeadVectorAnimatorKey, 1);
                    break;
                case MovementDirection.None:
                default:
                    _animator.SetInteger(HeadVectorAnimatorKey, 0);
                    break;
            }

            while (!_isHeadAnimationEnded)
            {
                await UniTask.NextFrame();
            }

            _isHeadAnimationEnded = false;
        }

        public async UniTask GetHit()
        {
            _isDamageAnimationEnded = false;
            
            _animator.SetTrigger(DamageTriggerAnimatorKey);

            while (!_isDamageAnimationEnded)
            {
                await UniTask.NextFrame();
            }

            _isDamageAnimationEnded = false;
        }

        public async UniTask Die()
        {
            _isDeathAnimationEnded = false;
            
            _animator.SetTrigger(DeathTriggerAnimatorKey);

            while (!_isDeathAnimationEnded)
            {
                await UniTask.NextFrame();
            }

            _isDeathAnimationEnded = false;
        }

        public void OnAttackAnimationEnd()
        {
            _isAttackAnimationEnded = true;
        }

        public void OnHeadAnimationEnd()
        {
            _isHeadAnimationEnded = true;
        }

        public void OnDamageAnimationEnd()
        {
            _isDamageAnimationEnded = true;
        }

        public void OnDeathAnimationEnd()
        {
            _isDeathAnimationEnded = true;
        }
    }
}
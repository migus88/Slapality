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
        private static readonly int DeathTriggerAnimatorKey = Animator.StringToHash("DeathTrigger");

        [SerializeField] private Animator _animator;
        
        public void Attack(MovementDirection direction)
        {
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
        }

        public void TurnHead(MovementDirection direction)
        {
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
        }

        public void Die()
        {
            _animator.SetTrigger(DeathTriggerAnimatorKey);
        }
    }
}
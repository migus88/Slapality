using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using Domains.Core.Subdomains.Battle.Code.Utils;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Managers
{
    public class BattleHandler : MonoBehaviour, IBattleHandler
    {
        [SerializeField] private float _delayBetweenAnimations = 1f;

        [Inject] private IGameState _gameState;
        [Inject] private FightConfiguration _fightConfiguration;

        private UniTaskCompletionSource<(float charge, GameButtonType hitDirection)> _chargeCompletionSource;
        private UniTaskCompletionSource<GameButtonType> _dodgeCompletionSource;

        private float _currentCharge = 0;
        private bool _shouldHandleCharge = false;
        private bool _shouldHandleDodge = false;

        private void Update()
        {
            UpdateHitDirection();
            UpdateCharge();
        }

        private void UpdateCharge()
        {
            if (!_shouldHandleCharge)
            {
                return;
            }

            _currentCharge -= _fightConfiguration.ChargeDropPerTick;
            _currentCharge = Mathf.Max(0f, _currentCharge);

            if (!_gameState.CurrentFighter.InputHandler.IsChargeButtonPressed)
            {
                return;
            }

            //TODO: Limit charge rate
            _currentCharge += _fightConfiguration.ChargePerTap;
            _currentCharge = MathF.Min(_fightConfiguration.MaxCharge, _currentCharge);
        }

        private void UpdateHitDirection()
        {
            if (!_shouldHandleCharge || !_gameState.CurrentFighter.InputHandler.IsGameButtonPressed)
            {
                return;
            }

            _shouldHandleCharge = false;
        }

        private void UpdateDodge()
        {
            if (!_shouldHandleDodge || !_gameState.CurrentFighter.InputHandler.IsGameButtonPressed)
            {
                return;
            }

            _shouldHandleDodge = false;
        }
        

        #region IBattleManager

        public async UniTask<(float charge, GameButtonType buttonPressed)> ChargeHit()
        {
            if (_shouldHandleCharge)
            {
                return await _chargeCompletionSource.Task;
            }

            _chargeCompletionSource = new UniTaskCompletionSource<(float charge, GameButtonType hitDirection)>();
            _gameState.CurrentFighter.InputHandler.ResetLastDirection();
            _currentCharge = 0f;
            _shouldHandleCharge = true;

            var timeElapsed = 0f;

            while (timeElapsed < _fightConfiguration.MaxSecondsToCharge)
            {
                await UniTask.NextFrame();
                timeElapsed += Time.deltaTime;

                if (!_shouldHandleCharge)
                {
                    break;
                }
            }

            var result = (_currentCharge, LastDirectionPressed: _gameState.CurrentFighter.InputHandler.LastGameButtonPressed);
            
            _chargeCompletionSource.TrySetResult(result);
            _shouldHandleCharge = false;

            return result;
        }

        public async UniTask<GameButtonType> SelectDodgeButton()
        {
            if (_shouldHandleDodge)
            {
                return await _dodgeCompletionSource.Task;
            }

            _dodgeCompletionSource = new UniTaskCompletionSource<GameButtonType>();
            _gameState.CurrentFighter.InputHandler.ResetLastDirection();
            
            _shouldHandleDodge = true;
            var timeElapsed = 0f;
            
            while (timeElapsed < _fightConfiguration.MaxSecondsToDodge)
            {
                await UniTask.NextFrame();
                timeElapsed += Time.deltaTime;

                if (!_shouldHandleDodge)
                {
                    break;
                }
            }

            var result = _gameState.CurrentEnemy.InputHandler.LastGameButtonPressed;
            _dodgeCompletionSource.TrySetResult(result);
            _shouldHandleDodge = false;

            return result;
        }

        public async UniTask<(float damage, DamageResult result)> CalculateDamage(float charge, GameButtonType hitButton, GameButtonType dodgeButton)
        {
            if (hitButton == GameButtonType.None)
            {
                return (0f, DamageResult.None);
            }

            if (dodgeButton == GameButtonType.None)
            {
                return (_fightConfiguration.BasicDamage * charge, DamageResult.Regular);
            }
            
            if (hitButton == dodgeButton)
            {
                return (0f, DamageResult.Dodge);
            }

            var critButton = hitButton.GetOppositeButton();

            if (dodgeButton == critButton)
            {
                return (_fightConfiguration.BasicDamage * charge * _fightConfiguration.CritMultiplier, DamageResult.Crit);
            }
            
            return (_fightConfiguration.BasicDamage * charge, DamageResult.Regular);
        }

        public async UniTask<RoundResult> ApplyDamage(float damage, DamageResult damageResult)
        {
            var playerView = _gameState.CurrentFighter.View;
            
            _gameState.CurrentEnemy.CurrentHP -= damage;
            var result = _gameState.CurrentEnemy.CurrentHP > 0 ? RoundResult.RoundEnded : RoundResult.GameEnded;

            if (damageResult == DamageResult.Regular)
            {
                playerView.AnimateSlap();
            }
            else if (damageResult == DamageResult.Crit)
            {
                playerView.AnimateCritSlap();
            }
            else if (damageResult == DamageResult.Dodge)
            {
                playerView.AnimateDodge();
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenAnimations));

            if (result == RoundResult.GameEnded)
            {
                playerView.AnimateDeath();
                await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenAnimations));
            }

            return result;
        }

        public async UniTask SwitchPlayers()
        {
            var hideTask = _gameState.CurrentFighter.View.Hide();
            var showTask = _gameState.CurrentEnemy.View.Show();

            var tasks = new List<UniTask> { hideTask, showTask };

            await UniTask.WhenAll(tasks);
            
            _gameState.NextRound();
        }

        public async UniTask GameOver()
        {
            //TODO: implement
        }
        

        #endregion
    }
}
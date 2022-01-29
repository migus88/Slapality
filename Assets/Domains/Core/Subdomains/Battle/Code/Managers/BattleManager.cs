using System;
using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Managers
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private GameObject _globalCountdownParent;
        [SerializeField] private TextMeshProUGUI _globalCountdownText;
        [SerializeField] private int _delayInSeconds;


        [Inject] private IBattleHandler _battleHandler;
        [Inject] private ICountdownView _countdownView;
        [Inject] private IGameState _gameState;
        [Inject] private FightConfiguration _fightConfiguration;

        private void Start()
        {
            Fight().Forget();
        }

        private async UniTaskVoid Fight()
        {
            foreach (var fighter in _gameState.Fighters)
            {
                fighter.View.Hide();
            }

            _globalCountdownParent.SetActive(true);
            _globalCountdownText.text = "Are you ready?";
            
            await UniTask.Delay(_delayInSeconds * 1000);

            for (int i = 0; i < _delayInSeconds + 1; i++)
            {
                _globalCountdownText.text = (_delayInSeconds - i).ToString();
                await UniTask.Delay(1000);
            }
            
            _globalCountdownParent.SetActive(false);

            await PlayRound();
        }

        private async UniTask PlayRound()
        {
            await _battleHandler.SwitchPlayers();

            _countdownView.Countdown(_fightConfiguration.MaxSecondsToCharge).Forget();
            var chargeResult = await _battleHandler.ChargeHit();

            if (chargeResult.buttonPressed == GameButtonType.None)
            {
                await PlayRound();
                return;
            }

            _countdownView.ShowButton(_fightConfiguration.MaxSecondsToDodge, chargeResult.buttonPressed,
                _gameState.CurrentEnemy.PlayerType);
            var dodgeResult = await _battleHandler.SelectDodgeButton();
            var damageResult =
                await _battleHandler.CalculateDamage(chargeResult.charge, chargeResult.buttonPressed, dodgeResult);

            var roundResult = await _battleHandler.ApplyDamage(damageResult.damage, damageResult.result);

            if (roundResult == RoundResult.RoundEnded)
            {
                await PlayRound();
            }
            else
            {
                await _battleHandler.GameOver();
                //TODO: Show winner dialog
            }
        }
    }
}
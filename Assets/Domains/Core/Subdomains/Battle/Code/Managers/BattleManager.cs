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
        
        //TODO: Move to configuration
        [SerializeField] private AudioClip _countdownAudio;
        [SerializeField] private AudioClip _battleMusic;
        [SerializeField] private AudioClip _slapAudio;
        [SerializeField] private AudioClip _critSlapAudio;
        [SerializeField] private AudioClip _dodgeAudio;
        [SerializeField] private float _slapDelay = 0.6f;
        



        [Inject] private IBattleHandler _battleHandler;
        [Inject] private ICountdownView _countdownView;
        [Inject] private IGameState _gameState;
        [Inject] private FightConfiguration _fightConfiguration;

        private void Start()
        {
            SoundManager.Instance?.StopMusic();
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

            SoundManager.Instance?.PlaySfx(_countdownAudio); //TODO: Fix this 'null thingie' with proper injection
            
            for (int i = 0; i < _delayInSeconds + 1; i++)
            {
                _globalCountdownText.text = (_delayInSeconds - i).ToString();
                await UniTask.Delay(1000);
            }

            SoundManager.Instance?.PlayMusic(_battleMusic);
            
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
            
            _countdownView.HideAll();
            
            var damageResult =
                await _battleHandler.CalculateDamage(chargeResult.charge, chargeResult.buttonPressed, dodgeResult);

            PlayAttackSound(damageResult.result);
            
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

        private void PlayAttackSound(DamageResult damageResult)
        {
            switch (damageResult)
            {
                case DamageResult.Regular:
                    SoundManager.Instance?.PlaySfxDelayed(_slapAudio, _slapDelay);
                    break;
                case DamageResult.Crit:
                    SoundManager.Instance?.PlaySfxDelayed(_critSlapAudio, _slapDelay);
                    break;
                case DamageResult.Dodge:
                    SoundManager.Instance?.PlaySfxDelayed(_dodgeAudio, _slapDelay);
                    break;
            }
        }
    }
}
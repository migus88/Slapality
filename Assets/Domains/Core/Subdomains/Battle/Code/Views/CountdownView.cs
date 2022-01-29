using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Views
{
    public class CountdownView : MonoBehaviour, ICountdownView
    {
        public CountdownState State { get; private set; }

        [SerializeField] private GameObject _timerParent;
        [SerializeField] private GameObject _buttonParent;
        
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private TextMeshProUGUI _buttonCountdownText;

        [Inject] private InputConfiguration _inputConfiguration;

        private bool _isNeedToStopCounting = false;
        private bool _isNeedToStopShowingButton = false;
        
        public async UniTask Countdown(float seconds)
        {
            _isNeedToStopCounting = false;
            _isNeedToStopShowingButton = true;
            
            _timerParent.SetActive(true);
            _buttonParent.SetActive(false);

            var elapsed = 0f;

            while (elapsed <= seconds)
            {
                if (_isNeedToStopCounting)
                {
                    break;
                }
                await UniTask.NextFrame();
                elapsed += Time.deltaTime;
                _countdownText.text = $"{(seconds - elapsed):F}";
            }
        }

        public async UniTask ShowButton(float durationSeconds, GameButtonType buttonType, PlayerType playerType)
        {
            _isNeedToStopCounting = true;
            _isNeedToStopShowingButton = false;
            
            _timerParent.SetActive(false);
            _buttonParent.SetActive(true);


            _buttonImage.sprite = _inputConfiguration.GetInput(playerType).GetSprite(buttonType);

            var elapsed = 0f;

            while (elapsed <= durationSeconds)
            {
                if (_isNeedToStopShowingButton)
                {
                    break;
                }
                await UniTask.NextFrame();
                elapsed += Time.deltaTime;
                _buttonCountdownText.text = $"{(durationSeconds - elapsed):F}";
            }
        }
    }
}
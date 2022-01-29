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
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private TextMeshProUGUI _buttonCountdownText;
        

        [Inject] private ButtonsConfiguration _buttonsConfiguration;
        
        
        public async UniTask Countdown(float seconds)
        {
            _timerParent.SetActive(true);
            _buttonParent.SetActive(false);

            var elapsed = 0f;

            while (elapsed <= seconds)
            {
                await UniTask.NextFrame();
                elapsed += Time.deltaTime;
                _countdownText.text = $"{(seconds - elapsed):F}";
            }
        }

        public async UniTask ShowButton(float durationSeconds, GameButtonType buttonType)
        {
            _timerParent.SetActive(false);
            _buttonParent.SetActive(true);

            _buttonText.text = buttonType.ToString();
            _buttonImage.sprite = _buttonsConfiguration.GetSprite(buttonType);

            var elapsed = 0f;

            while (elapsed <= durationSeconds)
            {
                await UniTask.NextFrame();
                elapsed += Time.deltaTime;
                _buttonCountdownText.text = $"{(durationSeconds - elapsed):F}";
            }
        }
    }
}
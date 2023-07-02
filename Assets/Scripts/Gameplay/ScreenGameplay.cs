using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenGameplay : MonoBehaviour
    {
        [Space] [SerializeField] private GameObject createAccount;
        [SerializeField] private GameObject waitTip;

        [Space] [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private Button startButton;

        private CanvasGroup _canvasGroup;


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            startButton.onClick.AddListener(RequestStart);

            nameInput.onValueChanged.AddListener(ValidateInput);
            ValidateInput(nameInput.text);
        }

        private void ValidateInput(string value)
        {
            startButton.interactable = value.Any();
        }


        private void OnEnable()
        {
            Gameplay.OnStart += OnStartGame;
            Gameplay.OnEnd += OnEndGame;
        }

        private void OnDisable()
        {
            Gameplay.OnStart -= OnStartGame;
            Gameplay.OnEnd -= OnEndGame;
        }


        public void RequestStart()
        {
            Switch_WaitCreate();

            var userName = nameInput.text;
            GameplayNetwork.Instance.CreateAccount(userName, () => { Gameplay.Instance.StartGame(); });
        }


        public void OnStartGame()
        {
            _canvasGroup.DOFade(0f, 0.25f);
            _canvasGroup.interactable = false;
        }

        public async void OnEndGame()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));

            Switch_CreateAccount();

            _canvasGroup.DOFade(1f, 0.25f);
            _canvasGroup.interactable = true;
        }


        private void Switch_CreateAccount()
        {
            createAccount.SetActive(true);
            waitTip.SetActive(false);
        }

        private void Switch_WaitCreate()
        {
            createAccount.SetActive(false);
            waitTip.SetActive(true);
        }
    }
}
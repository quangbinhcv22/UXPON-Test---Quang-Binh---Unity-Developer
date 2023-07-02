using Gameplay;
using TMPro;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayerName : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            GameplayNetwork.Instance.onCreateNew += OnCreateNew;
        }

        private void OnDisable()
        {
            GameplayNetwork.Instance.onCreateNew -= OnCreateNew;
        }

        private void OnCreateNew()
        {
            _text.SetText(GameplayNetwork.Instance.account.employee_name);
        }
    }
}
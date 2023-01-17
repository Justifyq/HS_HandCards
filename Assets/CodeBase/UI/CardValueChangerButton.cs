using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class CardValueChangerButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private CardRandomValueChanger valueChanger;
        
        private void Awake()
        {
            valueChanger.OnInitialized += EnableButton;
            button.onClick.AddListener(ChangeValue);
            DisableButton();
        }

        private void EnableButton() => 
            button.interactable = true;

        private void DisableButton() =>
            button.interactable = false;

        private void OnDestroy()
        {
            button.onClick.RemoveListener(ChangeValue);
            valueChanger.OnInitialized -= EnableButton;
        }

        private void ChangeValue() => 
            valueChanger.ChangeCardValue();
    }
}
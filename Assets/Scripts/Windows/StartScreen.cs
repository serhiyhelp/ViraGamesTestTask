using Logic;
using Services.ObjectMover;
using TMPro;
using UnityEngine;

namespace Windows
{
    public class StartScreen : WindowBase
    {
        [SerializeField] private TextMeshProUGUI coinAmountText;
        private GameObject _levelTimePlayingCounter;

        public void InitStartScreen(IObjectMover objectMover)
        {
            CloseButton.onClick.AddListener(StartGameClickAction);

            coinAmountText.text = PlayerPrefs.GetInt("Coin").ToString();
            Time.timeScale = 0;
            
            objectMover.MoveAction(true);
            _levelTimePlayingCounter = GameObject.FindWithTag("GameLifeTime");
        }

        private void StartGameClickAction()
        {
            Time.timeScale = 1;
            _levelTimePlayingCounter.GetComponent<LevelPlayingTimer>().StartLevelPlayingCoroutine();
        }
    }
}
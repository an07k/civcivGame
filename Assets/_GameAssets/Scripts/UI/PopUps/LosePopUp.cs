using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopUp : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private WinLoseUI _winloseui;
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _timerText;

    void OnEnable()
    {
        if (_timerUI == null)
        {
            Debug.LogWarning("LosePopUp: _timerUI is not assigned in the inspector!");
        }
        else if (_timerText == null)
        {
            Debug.LogWarning("LosePopUp: _timerText is not assigned in the inspector!");
        }
        else
        {
            _timerText.text = _timerUI.GetFinalTime();
        }

        if (_tryAgainButton == null)
        {
            Debug.LogWarning("LosePopUp: _tryAgainButton is not assigned in the inspector!");
        }
        else
        {
            _tryAgainButton.onClick.AddListener(OnTryAgainClicked);
        }
    }

    private void OnTryAgainClicked()
    {
        if (_winloseui == null)
        {
            Debug.LogWarning("LosePopUp: _winloseui is not assigned in the inspector!");
        }
        else
        {
            _winloseui._ifWinOrLose = false;
        }

        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }
}

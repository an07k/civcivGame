using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopUp : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WinLoseUI _winloseui;
    [SerializeField] TimerUI _timerUI;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _timerText;

    void OnEnable()
    {
        if (_timerUI == null)
        {
            Debug.LogWarning("WinPopUp: _timerUI is not assigned in the inspector!");
        }
        else if (_timerText == null)
        {
            Debug.LogWarning("WinPopUp: _timerText is not assigned in the inspector!");
        }
        else
        {
            _timerText.text = _timerUI.GetFinalTime();
        }

        if (_restartButton == null)
        {
            Debug.LogWarning("WinPopUp: _restartButton is not assigned in the inspector!");
        }
        else
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }
    }
    private void OnRestartClicked()
    {
        if (_winloseui == null)
        {
            Debug.LogWarning("WinPopUp: _winloseui is not assigned in the inspector!");
        }
        else
        {
            _winloseui._ifWinOrLose = false;
        }

        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }


}

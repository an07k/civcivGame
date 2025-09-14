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
        _timerText.text = _timerUI.GetFinalTime();
        _tryAgainButton.onClick.AddListener(OnTryAgainClicked);
    }

    private void OnTryAgainClicked()
    {
        _winloseui._ifWinOrLose = false;
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }
}
